import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

export interface Token {
  jwt: string;
  expiration: Date;
  isPlaying: boolean;
  userId: number;
}

@Injectable({
  providedIn: 'root'
})
export class TokenService {
     
    constructor(private http: HttpClient) {
    }
      
    headers() {
      return new HttpHeaders({
        'Authorization': 'Bearer ' + localStorage.getItem('userToken'),
        'Content-Type' : 'application/json',
        'Cache-Control' : 'no-cache'
      });
    }
      
    login(name:string, password:string ) {
        return this.http.post<Token>(environment.apiURL + '/token', {name, password});
    }

    switch(playerId: number) {
      return this.http.get<Token>(environment.apiURL + '/token/' + playerId, { headers : this.headers() });
  }
}