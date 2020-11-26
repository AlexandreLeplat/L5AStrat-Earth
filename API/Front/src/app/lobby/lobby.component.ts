import { Component, OnInit } from '@angular/core';
import { Campaign, CampaignsService } from '../services/campaigns.service';

@Component({
  selector: 'app-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.css']
})
export class LobbyComponent implements OnInit {

  campaignsList: Campaign[];

  constructor(private campaignsService: CampaignsService) {}

  ngOnInit(): void {
    this.campaignsService.getCampaigns(2).subscribe(c => this.campaignsList = c);
  }

}
