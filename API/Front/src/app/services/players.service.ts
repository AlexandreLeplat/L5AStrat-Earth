import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

export enum PlayerStatus {
  none = 0,
  ready = 1,
  playing = 2,
  suspended = 3,
  finished = 4
}
export interface Player {
  id: number;
  name: string;
  color: string;
  hasNewMap: boolean;
  assets: { [category: string]: { [asset: string]: string; }; };
  status: number;
  userId: number;
  campaignId: number;
  isCurrentPlayer: boolean;
  campaignName: string;
  userName: string;
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

  getCurrentCampaignPlayers() {
    return this.http.get<Player[]>(environment.apiURL + '/campaigns/current/players', { headers : this.headers() });
  }

  getCampaignPlayers(campaignId: number) {
    return this.http.get<Player[]>(environment.apiURL + '/campaigns/' + campaignId + '/players', { headers : this.headers() });
  }

  getUserPlayers() {
    return this.http.get<Player[]>(environment.apiURL + '/players', { headers : this.headers() });
  }

  createPlayer(player: Player) {
    return this.http.post<Player>(environment.apiURL + '/players/', player, { headers : this.headers() });
  }

  updatePlayer(player: Player) {
    return this.http.put<Player>(environment.apiURL + '/players/' + player.id, player, { headers : this.headers() });
  }

  deletePlayer(id: number) {
    return this.http.delete(environment.apiURL + '/players/' + id, { headers : this.headers() });
  }

  getRandomNewPlayer(campaignId: number) {
    return this.http.get<Player>(environment.apiURL + '/campaigns/' + campaignId + '/randomplayer', { headers : this.headers() });
  }
}