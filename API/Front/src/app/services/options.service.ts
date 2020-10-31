import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OptionsService {

  constructor(private http: HttpClient) { }

  headers() {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('userToken'),
      'Content-Type' : 'application/json',
      'Cache-Control' : 'no-cache'
    });
  }

  getOptions(resource: string, parameters: string) {
    return this.http.get<{ [label: string]: string; }>(environment.apiURL + '/options/'+ resource + "?parameters=" + parameters, { headers : this.headers() });
  }
}
