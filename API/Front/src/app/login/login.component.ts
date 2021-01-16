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
    if (localStorage.getItem("userToken")) {
      if (localStorage.getItem("isPlaying"))
      {
        this.router.navigate(['/','home']);
      } else {
        this.router.navigate(['/','lobby']);
      }
    }
  }

  login() : void {  
    this.showSpinner = true;
    var token = this.tokenService.login(this.username, this.password);
    token.subscribe({
      next: data => { 
        localStorage.setItem("userToken", data.jwt);
        localStorage.setItem("userId", data.userId.toString());
        localStorage.setItem("expiresAt", data.expiration.toString());
        if (data.isPlaying) {
          localStorage.setItem("isPlaying", "yes");
          this.router.navigate(['/','home']);
        } else {
          localStorage.removeItem("isPlaying");
          this.router.navigate(['/','lobby']);
        }
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

  subscribe(): void {
    this.router.navigate(['/','subscribe']);
  }
}