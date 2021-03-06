import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

export interface Message {
  id: number;
  subject: string;
  body: string;
  isNotification: boolean;
  isRead: boolean;
  isArchived: boolean;
  sendDate: Date;
  playerId: number;
  senderId: number;
  turn: number;
  previousMessageId: number;
}

@Injectable({
  providedIn: 'root'
})
export class MessagesService {

  constructor(private http: HttpClient) { }

  headers() {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('userToken'),
      'Content-Type' : 'application/json',
      'Cache-Control' : 'no-cache'
    });
  }

  sendMessage(message: Message) {
    return this.http.post<Message>(environment.apiURL + '/messages', message, { headers : this.headers() });
  }

  getMessage(id: number) {
    return this.http.get<Message>(environment.apiURL + '/messages/'+id, { headers : this.headers() });
  }

  getMessages(pageSize:number, pageIndex: number) {
    return this.http.get<Message[]>(environment.apiURL + '/messages?pageSize='+pageSize+'&pageIndex='+pageIndex, { headers : this.headers() });
  }

  getSentMessages(pageSize:number, pageIndex: number) {
    return this.http.get<Message[]>(environment.apiURL + '/messages/sent?pageSize='+pageSize+'&pageIndex='+pageIndex, { headers : this.headers() });
  }

  getMessageCount(category:string) {
    return this.http.get<number>(environment.apiURL + '/messages/count?category='+category, { headers : this.headers() });
  }

  updateMessage(message: Message) {
    return this.http.put<Message>(environment.apiURL + '/messages/' + message.id, message, { headers : this.headers() });
  }

  deleteMessage(id: number) {
    return this.http.delete(environment.apiURL + '/messages/' + id, { headers : this.headers() });
  }
}
