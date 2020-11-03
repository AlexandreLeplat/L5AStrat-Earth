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
  mapSize:number = 9;
  @ViewChild("zoomableMap") mapElement : ElementRef;

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
      var zoom = this.mapSize / 4.7;
      this.mapElement.nativeElement.style.transform = "scale(" + zoom + ") translate(" + this.calculateTranslation(tile.x) + "%," + this.calculateTranslation(tile.y) + "%)";
      this.selectedTile = tile;
      this.zoomedIn = true;
    }
  }

  selectMap(): void {
    this.mapsService.getMapTiles(this.currentMap.id).subscribe(t => this.mapTiles = t);
    this.zoomOut();
  }

  zoomOut(): void {
    this.mapElement.nativeElement.style.transform = "scale(1)";
    this.selectedTile = null;
    this.zoomedIn = false;
  }

  calculateTranslation(i: number): number {
    var step = 100 / this.mapSize;
    var limit = 50 - 235 / this.mapSize;
    return Math.max(Math.min(((this.mapSize - 1) / 2 - i) * step, limit), -limit);
  }
}
