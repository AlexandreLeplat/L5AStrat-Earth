import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ThemeService } from '../services/theme.service';
import { Observable } from 'rxjs';
import { TokenService } from '../services/token.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  isDarkTheme: Observable<boolean>;
  username: string;
  password: string;
  errorMessage: string;
  showSpinner: boolean;

  constructor(private router: Router, private themeService: ThemeService, private tokenService: TokenService) { }

  ngOnInit() {  
    this.isDarkTheme = this.themeService.isDarkTheme;
    if (localStorage.getItem("userToken"))
    {
      this.router.navigate(['/','home']);
    }
  }

  login() : void {  
    this.showSpinner = true;
    var token = this.tokenService.login(this.username, this.password);
    token.subscribe({
      next: data => { 
        localStorage.setItem("userToken", data.jwt);
        localStorage.setItem("expiresAt", data.expiration.toString());
        this.router.navigate(['/','home']);
      },
      error: error => {
        this.showSpinner = false;
        if (error.status == 401)
        {
          this.errorMessage = "Informations de connexion incorrectes";
          this.password = "";
        }
        else
        {
          this.errorMessage = "Erreur technique à l'authentification";
          this.password = "";
        }
      } 
    });
  }
}