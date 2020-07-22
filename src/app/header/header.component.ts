import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ThemeService } from '../services/theme.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  loggedIn: boolean;
  isDarkTheme: boolean;
  
  constructor(private router: Router, private themeService: ThemeService) { }

  ngOnInit(): void {
    this.themeService.isDarkTheme.subscribe(b => this.isDarkTheme = b);
    if (localStorage.getItem("userToken"))
    {
      this.loggedIn = true;
    }
  }

  logOut(): void {
    localStorage.removeItem("userToken");
    this.router.navigate(['/','login'])
  }

  navigateToInfo(): void {
    this.router.navigate(['/','info']);
  }

  navigateToRules(): void {
    this.router.navigate(['/','rules']);
  }

  switchBrightness(dark: boolean): void {
    this.themeService.setDarkTheme(dark);
  }

}
