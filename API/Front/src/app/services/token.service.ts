import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

export interface Token {
  jwt: string;
  expiration: Date;
}

@Injectable({
  providedIn: 'root'
})
export class TokenService {
     
    constructor(private http: HttpClient) {
    }
      
    login(name:string, password:string ) {
        return this.http.post<Token>(environment.apiURL + '/token', {name, password});
    }
}