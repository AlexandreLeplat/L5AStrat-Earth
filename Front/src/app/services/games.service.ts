import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

export interface Game {
  id: number;
  name: string;
  homeWidgets: string[];
}

@Injectable({
  providedIn: 'root'
})
export class GamesService {
     
    constructor(private http: HttpClient) {
    }
    
    headers() {
      return new HttpHeaders({
        'Authorization': 'Bearer ' + localStorage.getItem('userToken'),
        'Content-Type' : 'application/json'
      });
    }
    
    getCurrentCampaign() {
        return this.http.get<Game>('https://localhost:44332/games/current', { headers : this.headers() });
    }
}