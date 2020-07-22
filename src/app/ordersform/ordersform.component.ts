import { Component, OnInit } from '@angular/core';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';

export interface OrderInput {
  id: number;
  label: string;
  type: string;
  description: string;
  required: boolean;
}
export interface ActionType {
  label: string;
  description: string;
  form: OrderInput[];
}
export interface Order {
  type: ActionType;
  summary: string;
  parameters: { [key: number]: string; };
}

@Component({
  selector: 'app-ordersform',
  templateUrl: './ordersform.component.html',
  styleUrls: ['./ordersform.component.css']
})
export class OrdersFormComponent implements OnInit {

  actionTypes: { [label: number]: ActionType } =
  {
    0:
    { label: 'Déplacement', 
      description: 'Déplacez une armée', 
      form: [] },
    1:
      { label: 'Flatterie', 
        description: 'Gagnez un point de Gloire', 
        form: [ { id: 0,
                  label: "Cible",
                  type: 'opponent', 
                  description: 'Ciblez un adversaire pour lui faire gagner 2 points de Gloire et gagner un point de Gloire supplémentaire',
                  required: false },
                { id: 1,
                  label: "Augmentation", 
                  type: 'checkbox', 
                  description: 'Dépensez une Influence pour gagner 5 points de Gloire supplémentaires',
                  required: false },
             ] },
    2:
      { label: 'Planification', 
        description: 'Gagnez un point de Stratégie', 
        form: [ { id: 0,
                  label: "Manoeuvre sournoise",
                  type: 'checkbox', 
                  description: 'Gagnez 2 points d\'Infamie pour gagner un point de Stratégie supplémentaire',
                  required: false },
                { id: 1,
                  label: "Augmentation", 
                  type: 'checkbox', 
                  description: 'Dépensez une Influence pour gagner 5 points de Stratégie supplémentaires',
                  required: false },
             ] },
    3:
    { label: 'Renfort', 
      description: 'Ajoutez une armée sur carte', 
      form: [] }
  };

  selectedActionType: number;

  orders: Order[] =
  [
    { type: this.actionTypes[0], summary: 'A4 -> B3', parameters: {} },
    { type: this.actionTypes[1], summary: '+2 Gloire', parameters: { 0:"Doji Misao", 1:"False" } },
    { type: this.actionTypes[3], summary: '-> A3', parameters: {} }
  ];
    
  constructor() { }

  ngOnInit(): void {
  }

  drop(event: CdkDragDrop<Order[]>) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
  }

  addOrder() {
    var order: Order = { type: this.actionTypes[this.selectedActionType], summary:'', parameters: {}}
    this.orders = this.orders.concat(order);
    this.selectedActionType = null;
  }

  remove(order: Order) {
    var index: number = this.orders.indexOf(order);
    this.orders.splice(index, 1);
  }
}
