import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable()
export class ThemeService {
  private _darkTheme = new Subject<boolean>();
  isDarkTheme = this._darkTheme.asObservable();

  constructor() {
    if (localStorage.getItem("nightMode") == "ON")
    {
      this._darkTheme.next(true);
    }
  }

  setDarkTheme(isDarkTheme: boolean): void {
    this._darkTheme.next(isDarkTheme);
    if (isDarkTheme)
    {
      localStorage.setItem("nightMode", "ON");
    } else {
      localStorage.removeItem("nightMode");
    }
  }
}