import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ThemeService } from '../services/theme.service';
import { RulesComponent } from './rules/rules.component';
import { InfoComponent } from './info/info.component';
import { Player, PlayersService } from '../services/players.service';
import { TokenService } from '../services/token.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  loggedIn: boolean;
  isDarkTheme: boolean;
  playersList: Player[];
  
  constructor(private router: Router, private themeService: ThemeService, public dialog: MatDialog
        , private playersService: PlayersService, private tokenService: TokenService) { }

  ngOnInit(): void {
    this.themeService.isDarkTheme.subscribe(b => this.isDarkTheme = b);
    if (localStorage.getItem("userToken"))
    {
      this.loggedIn = true;
      this.playersService.getUserPlayers().subscribe(l => this.playersList = l);
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

  switchPlayer(player: Player): void {
    if (!player.isCurrentPlayer) {
      this.tokenService.switch(player.id).subscribe(t => {
        localStorage.setItem("userToken", t.jwt);
        localStorage.setItem("expiresAt", t.expiration.toString());
        if (t.isPlaying) {
          //window.location.reload(); // TODO : utiliser un service de refresh pour appeler le ngOnInit de chaque composant sans recharger la page
          this.router.navigate(['/','home']);
        } else {
          this.router.navigate(['/','lobby']);
        }
      });
    }
  }

  switchBrightness(dark: boolean): void {
    this.themeService.setDarkTheme(dark);
  }
}
