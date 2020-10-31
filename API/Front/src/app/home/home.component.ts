import { Component, OnInit } from '@angular/core';
import { CampaignsService, Campaign } from '../services/campaigns.service';
import { PlayersService, Player } from '../services/players.service';
import { ThemeService } from '../services/theme.service';
import { Observable } from 'rxjs';
import { GamesService, Game } from '../services/games.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  loadingGame: boolean;
  loadingCampaign: boolean;
  loadingPlayer: boolean;
  currentDate = new Date().getTime();
  isDarkTheme: Observable<boolean>;
  game: Game;
  campaign: Campaign;
  player: Player;
  
  constructor(private themeService: ThemeService, private playersService: PlayersService
    , private campaignsService: CampaignsService, private gamesService: GamesService) { }

  ngOnInit(): void {
    this.isDarkTheme = this.themeService.isDarkTheme;
    this.gamesService.getCurrentGame().subscribe(g => this.game = g);
    this.campaignsService.getCurrentCampaign().subscribe(c => 
      {
        this.campaign = c;
        this.campaignsService.workOnCampaign(c.id).subscribe(w => 
          {
            this.campaignsService.getCurrentCampaign().subscribe(c => this.campaign = c);
            this.playersService.getCurrentPlayer().subscribe(p => this.player = p);
          });
      });
    this.playersService.getCurrentPlayer().subscribe(p => this.player = p);
  }

  calculatePhaseProgress(campaign: Campaign): number {
    var phaseStart = new Date(campaign.nextPhase).getTime() - (campaign.phaseLength*60000);
    var progress = (this.currentDate - phaseStart) / (campaign.phaseLength*600)

    return progress;
  }
}
