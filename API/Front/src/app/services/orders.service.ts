import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

export enum OrderStatus {
  None = 0,
  Invalid = 1,
  Valid = 2,
  Completed = 3,
  Failed = 4,
  Error = 5
}
export enum OrdersSheetStatus {
  None = 0,
  Writing = 1,
  Planned = 2,
  Treating = 3,
  Completed = 4,
  Expired = 5,
  Error = 6
}

export interface OrderInput {
  label: string;
  type: string;
  description: string;
  parameter: string;
  isPredefinedOnMap: boolean;
  isSelectedTileOnMap: boolean;
  isSelectableOnMap: boolean;
  mapDescription: string;
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
  rank: number;
  status: OrderStatus;
  selected: boolean;
}
export interface OrdersSheet {
  id: number;
  priority: number;
  turn: number;
  maxOrdersCount: number;
  maxPriority: number;
  status: OrdersSheetStatus;
  sendDate: Date;
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
      'Content-Type' : 'application/json',
      'Cache-Control' : 'no-cache'
    });
  }
  
  getActionTypes() {
    return this.http.get<ActionType[]>(environment.apiURL + '/ref/actiontypes', { headers : this.headers() });
  }

  getOrdersSheets() {
    return this.http.get<OrdersSheet[]>(environment.apiURL + '/orderssheets', { headers : this.headers() });
  }

  getCurrentOrdersSheet() {
    return this.http.get<OrdersSheet>(environment.apiURL + '/orderssheets/current', { headers : this.headers() });
  }

  updateOrdersSheet(ordersSheet: OrdersSheet) {
    return this.http.put<OrdersSheet>(environment.apiURL + '/orderssheets/' + ordersSheet.id, ordersSheet, { headers : this.headers() });
  }

  checkOrdersSheet() {
    return this.http.post<Order[]>(environment.apiURL + '/orderssheets/check', null, { headers : this.headers() });
  }

  submitOrdersSheet() {
    return this.http.post<OrdersSheet>(environment.apiURL + '/orderssheets/submit', null, { headers : this.headers() });
  }

  getOrders(idSheet: number) {
    return this.http.get<Order[]>(environment.apiURL + '/orderssheets/' + idSheet + '/orders', { headers : this.headers() });
  }

  createOrder(idSheet: number, order: Order) {
    return this.http.post<Order>(environment.apiURL + '/orderssheets/' + idSheet + '/orders', order, { headers : this.headers() });
  }

  updateOrder(idSheet: number, order: Order) {
    return this.http.put<Order>(environment.apiURL + '/orderssheets/' + idSheet + '/orders/' + order.id, order, { headers : this.headers() });
  }

  deleteOrder(idSheet: number, idOrder: number) {
    return this.http.delete(environment.apiURL + '/orderssheets/' + idSheet + '/orders/' + idOrder, { headers : this.headers() });
  }
}