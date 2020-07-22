import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  API_KEY = 'l5aStrat';

  constructor(private httpClient: HttpClient) { }

  public authenticateUser(user : string, password : string){
    return this.httpClient.get(`http://localhost:80?user=${user}&amp;apiKey=${this.API_KEY}`);
  }
  
}
