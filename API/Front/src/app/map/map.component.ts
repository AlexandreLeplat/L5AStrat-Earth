import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MapsService, Map, MapTile } from '../services/maps.service';
import { CdkDragDrop } from '@angular/cdk/drag-drop';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { PlayersService } from '../services/players.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {

  currentMap: Map;
  mapsList: Map[];
  mapTiles: MapTile[][];
  selectedTile: MapTile;
  zoomedIn: boolean;
  @ViewChild("map") mapElement : ElementRef;

  constructor(private mapsService: MapsService, private playersService: PlayersService
      , private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer) {
    this.matIconRegistry.addSvgIcon('home', this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/torii.svg'));
    this.matIconRegistry.addSvgIcon('house', this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/house.svg'));
    this.matIconRegistry.addSvgIcon('tower', this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/tower.svg'));
    this.matIconRegistry.addSvgIcon('castle', this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/castle.svg'));
  }

  ngOnInit(): void {
    this.mapsService.getMapsList().subscribe(m => {
        this.mapsList = m;
        this.currentMap = m[0];
        this.mapsService.getMapTiles(this.currentMap.id).subscribe(t => this.mapTiles = t);
      });
      this.playersService.getCurrentPlayer().subscribe(p => {
        p.hasNewMap = false;
        this.playersService.updatePlayer(p).subscribe();
      });
    }

  onTileClick(tile: MapTile): void {
    if (this.zoomedIn && this.selectedTile && tile.id == this.selectedTile.id) {
      this.zoomOut();
    }
    else {
      this.mapElement.nativeElement.style.transform = "scale(2) translate(" + this.calculateTranslation(tile.x) + "%," + this.calculateTranslation(tile.y) + "%)";
      this.zoomedIn = true;
      this.selectedTile = tile;
    }
  }

  selectMap(): void {
    this.mapsService.getMapTiles(this.currentMap.id).subscribe(t => this.mapTiles = t);
    this.zoomOut();
  }

  zoomOut(): void {
    this.mapElement.nativeElement.style.transform = "scale(1)";
    this.zoomedIn = false;
  }

  calculateTranslation(i: number): number {
    return Math.max(Math.min((4 - i)  * 11, 25), -25);
  }
}
