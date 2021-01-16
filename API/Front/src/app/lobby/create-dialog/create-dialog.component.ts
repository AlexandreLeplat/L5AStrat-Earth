import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Campaign, CampaignsService } from 'src/app/services/campaigns.service';

export interface CampaignLengthOption {
  label: string,
  phaseLengthLabel: string,
  phaseLength: number
}
@Component({
  selector: 'app-create-dialog',
  templateUrl: './create-dialog.component.html',
  styleUrls: ['./create-dialog.component.css']
})
export class CreateDialogComponent implements OnInit {

  newCampaign: Campaign = { 
    id:0,
    name:"",
    nextPhase: new Date(),
    phaseLength: 1440,
    assets:null,
    currentPhase:0,
    currentTurn:0,
    status:0,
    gameId:1,
    creatorId:0
  };
  
  lengthOptions: CampaignLengthOption[] = [
    { label: "Blitz (96 minutes)", phaseLengthLabel: "2 minutes", phaseLength: 2 },
    { label: "Express (4 heures)", phaseLengthLabel: "5 minutes", phaseLength: 5 },
    { label: "Rapide (24 jours)", phaseLengthLabel: "12 heures", phaseLength: 720 },
    { label: "Normal (48 jours)", phaseLengthLabel: "24 heures", phaseLength: 1440 },
    { label: "Long (12 semaines)", phaseLengthLabel: "42 heures", phaseLength: 2520 }
  ];
  selectedLength: CampaignLengthOption = { label: "Normal (48 jours)", phaseLengthLabel: "24 heures", phaseLength: 1440 };

  startOptions = [ 
    { label:"00h", value:0 },
    { label:"01h", value:1 },
    { label:"02h", value:2 },
    { label:"03h", value:3 },
    { label:"04h", value:4 },
    { label:"05h", value:5 },
    { label:"06h", value:6 },
    { label:"07h", value:7 },
    { label:"08h", value:8 },
    { label:"09h", value:9 },
    { label:"10h", value:10 },
    { label:"11h", value:11 },
    { label:"12h", value:12 },
    { label:"13h", value:13 },
    { label:"14h", value:14 },
    { label:"15h", value:15 },
    { label:"16h", value:16 },
    { label:"17h", value:17 },
    { label:"18h", value:18 },
    { label:"19h", value:19 },
    { label:"20h", value:20 },
    { label:"21h", value:21 },
    { label:"22h", value:22 },
    { label:"23h", value:23 }
  ];

  constructor(public dialogRef: MatDialogRef<CreateDialogComponent>, 
        private campaignsService: CampaignsService, private snackBar: MatSnackBar) { }

  ngOnInit(): void {
  }

  onSelectStartHour(hour: number): void {
    this.newCampaign.nextPhase.setHours(hour, 0, 0, 0);
  }

  onSelectDuration(): void {
    this.newCampaign.phaseLength = this.selectedLength.phaseLength;
  }

  create(): void {
    this.campaignsService.createCampaign(this.newCampaign).subscribe(c => {
      console.log(c);
      this.snackBar.open("La campagne a été créée avec succès", "", { duration : 2500 });
      this.dialogRef.close(c);
    }, err => {
      console.log(err);
      if (err.status >= 400 && err.status < 500) this.snackBar.open(err.error, "", { duration : 2500 });
      else this.snackBar.open("Une erreur s'est produite", "", { duration : 2500 });
    });
  }
}
