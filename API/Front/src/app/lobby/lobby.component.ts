import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatIconRegistry } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DomSanitizer } from '@angular/platform-browser';
import { Campaign, CampaignsService } from '../services/campaigns.service';
import { Player, PlayersService, PlayerStatus } from '../services/players.service';
import { UsersService } from '../services/users.service';
import { CreateDialogComponent } from './create-dialog/create-dialog.component';
import { JoinDialogComponent } from './join-dialog/join-dialog.component';

@Component({
  selector: 'app-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.css']
})
export class LobbyComponent implements OnInit {

  newCampaignsList: Campaign[] = [];
  runningCampaignsList: Campaign[] = [];
  finishedCampaignsList: Campaign[] = [];
  playersLists: { [campaignId: number]: Player[] } = {};
  userId: number = 0;
  playersList: Player[] = [];

  campaignLengthLabels: { [length: number]: string } = {
    2: "Blitz (96 minutes)",
    5: "Express (4 heures)",
    720: "Rapide (24 jours)",
    1440: "Normal (48 jours)",
    2520: "Long (12 semaines)"
  };

  constructor(private campaignsService: CampaignsService, private playersService: PlayersService, private matIconRegistry: MatIconRegistry
      , private usersService: UsersService, private domSanitizer: DomSanitizer, public dialog: MatDialog, private snackBar: MatSnackBar) {
    this.matIconRegistry.addSvgIcon('earth', this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/earth.svg'));
  }

  ngOnInit(): void {
    this.userId = +localStorage.getItem("userId");
    this.campaignsService.getCampaigns().subscribe(l => {
      l.forEach(c => {
        if (c.status == 1) this.newCampaignsList.push(c);
        else if (c.status == 4) this.finishedCampaignsList.push(c);
        else this.runningCampaignsList.push(c);
        this.playersService.getCampaignPlayers(c.id).subscribe(p => {
          this.playersLists[c.id] = p;
        })
      });
    });
    this.playersService.getUserPlayers().subscribe(l => {
      this.playersList = [];
      l.forEach(p => {
        if (p.status > PlayerStatus.none) this.playersList.push(p);
      });
    });
  }

  join(campaign: Campaign): void {
    var dialogRef = this.dialog.open(JoinDialogComponent, { data: campaign });
    dialogRef.afterClosed().subscribe(result => {
      if(result) {
        this.playersLists[campaign.id].push(result);
        this.usersService.getCurrentUser().subscribe(u => {
          result.userName = u.name;
        })
      }
    });
  }

  createCampaign(): void {
    var dialogRef = this.dialog.open(CreateDialogComponent);
    dialogRef.afterClosed().subscribe(result => {
      if(result) {
        this.newCampaignsList.push(result);
        this.playersLists[result.id] = [];
      }
    });
  }

  isCampaignFull(campaign: Campaign): boolean {
    return (this.playersLists[campaign.id].length >= 4);
  }

  isUserInCampaign(campaign: Campaign): boolean {
    var found = false;
    this.playersLists[campaign.id].forEach(p => {
      if (p.userId == this.userId) {
        console.log(p.userId);
        found = true;
      } 
    });
    return found;
  }

  launch(campaign: Campaign): void {
    this.campaignsService.startCampaign(campaign.id).subscribe(() => {
      this.snackBar.open("Campagne lancée !", "", { duration : 2500 });
      var index: number = this.newCampaignsList.indexOf(campaign);
      this.newCampaignsList.splice(index, 1);
      this.runningCampaignsList.push(campaign);
      this.playersService.getUserPlayers().subscribe(l => {
        this.playersList = [];
        l.forEach(p => {
          if (p.status > PlayerStatus.none) this.playersList.push(p);
        });
      });
    }, err => {
      console.log(err);
      if (err.status >= 400 && err.status < 500) this.snackBar.open(err.error, "", { duration : 2500 });
      else this.snackBar.open("Une erreur s'est produite", "", { duration : 2500 });
    });
  }

  removePlayer(player: Player): void {
    this.playersService.deletePlayer(player.id).subscribe(() => {
      this.snackBar.open("Joueur supprimé", "", { duration : 2500 });
      var index: number = this.playersLists[player.campaignId].indexOf(player);
      this.playersLists[player.campaignId].splice(index, 1);
    }, err => {
      console.log(err);
      if (err.status >= 400 && err.status < 500) this.snackBar.open(err.error, "", { duration : 2500 });
      else this.snackBar.open("Une erreur s'est produite", "", { duration : 2500 });
    });
  }

  removeCampaign(campaign: Campaign): void {
    this.campaignsService.removeCampaign(campaign.id).subscribe(() => {
      this.snackBar.open("Campagne supprimée", "", { duration : 2500 });
      var index: number = this.newCampaignsList.indexOf(campaign);
      this.newCampaignsList.splice(index, 1);
  }, err => {
      console.log(err);
      if (err.status >= 400 && err.status < 500) this.snackBar.open(err.error, "", { duration : 2500 });
      else this.snackBar.open("Une erreur s'est produite", "", { duration : 2500 });
    });
  }
}
