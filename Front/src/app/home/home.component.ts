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
  game$: Observable<Game>;
  campaign$: Observable<Campaign>;
  player$: Observable<Player>;
  
  constructor(private themeService: ThemeService, private playersService: PlayersService
    , private campaignsService: CampaignsService, private gamesService: GamesService) { }

  ngOnInit(): void {

    this.game$ = this.gamesService.getCurrentCampaign();
    this.campaign$ = this.campaignsService.getCurrentCampaign();
    this.player$ = this.playersService.getCurrentPlayer();
    this.isDarkTheme = this.themeService.isDarkTheme;
  }

  calculatePhaseProgress(campaign: Campaign): number {
    var phaseStart = new Date(campaign.nextPhase).getTime() - (campaign.phaseLength*3600000);
    var progress = (this.currentDate - phaseStart) / (campaign.phaseLength*36000)

    return progress;
  }
}
