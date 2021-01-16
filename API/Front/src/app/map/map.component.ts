import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MapsService, Map, MapTile } from '../services/maps.service';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { Player, PlayersService, PlayerStatus } from '../services/players.service';
import { ActionType, OrdersService, OrdersSheet, Order, OrderStatus, OrdersSheetStatus } from '../services/orders.service';
import { OptionsService } from '../services/options.service';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { MatSnackBar } from '@angular/material/snack-bar';
import { I18nPluralPipe } from '@angular/common';
import { CampaignsService } from '../services/campaigns.service';

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
  actionTypes: { [id: number]: ActionType };
  actionMode: boolean;
  selectedAction: number;
  ordersSheet: OrdersSheet;
  orders: Order[];
  newOrder: Order;
  optionsList: { [inputLabel: string]: { [label: string]: string; }; } = {};
  selectableTilesIds: number[] = [];
  selectedParameterTileId: number;
  playersList: Player[] = [];

  @ViewChild("zoomableMap") mapElement : ElementRef;

  constructor(private mapsService: MapsService, private playersService: PlayersService, private ordersService: OrdersService
      , private optionsService: OptionsService, private campaignsService: CampaignsService
      , private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer, private snackBar: MatSnackBar) {
    this.matIconRegistry.addSvgIcon('home', this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/torii.svg'));
    this.matIconRegistry.addSvgIcon('house', this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/house.svg'));
    this.matIconRegistry.addSvgIcon('tower', this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/tower.svg'));
    this.matIconRegistry.addSvgIcon('castle', this.domSanitizer.bypassSecurityTrustResourceUrl('../../assets/castle.svg'));
  }

  ngOnInit(): void {
    this.campaignsService.getCurrentCampaign().subscribe(c => this.campaignsService.workOnCampaign(c.id).subscribe(i => {
      this.mapsService.getMapsList().subscribe(m => {
        this.mapsList = m;
        this.currentMap = m[0];
        this.mapsService.getMapTiles(this.currentMap.id).subscribe(t => {
          this.mapTiles = t
          this.ordersService.getCurrentOrdersSheet().subscribe(s => {
            this.ordersSheet = s;
            this.ordersService.getOrders(this.ordersSheet.id).subscribe(o => {
              this.orders = o;
            });
            this.ordersService.getActionTypes().subscribe(a => {
              this.actionTypes = {};
              a.forEach(t => {
                this.actionTypes[t.id] = t;
                if (s.turn == this.currentMap.turn && s.status == OrdersSheetStatus.Writing) {
                  this.intializeActions(t);
                }
              });
            });
          });
        });
      });
      this.playersService.getCurrentPlayer().subscribe(p => {
        p.hasNewMap = false;
        this.playersService.updatePlayer(p).subscribe();
      });
    }));
    this.playersService.getUserPlayers().subscribe(l => {
      this.playersList = [];
      l.forEach(p => {
        if (p.status > PlayerStatus.none) this.playersList.push(p);
      });
    });
  }

  onTileClick(tile: MapTile): void {
    if (this.actionMode) {
      if (this.selectableTilesIds.indexOf(tile.id) > -1) {
        this.selectedParameterTileId = tile.id;
        var input = this.actionTypes[this.newOrder.actionTypeId].form.find(input => input.isSelectableOnMap);
        this.newOrder.parameters[input.label] = tile.id.toString() + ";" + tile.name;
        this.updateOrder(this.newOrder);
      }
      return;
    }
  
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
    this.mapsService.getMapTiles(this.currentMap.id).subscribe(t => {
      this.mapTiles = t
      if (this.ordersSheet.turn == this.currentMap.turn && this.ordersSheet.status == OrdersSheetStatus.Writing) {
        Object.keys(this.actionTypes).forEach(k => {
          this.intializeActions(this.actionTypes[k]);
        });
      }
    });
    this.zoomOut();
  }

  intializeActions(actionType: ActionType): void {
    actionType.form.forEach(input => {
      if (input.isSelectedTileOnMap || input.isPredefinedOnMap) {
        this.optionsService.getOptions(input.type, null).subscribe(l => {
          for (let key in l) {
            let paramId = l[key].split(";")[0];
            this.mapTiles.forEach(row => row.forEach(tile => {
              if (!tile.parameters) {
                tile.parameters = {};
              }
              if ((input.isSelectedTileOnMap && tile.id.toString() == paramId)
                || (input.isPredefinedOnMap && tile.parameters[input.type] && tile.parameters[input.type].split(";")[0] == paramId)) {
                if (!tile.actions) {
                  tile.actions = {};
                }
                tile.actions[actionType.id] = l[key];
              }
            }));
          }
        });
      }
    });
  }

  selectAction(): void {
    var actionType: ActionType = this.actionTypes[this.selectedAction];
    this.selectedAction = null;

    if (this.orders.length >= this.ordersSheet.maxOrdersCount)
    {
      this.snackBar.open("Nombre maximal d'ordres atteint", "", { duration : 2500 })
      return;
    }
    var order: Order = {
      actionTypeId: actionType.id,
      comment: '',
      id: 0,
      parameters: {},
      rank: this.orders.length,
      selected: true,
      status: OrderStatus.None
    };
    actionType.form.forEach(input => {
      if (input.isSelectedTileOnMap) {
        order.parameters[input.label] = this.selectedTile.id.toString() + ";" + this.selectedTile.name;
      }
      if (input.isPredefinedOnMap) {
        order.parameters[input.label] = this.selectedTile.parameters[input.type];
      }
    });
    actionType.form.forEach(input => {
      if (input.isSelectableOnMap) {
        this.optionsService.getOptions(input.type, order.parameters[input.parameter]).subscribe(t => {
          for (let key in t) {
            let tileId = t[key].split(";")[0];
            this.selectableTilesIds.push(+tileId);
          }
          console.log(this.selectableTilesIds);
        });
      }
    });

    this.actionMode = true;
    this.ordersService.createOrder(this.ordersSheet.id, order).subscribe(o => {
      this.orders.push(o);
      this.newOrder = o;
      this.retrieveOptionsList();
      this.checkOrdersSheet();
    }, err => {
      this.snackBar.open("Erreur à la création d'ordre", "", { duration : 2500 })
    });
  }

  removeOrder(order: Order) {
    this.ordersService.deleteOrder(this.ordersSheet.id, order.id).subscribe(() => {
      var index: number = this.orders.indexOf(order);
      this.orders.splice(index, 1);
    });
    this.selectableTilesIds = [];
    this.selectedParameterTileId = null;
    this.actionMode = false;
    this.snackBar.open("Ordre annulé", "", { duration : 2500 })
  }

  validateOrder() {
    this.selectableTilesIds = [];
    this.selectedParameterTileId = null;
    this.actionMode = false;
    this.snackBar.open("Ordre validé", "", { duration : 2500 })
  }

  retrieveOptionsList() {
    this.orders.forEach(order => {    
      this.actionTypes[order.actionTypeId].form.forEach(input => {
        var parameterValue = "";
        if (input.parameter) {
          parameterValue = order.parameters[input.parameter];
        }
        this.optionsService.getOptions(input.type, parameterValue).subscribe(s => {
          if (!this.optionsList) {
            this.optionsList = {};
          }
          this.optionsList[input.label] = s;
        });
      });
    });
  }

  updateOrder(order: Order) {
    this.ordersService.updateOrder(this.ordersSheet.id, order).subscribe(o => {
      this.retrieveOptionsList();
      this.checkOrdersSheet();
    });
  }

  updateCheckbox(order: Order, label:string, event: MatCheckboxChange) {
    order.parameters[label] = event.checked.toString();
    this.updateOrder(order);
  }

  checkOrdersSheet() {
    if (this.newOrder) {
      this.ordersService.checkOrdersSheet().subscribe(ordersList => {
        this.orders = ordersList;
        this.newOrder = this.orders.find(o => o.id == this.newOrder.id);
      });
    }
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
