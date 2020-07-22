import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { CdkDragDrop } from '@angular/cdk/drag-drop';

export interface Tile {
  color: string;
  text: string;
  x: number;
  y: number;
}
@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {

  zoomedIn: boolean;
  @ViewChild("map") mapElement : ElementRef;

  tiles: Tile[][] = [[
    {text: '', color: 'wheat', x: 25, y: 25},
    {text: '', color: 'linen', x: 25, y: 25},
    {text: '', color: 'wheat', x: 22, y: 25},
    {text: 'e', color: 'linen', x: 11, y: 25},
    {text: 'e', color: 'wheat', x: 0, y: 25},
    {text: 'e', color: 'linen', x: -11, y: 25},
    {text: '', color: 'wheat', x: -22, y: 25},
    {text: '', color: 'linen', x: -25, y: 25},
    {text: '', color: 'wheat', x: -25, y: 25}],
    [{text: '', color: 'linen', x: 25, y: 25},
    {text: 'A', color: 'wheat', x: 25, y: 25},
    {text: '', color: 'linen', x: 22, y: 25},
    {text: '', color: 'wheat', x: 11, y: 25},
    {text: '', color: 'linen', x: 0, y: 25},
    {text: '', color: 'wheat', x: -11, y: 25},
    {text: '', color: 'linen', x: -22, y: 25},
    {text: '', color: 'wheat', x: -25, y: 25},
    {text: '', color: 'linen', x: -25, y: 25}],
    [{text: '', color: 'wheat', x: 25, y: 22},
    {text: '', color: 'linen', x: 25, y: 22},
    {text: '', color: 'wheat', x: 22, y: 22},
    {text: '', color: 'linen', x: 11, y: 22},
    {text: '', color: 'wheat', x: 0, y: 22},
    {text: 'V', color: 'linen', x: -11, y: 22},
    {text: '', color: 'wheat', x: -22, y: 22},
    {text: '', color: 'linen', x: -25, y: 22},
    {text: '', color: 'wheat', x: -25, y: 22}],
    [{text: 'e', color: 'linen', x: 25, y: 11},
    {text: '', color: 'wheat', x: 25, y: 11},
    {text: 'V', color: 'linen', x: 22, y: 11},
    {text: '', color: 'wheat', x: 11, y: 11},
    {text: '', color: 'linen', x: 0, y: 11},
    {text: '', color: 'wheat', x: -11, y: 11},
    {text: '', color: 'linen', x: -22, y: 11},
    {text: '', color: 'wheat', x: -25, y: 11},
    {text: 'e', color: 'linen', x: -25, y: 11}],
    [{text: 'e', color: 'wheat', x: 25, y: 0},
    {text: '', color: 'linen', x: 25, y: 0},
    {text: '', color: 'wheat', x: 22, y: 0},
    {text: '', color: 'linen', x: 11, y: 0},
    {text: 'F', color: 'wheat', x: 0, y: 0},
    {text: '', color: 'linen', x: -11, y: 0},
    {text: '', color: 'wheat', x: -22, y: 0},
    {text: '', color: 'linen', x: -25, y: 0},
    {text: 'e', color: 'wheat', x: -25, y: 0}],
    [{text: 'e', color: 'linen', x: 25, y: -11},
    {text: '', color: 'wheat', x: 25, y: -11},
    {text: '', color: 'linen', x: 22, y: -11},
    {text: '', color: 'wheat', x: 11, y: -11},
    {text: '', color: 'linen', x: 0, y: -11},
    {text: '', color: 'wheat', x: -11, y: -11},
    {text: 'V', color: 'linen', x: -22, y: -11},
    {text: '', color: 'wheat', x: -25, y: -11},
    {text: 'e', color: 'linen', x: -25, y: -11}],
    [{text: '', color: 'wheat', x: 25, y: -22},
    {text: '', color: 'linen', x: 25, y: -22},
    {text: '', color: 'wheat', x: 22, y: -22},
    {text: 'V', color: 'linen', x: 11, y: -22},
    {text: '', color: 'wheat', x: 0, y: -22},
    {text: '', color: 'linen', x: -11, y: -22},
    {text: '', color: 'wheat', x: -22, y: -22},
    {text: '', color: 'linen', x: -25, y: -22},
    {text: '', color: 'wheat', x: -25, y: -22}],
    [{text: '', color: 'linen', x: 25, y: -25},
    {text: '', color: 'wheat', x: 25, y: -25},
    {text: '', color: 'linen', x: 22, y: -25},
    {text: '', color: 'wheat', x: 11, y: -25},
    {text: '', color: 'linen', x: 0, y: -25},
    {text: '', color: 'wheat', x: -11, y: -25},
    {text: '', color: 'linen', x: -22, y: -25},
    {text: 'A', color: 'wheat', x: -25, y: -25},
    {text: '', color: 'linen', x: -25, y: -25}],
    [{text: '', color: 'wheat', x: 25, y: -25},
    {text: '', color: 'linen', x: 25, y: -25},
    {text: '', color: 'wheat', x: 22, y: -25},
    {text: 'e', color: 'linen', x: 11, y: -25},
    {text: 'e', color: 'wheat', x: 0, y: -25},
    {text: 'e', color: 'linen', x: -11, y: -25},
    {text: '', color: 'wheat', x: -22, y: -25},
    {text: '', color: 'linen', x: -25, y: -25},
    {text: '', color: 'wheat', x: -25, y: -25}]
  ];
  constructor() { }

  ngOnInit(): void {
  }

  onTileClick(tile: Tile): void {
    this.mapElement.nativeElement.style.transform = "scale(2) translate(" + tile.x + "%," + tile.y + "%)";
    this.zoomedIn = true;
  }

  zoomOut(): void {
    this.mapElement.nativeElement.style.transform = "scale(1)";
    this.zoomedIn = false;
  }
}
