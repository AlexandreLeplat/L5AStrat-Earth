import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

export interface Campaign {
  id: number;
  name: string;
  assets: { [category: string]: { [asset: string]: string; }; };
  currentPhase: number;
  nextPhase: Date;
  currentTurn: number;
  phaseLength: number;
  gameId: number;
}

@Injectable({
  providedIn: 'root'
})
export class CampaignsService {
     
    constructor(private http: HttpClient) {
    }
    
    headers() {
      return new HttpHeaders({
        'Authorization': 'Bearer ' + localStorage.getItem('userToken'),
        'Content-Type' : 'application/json'
      });
    }
    
    getCurrentCampaign() {
        return this.http.get<Campaign>('https://localhost:44332/campaigns/current', { headers : this.headers() });
    }
}