import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

export interface OrderInput {
  label: string;
  type: number;
  description: string;
}
export interface ActionType {
  id: number;
  label: string;
  description: string;
  form: OrderInput[];
}
export interface Order {
  id: number;
  actionTypeId: number;
  comment: string;
  parameters: { [key: string]: string; };
}
export interface OrdersSheet {
  id: number;
  priority: number;
  turn: number;
  maxOrdersCount: number;
}

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
     
  constructor(private http: HttpClient) {
  }
  
  headers() {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('userToken'),
      'Content-Type' : 'application/json'
    });
  }
  
  getActionTypes() {
    return this.http.get<ActionType[]>(environment.apiURL + '/ref/actiontypes', { headers : this.headers() });
  }

  getCurrentOrdersSheet() {
      return this.http.get<OrdersSheet>(environment.apiURL + '/orderssheets/current', { headers : this.headers() });
  }

  getOrders(idSheet: number) {
    return this.http.get<Order[]>(environment.apiURL + '/orderssheets/' + idSheet + '/orders', { headers : this.headers() });
  }

  createOrder(idSheet: number, order: Order) {
    return this.http.post<Order>(environment.apiURL + '/orderssheets/' + idSheet + '/orders', order, { headers : this.headers() });
  }

  upateOrder(idSheet: number, order: Order) {
    console.log('update');
    return this.http.put<Order>(environment.apiURL + '/orderssheets/' + idSheet + '/orders/' + order.id, order, { headers : this.headers() });
  }

  deleteOrder(idSheet: number, idOrder: number) {
    return this.http.delete(environment.apiURL + '/orderssheets/' + idSheet + '/orders/' + idOrder, { headers : this.headers() });
  }
}