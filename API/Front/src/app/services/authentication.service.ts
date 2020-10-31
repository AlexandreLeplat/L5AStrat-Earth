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
    if(!token) return false;

    const expiration = localStorage.getItem('expiresAt');
    if(!expiration) return false;
    return (Date.parse(expiration) > new Date().valueOf());
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
