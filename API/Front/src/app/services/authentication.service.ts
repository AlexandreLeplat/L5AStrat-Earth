import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt'
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router'

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

  canActivate(route: ActivatedRouteSnapshot): boolean {
    if (!this.isAuthenticated()) {
      this.router.navigate(['login']);
      localStorage.removeItem('userToken');
      return false;
    }
    if (route.data.isPlaying && !localStorage.getItem('isPlaying')) {
      this.router.navigate(['lobby']);
      return false;
    }
    if (!route.data.isPlaying && localStorage.getItem('isPlaying')) {
      this.router.navigate(['home']);
      return false;
    }
    return true;
  }
}
