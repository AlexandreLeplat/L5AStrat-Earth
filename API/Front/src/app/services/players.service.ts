import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

export interface Player {
  id: number;
  name: string;
  color: string;
  hasNewMap: boolean;
  assets: { [category: string]: { [asset: string]: string; }; };
  userId: number;
  campaignId: number;
  isCurrentPlayer: boolean;
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
      'Content-Type' : 'application/json',
      'Cache-Control' : 'no-cache'
    });
  }
    
  getCurrentPlayer() {
      return this.http.get<Player>(environment.apiURL + '/players/current', { headers : this.headers() });
  }

  getCampaignPlayers() {
    return this.http.get<Player[]>(environment.apiURL + '/campaigns/current/players', { headers : this.headers() });
  }

  getUserPlayers() {
    return this.http.get<Player[]>(environment.apiURL + '/players', { headers : this.headers() });
  }

  updatePlayer(player: Player) {
    return this.http.put<Player>(environment.apiURL + '/players/' + player.id, player, { headers : this.headers() });
  }
}