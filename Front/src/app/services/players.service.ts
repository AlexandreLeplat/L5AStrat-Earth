import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

export interface Player {
  id: number;
  name: string;
  // color: string;
  assets: { [category: string]: { [asset: string]: string; }; };
  userId: number;
  campaignId: number;
}

@Injectable({
  providedIn: 'root'
})
export class PlayersService {
     
    constructor(private http: HttpClient) {
    }
    
    headers() {
      return new HttpHeaders({
        'Authorization': 'Bearer ' + localStorage.getItem('userToken'),
        'Content-Type' : 'application/json'
      });
    }
    
    getCurrentPlayer() {
        return this.http.get<Player>('https://localhost:44332/players/current', { headers : this.headers() });
    }
}