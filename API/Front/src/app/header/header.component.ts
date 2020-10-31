import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ThemeService } from '../services/theme.service';
import { RulesComponent } from '../rules/rules.component';
import { InfoComponent } from '../info/info.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  loggedIn: boolean;
  isDarkTheme: boolean;
  
  constructor(private router: Router, private themeService: ThemeService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.themeService.isDarkTheme.subscribe(b => this.isDarkTheme = b);
    if (localStorage.getItem("userToken"))
    {
      this.loggedIn = true;
    }
  }

  logOut(): void {
    localStorage.removeItem("userToken");
    localStorage.removeItem("expiresAt");
    this.router.navigate(['/','login'])
  }

  openInfoDialog(): void {
    this.dialog.open(InfoComponent);
  }

  openRulesDialog(): void {
    this.dialog.open(RulesComponent);
  }

  switchBrightness(dark: boolean): void {
    this.themeService.setDarkTheme(dark);
  }

}
