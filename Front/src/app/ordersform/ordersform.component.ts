import { Component, OnInit } from '@angular/core';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { OrdersService, ActionType, Order, OrdersSheet, OrderStatus, OrderInputType } from '../services/orders.service';
import { PlayersService, Player } from '../services/players.service';
import { MatCheckboxChange } from '@angular/material/checkbox';

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
  opponents: Player[];
  selectedActionType: number;
    
  constructor(private ordersService: OrdersService, private playersService: PlayersService) { }

  ngOnInit(): void {
    this.ordersService.getActionTypes().subscribe(a => {
      this.actionTypes = {};
      a.forEach(t => {
        this.actionTypes[t.id] = t;
      });
    });
    this.ordersService.getCurrentOrdersSheet().subscribe(s => 
      {
        this.ordersSheet = s;        
        this.ordersService.getOrders(this.ordersSheet.id).subscribe(o => this.orders = o);
      });
      this.playersService.getCurrentPlayer().subscribe(p => {
        this.player = p;
        this.playersService.getCampaignPlayers().subscribe(c => {
          this.opponents = c;
          var index: number = this.opponents.findIndex(f => f.id == p.id);
          this.opponents.splice(index, 1);
        });
      })
  }

  drop(event: CdkDragDrop<Order[]>) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
      this.orders.forEach(order => {
        order.rank = this.orders.indexOf(order);
      });
      this.ordersService.updateAllOrders(this.ordersSheet.id, this.orders).subscribe();
  }

  addOrder() {
    var order: Order = { id:null, actionTypeId: this.actionTypes[this.selectedActionType].id, parameters: {},  
        comment: '', status: OrderStatus.None, rank: this.orders.length }
    this.ordersService.createOrder(this.ordersSheet.id, order).subscribe(o => {
      this.orders.push(o);
      this.selectedActionType = null;
    });
  }

  removeOrder(order: Order) {
    this.ordersService.deleteOrder(this.ordersSheet.id, order.id).subscribe(() => {
        var index: number = this.orders.indexOf(order);
        this.orders.splice(index, 1);
      });
  }

  updateOrder(order: Order) {
    this.ordersService.updateOrder(this.ordersSheet.id, order).subscribe();
  }

  inputType(type: string): number {
    switch(type)
    {
      case 'checkbox': return 1;
      case 'opponent': return 2;
      case 'entryTile': return 3;
      case 'unit': return 4;
      case 'unitMove': return 5;
      case 'formation': return 6;
      default: return 0;
    }
  }

  updatePriority() {
    this.ordersService.updateOrdersSheet(this.ordersSheet).subscribe(s => {});
  }

  updateCheckbox(order: Order, label:string, event: MatCheckboxChange) {
    order.parameters[label] = event.checked.toString();
    this.updateOrder(order);
  }

  submitOrdersSheet() {
    this.ordersService.submitOrdersSheet().subscribe(s => this.ordersSheet = s);
  }
}
