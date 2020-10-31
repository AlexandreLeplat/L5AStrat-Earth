import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

export interface Map {
  id: number;
  name: string;
  turn: number;
  playerId: number;
  campaignId: number;
  creationDate: Date;
}

export interface MapTile {
  id: number;
  name: string;
  x: number;
  y: number;
  symbol: string;
  color: string;
  borderColor: string;
  assets: { [category: string]: { [asset: string]: string; }; };
  mapId: number;
}

@Injectable({
  providedIn: 'root'
})
export class MapsService {

  constructor(private http: HttpClient) {
  }
    
  headers() {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('userToken'),
      'Content-Type' : 'application/json',
      'Cache-Control' : 'no-cache'
    });
  }

  getMapsList() {
    return this.http.get<Map[]>(environment.apiURL + '/maps', { headers : this.headers() });
  }

  getMapTiles(id: number) {
    return this.http.get<MapTile[][]>(environment.apiURL + '/maps/'+ id + '/tiles', { headers : this.headers() });
  }
}
