import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt'
import { Router, CanActivate } from '@angular/router'

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService implements CanActivate {

  constructor(private jwtHelper: JwtHelperService, private router: Router) { }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem('userToken');
    //return !this.jwtHelper.isTokenExpired(token);
    return (token ? true : false);
  }

  canActivate(): boolean {
    if (!this.isAuthenticated())
    {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }
}
