import { Component, Input, OnInit, SimpleChange } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ThemeService } from '../services/theme.service';
import { RulesComponent } from './rules/rules.component';
import { InfoComponent } from './info/info.component';
import { Player, PlayersService, PlayerStatus } from '../services/players.service';
import { TokenService } from '../services/token.service';
import { User, UsersService } from '../services/users.service';
import { EventEmitter } from 'events';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  @Input() playersList: Player[];
  loggedIn: boolean;
  isDarkTheme: boolean;
  currentUser: User;
  currentPlayer: Player;
  hasReadyNewPlayer: boolean;
  
  constructor(private router: Router, private themeService: ThemeService, public dialog: MatDialog
        , private playersService: PlayersService, private usersService: UsersService, private tokenService: TokenService) { }

  ngOnInit(): void {
    this.themeService.isDarkTheme.subscribe(b => this.isDarkTheme = b);
    if (localStorage.getItem("userToken"))
    {
      this.loggedIn = true;
      this.usersService.getCurrentUser().subscribe(u => {
        this.currentUser = u;
      })
    }
    this.playersList.forEach(p => {
      if (p.isCurrentPlayer) this.currentPlayer = p;
      if (p.status == PlayerStatus.ready) this.hasReadyNewPlayer = true;
    });
  }

  ngOnChanges(changes: SimpleChange): void {
    for(var prop in changes) {
      if (prop == "playersList" && changes[prop]) {
        this.playersList.forEach(p => {
          if (p.isCurrentPlayer) this.currentPlayer = p;
          if (p.status == PlayerStatus.ready) this.hasReadyNewPlayer = true;
        });
      }
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
    console.log(this.currentPlayer);
    if (this.currentPlayer != player) {
      var playerId = 0;
      if (player) {
        playerId = player.id;
      }
      console.log(playerId);
      this.tokenService.switch(playerId).subscribe(t => {
        localStorage.setItem("userToken", t.jwt);
        localStorage.setItem("userId", t.userId.toString());
        localStorage.setItem("expiresAt", t.expiration.toString());
        if (t.isPlaying) {
          this.currentPlayer = player;
          localStorage.setItem("isPlaying", "yes");
          this.router.navigate(['/','home']);
          window.location.reload(); // TODO : utiliser un service de refresh pour appeler le ngOnInit de chaque composant sans recharger la page
        } else {
          localStorage.removeItem("isPlaying");
          this.router.navigate(['/','lobby']);
        }
      });
    }
  }

  switchBrightness(dark: boolean): void {
    this.themeService.setDarkTheme(dark);
  }
}
