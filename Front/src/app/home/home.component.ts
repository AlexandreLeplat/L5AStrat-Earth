import { Component, OnInit } from '@angular/core';
import { CampaignsService, Campaign } from '../services/campaigns.service';
import { PlayersService, Player } from '../services/players.service';
import { ThemeService } from '../services/theme.service';
import { Observable } from 'rxjs';
import { GamesService, Game } from '../services/games.service';
import { GridColumnStyleBuilder } from '@angular/flex-layout/grid/typings/column/column';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  loadingGame: boolean;
  loadingCampaign: boolean;
  loadingPlayer: boolean;
  isDarkTheme: Observable<boolean>;
  game: Observable<Game>;
  campaign: Observable<Campaign>;
  player: Observable<Player>;
  
  constructor(private themeService: ThemeService, private playersService: PlayersService
    , private campaignsService: CampaignsService, private gamesService: GamesService) { }

  ngOnInit(): void {
    this.loadingGame = true;
    this.loadingCampaign = true;
    this.loadingPlayer = true;

    this.game = this.gamesService.getCurrentCampaign();
    this.game.subscribe( o => {
      console.log(o);
      this.loadingGame = false;
    })

    this.campaign = this.campaignsService.getCurrentCampaign();
    this.campaign.subscribe( () => {
      this.loadingCampaign = false;
    })

    this.player = this.playersService.getCurrentPlayer();
    this.player.subscribe( () => {
      this.loadingPlayer = false;
    })

    this.isDarkTheme = this.themeService.isDarkTheme;
  }

  calculatePhaseProgress(campaign: Campaign): number {
    var phaseStart = new Date(campaign.nextPhase).getTime() - (campaign.phaseLength*3600000);
    var currentDate = new Date().getTime();
    var progress = (currentDate - phaseStart) / (campaign.phaseLength*36000)

    return progress;
  }
}
