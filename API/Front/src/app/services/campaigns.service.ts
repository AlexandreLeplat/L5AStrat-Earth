import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

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
        'Content-Type' : 'application/json',
        'Cache-Control' : 'no-cache'
      });
    }
    
    getCurrentCampaign() {
        return this.http.get<Campaign>(environment.apiURL + '/campaigns/current', { headers : this.headers() });
    }

    workOnCampaign(id: number) {
      return this.http.post<Campaign>(environment.apiURL + '/campaigns/' + id + '/work', '', { headers : this.headers() });
  }

}