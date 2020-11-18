import { Component, OnInit, Input } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from "@angular/platform-browser";
import { Router } from '@angular/router';
import { MessagesService } from '../services/messages.service';
import { PlayersService } from '../services/players.service';

export interface MenuItem {
  label: string;
  route: string;
  badge: string;
}
@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  @Input() selectedRoute: string;
  menuItems: MenuItem[] =
  [
    { label: 'Situation', route: 'home', badge: '' },
    { label: 'Carte', route: 'map', badge: '' },
    { label: 'Ordres', route: 'orders', badge: '' },
    { label: 'Messages', route: 'inbox', badge: '' }
  ];

  constructor(private router: Router, private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer
      , private playersService: PlayersService, private messagesService: MessagesService) 
  {
    this.matIconRegistry.addSvgIcon('home', this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/torii.svg'));
    this.matIconRegistry.addSvgIcon('map', this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/mark.svg'));
    this.matIconRegistry.addSvgIcon('orders', this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/tessen.svg'));
    this.matIconRegistry.addSvgIcon('inbox', this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/message.svg'));
  }

  ngOnInit(): void {
    this.playersService.getCurrentPlayer().subscribe(p => {
        if (p.hasNewMap) this.menuItems[1].badge = '!';
      });

    this.messagesService.getMessageCount("unread").subscribe(c => {
        if (c > 0) this.menuItems[3].badge = c.toString();
      });
  }

  navigateToRoute(route: string) {
    this.router.navigate(['/', route])
  }
}
