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
  status: number;
  creatorId: number;
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
  
  getCampaigns() {
    return this.http.get<Campaign[]>(environment.apiURL + '/campaigns', { headers : this.headers() });
  }
  
  getCampaign(id: number) {
    return this.http.get<Campaign>(environment.apiURL + '/campaigns/' + id.toString(), { headers : this.headers() });
  }
  
  getCurrentCampaign() {
      return this.http.get<Campaign>(environment.apiURL + '/campaigns/current', { headers : this.headers() });
  }

  createCampaign(campaign: Campaign) {
    return this.http.post<Campaign>(environment.apiURL + '/campaigns', campaign, { headers : this.headers() });
  }
  
  removeCampaign(campaignId: number) {
    return this.http.delete(environment.apiURL + '/campaigns/' + campaignId, { headers : this.headers() });
  }
  
  workOnCampaign(id: number) {
    return this.http.post<Campaign>(environment.apiURL + '/campaigns/' + id + '/work', '', { headers : this.headers() });
  }

  startCampaign(id: number) {
    return this.http.post<Campaign>(environment.apiURL + '/campaigns/' + id + '/start', '', { headers : this.headers() });
  }
}