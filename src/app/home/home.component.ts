import { Component, OnInit } from '@angular/core';
import { ThemeService } from '../services/theme.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  showSpinner: boolean;
  isDarkTheme: Observable<boolean>;
  
  constructor(private themeService: ThemeService) { }

  ngOnInit(): void {
    this.showSpinner = true;
    setTimeout(x => { this.showSpinner = false }, 1000);
    this.isDarkTheme = this.themeService.isDarkTheme;
  }

}
