import { Component, OnInit } from '@angular/core';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { OrdersService, ActionType, Order, OrdersSheet, OrderStatus, OrdersSheetStatus } from '../services/orders.service';
import { OptionsService } from '../services/options.service';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { CampaignsService } from '../services/campaigns.service';
import { forkJoin } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-ordersform',
  templateUrl: './ordersform.component.html',
  styleUrls: ['./ordersform.component.css']
})
export class OrdersFormComponent implements OnInit {

  actionTypes: ActionType[];
  ordersSheet: OrdersSheet;
  sheetsList: OrdersSheet[];
  orders: Order[];
  selectedActionType: number;
  writingStatus: number = OrdersSheetStatus.Writing;
  optionsLists: { [orderId: number]: { [inputLabel: string]: { [label: string]: string; }; }; } = {};
    
  constructor(private ordersService: OrdersService, private campaignsService: CampaignsService
    , private optionsService: OptionsService, private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.ordersService.getActionTypes().subscribe(a => {
      this.actionTypes = a;
    });
    this.ordersService.getOrdersSheets().subscribe(l => {
      this.sheetsList = l;
      this.ordersSheet = l[0];
      this.ordersService.getOrders(this.ordersSheet.id).subscribe(o => {
        this.orders = o;
        this.retrieveOptionsLists();
      });
    });
  }

  orderType(order: Order): ActionType {
    return this.actionTypes.find(a => a.id == order.actionTypeId);
  }

  selectSheet() {
    this.ordersService.getOrders(this.ordersSheet.id).subscribe(o => {
      this.orders = o;
      this.retrieveOptionsLists();
    });
  }

  drop(event: CdkDragDrop<Order[]>) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
      var tasks$ = [];
      this.orders.forEach(order => {
        order.rank = this.orders.indexOf(order);
        tasks$.push(this.ordersService.updateOrder(this.ordersSheet.id, order));
      });
      forkJoin(tasks$).subscribe(results => this.checkOrdersSheet());
    }

  addOrder() {
    var order: Order = { id: null, actionTypeId: this.selectedActionType, parameters: {},  
        comment: '', status: OrderStatus.None, rank: this.orders.length, selected: true }
    this.ordersService.createOrder(this.ordersSheet.id, order).subscribe(o => {
      this.orders.forEach(i => i.selected = false);
      this.orders.push(o);
      o.selected = true;
      this.selectedActionType = null;
      this.retrieveOptionsLists();
      this.checkOrdersSheet();
    });
  }

  removeOrder(order: Order) {
    this.ordersService.deleteOrder(this.ordersSheet.id, order.id).subscribe(() => {
        var index: number = this.orders.indexOf(order);
        this.orders.splice(index, 1);
        this.checkOrdersSheet();
      });
  }

  retrieveOptionsLists() {
    this.orders.forEach(order => {    
      this.orderType(order).form.forEach(input => {
        var parameterValue = "";
        if (input.parameter) {
          parameterValue = order.parameters[input.parameter];
        }
        this.optionsService.getOptions(input.type, parameterValue).subscribe(s => {
          if (!this.optionsLists[order.id]) {
            this.optionsLists[order.id] = {};
          }
          this.optionsLists[order.id][input.label] = s;
        });
      });
    });
  }

  updateOrder(order: Order) {
    this.ordersService.updateOrder(this.ordersSheet.id, order).subscribe(o => {
      this.retrieveOptionsLists();
      this.checkOrdersSheet();
    });
  }

  updatePriority() {
    this.ordersService.updateOrdersSheet(this.ordersSheet).subscribe(s => {});
    this.checkOrdersSheet();
  }

  updateCheckbox(order: Order, label:string, event: MatCheckboxChange) {
    order.parameters[label] = event.checked.toString();
    this.updateOrder(order);
  }

  submitOrdersSheet() {
    this.ordersService.submitOrdersSheet().subscribe(s => {
      this.ordersSheet.status = s.status;
      this.ordersSheet.sendDate = s.sendDate;
      this.campaignsService.getCurrentCampaign().subscribe(c => this.campaignsService.workOnCampaign(c.id).subscribe(i => {
        this.ordersService.getOrders(this.ordersSheet.id).subscribe(o => {
          this.orders = o;
          this.snackBar.open("Ordres envoyés", "", { duration : 2500 })
        });
      }));
    });
  }

  checkOrdersSheet() {
    this.ordersService.checkOrdersSheet().subscribe(ordersList => {
      var selectedOrderId = this.orders.find(o => o.selected).id;
      this.orders = ordersList;
      this.orders.find(o => o.id == selectedOrderId).selected = true;
    });
  }
}
