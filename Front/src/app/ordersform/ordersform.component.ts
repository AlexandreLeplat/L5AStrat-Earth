import { Component, OnInit } from '@angular/core';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { OrdersService, ActionType, Order, OrdersSheet, OrderStatus, OrderInputType } from '../services/orders.service';
import { Observable } from 'rxjs';
import { PlayersService, Player } from '../services/players.service';
import { MatCheckboxChange } from '@angular/material/checkbox';

@Component({
  selector: 'app-ordersform',
  templateUrl: './ordersform.component.html',
  styleUrls: ['./ordersform.component.css']
})
export class OrdersFormComponent implements OnInit {

  actionTypes: { [id: number]: ActionType };
  ordersSheet$: Observable<OrdersSheet>;
  orders: Order[];
  ordersSheetId: number;
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
    this.ordersSheet$ = this.ordersService.getCurrentOrdersSheet();
    this.ordersSheet$.subscribe(s => 
      {
        this.ordersSheetId = s.id;
        this.ordersService.getOrders(this.ordersSheetId).subscribe(o => this.orders = o);
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
  }

  addOrder() {
    console.log(this.actionTypes[this.selectedActionType]);
    var order: Order = { id:null, actionTypeId: this.actionTypes[this.selectedActionType].id, parameters: {}, comment: '', status: OrderStatus.None}
    this.ordersService.createOrder(this.ordersSheetId, order).subscribe(o => {
      this.orders.push(o);
      this.selectedActionType = null;
    });
  }

  removeOrder(order: Order) {
    this.ordersService.deleteOrder(this.ordersSheetId, order.id).subscribe(() => {
        var index: number = this.orders.indexOf(order);
        this.orders.splice(index, 1);
      });
  }

  updateOrder(order: Order) {
    console.log(order);
    this.ordersService.upateOrder(this.ordersSheetId, order).subscribe(o => console.log(o));
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

  updateCheckbox(order: Order, label:string, event: MatCheckboxChange) {
    order.parameters[label] = event.checked.toString();
    this.updateOrder(order);
  }
}
