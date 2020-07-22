import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ThemeService } from '../services/theme.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  isDarkTheme: Observable<boolean>;
  username: string;
  password: string;
  showSpinner: boolean;

  constructor(private router: Router, private themeService: ThemeService) { }

    ngOnInit() {  
      this.isDarkTheme = this.themeService.isDarkTheme;
      if (localStorage.getItem("userToken"))
      {
        this.router.navigate(['/','home']);
      }
    }
  
    login() : void {  
      if(this.username == 'admin' && this.password == 'admin'){  
        this.showSpinner = true;
        localStorage.setItem("userToken", "token");
        setTimeout(x => { this.router.navigate(['/','home']).then(nav => {
          console.log(nav); // true if navigation is successful
        }, err => {
          console.log(err) // when there's an error
        });; }, 1000);
      } else {  
        alert("Informations de connexion incorrectes");  
      }
  
    }
  }
