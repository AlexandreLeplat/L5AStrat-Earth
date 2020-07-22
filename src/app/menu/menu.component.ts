import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';

export interface MenuItem {
  label: string;
  route: string;
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
    { label: 'Situation', route: 'home' },
    { label: 'Carte', route: 'map' },
    { label: 'Ordres', route: 'orders' },
    { label: 'Messages', route: 'inbox' }
  ];

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  navigateToRoute(route: string) {
    this.router.navigate(['/', route])
  }

}
