import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

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
        return this.http.get<Player>(environment.apiURL + '/players/current', { headers : this.headers() });
    }

    getCampaignPlayers() {
      return this.http.get<Player[]>(environment.apiURL + '/campaigns/current/players', { headers : this.headers() });
  }

  }