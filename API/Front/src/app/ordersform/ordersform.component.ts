import { Component, OnInit } from '@angular/core';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { OrdersService, ActionType, Order, OrdersSheet, OrderStatus } from '../services/orders.service';
import { PlayersService, Player } from '../services/players.service';
import { OptionsService } from '../services/options.service';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { CampaignsService } from '../services/campaigns.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-ordersform',
  templateUrl: './ordersform.component.html',
  styleUrls: ['./ordersform.component.css']
})
export class OrdersFormComponent implements OnInit {

  actionTypes: { [id: number]: ActionType };
  ordersSheet: OrdersSheet;
  orders: Order[];
  player: Player;
  selectedActionType: number;
  optionsLists: { [orderId: number]: { [inputLabel: string]: { [label: string]: string; }; }; } = {};
    
  constructor(private ordersService: OrdersService, private playersService: PlayersService, 
    private campaignsService: CampaignsService, private purchasesService: OptionsService) { }

  ngOnInit(): void {
    this.ordersService.getActionTypes().subscribe(a => {
      this.actionTypes = {};
      a.forEach(t => {
        this.actionTypes[t.id] = t;
      });
    });
    this.ordersService.getCurrentOrdersSheet().subscribe(s => {
        this.ordersSheet = s;        
        this.ordersService.getOrders(this.ordersSheet.id).subscribe(o => {
          this.orders = o;
          this.orders.forEach(e => this.retrieveOptionsList(e));
        });
      });
      this.playersService.getCurrentPlayer().subscribe(p => {
        this.player = p;
      })
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
    var order: Order = { id:null, actionTypeId: this.actionTypes[this.selectedActionType].id, parameters: {},  
        comment: '', status: OrderStatus.None, rank: this.orders.length, selected: true }
      this.ordersService.createOrder(this.ordersSheet.id, order).subscribe(o => {
        this.retrieveOptionsList(o);
        this.orders.forEach(i => i.selected = false);
        this.orders.push(o);
        o.selected = true;
        this.selectedActionType = null;
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

  retrieveOptionsList(order: Order) {
    this.actionTypes[order.actionTypeId].form.forEach(input => 
      {
        var parameterValue = "";
        if (input.parameter) {
          parameterValue = order.parameters[input.parameter];
        }
        this.purchasesService.getOptions(input.type, parameterValue).subscribe(s => {
          if (!this.optionsLists[order.id]) {
            this.optionsLists[order.id] = {};
          }
          this.optionsLists[order.id][input.label] = s;
        });
      });
    }

  updateOrder(order: Order) {
    this.ordersService.updateOrder(this.ordersSheet.id, order).subscribe(o => {
      this.retrieveOptionsList(o);
      this.checkOrdersSheet();
    });
  }

  updatePriority() {
    this.ordersService.updateOrdersSheet(this.ordersSheet).subscribe(s => {});
  }

  updateCheckbox(order: Order, label:string, event: MatCheckboxChange) {
    order.parameters[label] = event.checked.toString();
    this.updateOrder(order);
  }

  submitOrdersSheet() {
    this.ordersService.submitOrdersSheet().subscribe(s => 
      {
        this.ordersSheet = s;
        this.campaignsService.getCurrentCampaign().subscribe(c => this.campaignsService.workOnCampaign(c.id).subscribe(i =>
          {
            this.ordersService.getOrders(this.ordersSheet.id).subscribe(o => {
              this.orders = o;
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