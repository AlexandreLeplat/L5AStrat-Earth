import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

export interface User {
  id: number;
  name: string;
  role: number;
  password: string;
}

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) {
  }
    
  headers() {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('userToken'),
      'Content-Type' : 'application/json',
      'Cache-Control' : 'no-cache'
    });
  }
    
  getCurrentUser() {
    return this.http.get<User>(environment.apiURL + '/users/current', { headers : this.headers() });
  }

  createUser(user: User, subscriptionKey: string) {
    return this.http.post<User>(environment.apiURL + '/users/?subscriptionKey=' + subscriptionKey, user);
  }
}
