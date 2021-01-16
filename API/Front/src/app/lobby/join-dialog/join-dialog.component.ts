import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Campaign } from 'src/app/services/campaigns.service';
import { Player, PlayersService } from 'src/app/services/players.service';

@Component({
  selector: 'app-join-dialog',
  templateUrl: './join-dialog.component.html',
  styleUrls: ['./join-dialog.component.css']
})
export class JoinDialogComponent implements OnInit {

  colors: string[] = [
    "blue", "#35a",
    "green", "#484",
    "cyan", "#0cf",
    "mediumpurple", "mediumorchid",
    "yellow", "gold",
    "darkorange", "orangered",
    "red", "crimson",
    "lime", "greenyellow",
    "indigo", "saddlebrown", "olive", "indianred", "teal", "rosybrown"
  ]
  
  campaign: Campaign;
  newPlayer: Player = {
    id: 0,
    assets: null,
    campaignId: 0,
    color: "",
    hasNewMap: false,
    status: 0,
    isCurrentPlayer: false,
    name: "",
    userId: 0,
    campaignName: "",
    userName: ""
  }

  constructor(public dialogRef: MatDialogRef<JoinDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: Campaign
        , private playersService: PlayersService, private snackBar: MatSnackBar) 
  { 
    this.campaign = data;
    this.newPlayer.campaignName = data.name;
  }

  ngOnInit(): void { 
    this.playersService.getCampaignPlayers(this.campaign.id).subscribe(l => {
      l.forEach(p => {
        var index: number = this.colors.indexOf(p.color);
        this.colors.splice(index, 1);
      });
    });
    this.playersService.getRandomNewPlayer(this.campaign.id).subscribe(p => {
      this.newPlayer = p;
    });
  }

  reroll(): void {
    this.playersService.getRandomNewPlayer(this.campaign.id).subscribe(p => {
      this.newPlayer = p;
    });
  }

  create(): void {
    this.playersService.createPlayer(this.newPlayer).subscribe(p => {
      this.snackBar.open("Vous avez rejoint la campagne", "", { duration : 2500 });
      this.dialogRef.close(p);
    }, err => {
      if (err.status >= 400 && err.status < 500) this.snackBar.open(err.error, "", { duration : 2500 });
      else this.snackBar.open("Une erreur s'est produite", "", { duration : 2500 });
    });
  }
}
