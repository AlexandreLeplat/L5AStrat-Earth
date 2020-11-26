import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CustomMaterialModule } from './material/material.module';
import { HttpClientModule } from '@angular/common/http';
import { registerLocaleData } from '@angular/common';
import localeFr from '@angular/common/locales/fr';

// Components
import { LoginComponent } from './login/login.component';
import { UserComponent } from './user/user.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { MapComponent } from './map/map.component';
import { InboxComponent } from './inbox/inbox.component';
import { MenuComponent } from './menu/menu.component';
import { OrdersFormComponent } from './ordersform/ordersform.component';
import { InfoComponent } from './header/info/info.component';
import { RulesComponent } from './header/rules/rules.component';

// Services
import { AuthenticationService } from './services/authentication.service';
import { CampaignsService } from './services/campaigns.service'
import { GamesService } from './services/games.service'
import { MapsService } from './services/maps.service'
import { MessagesService } from './services/messages.service'
import { OrdersService } from './services/orders.service'
import { PlayersService } from './services/players.service'
import { OptionsService } from './services/options.service'
import { ThemeService } from './services/theme.service'
import { TokenService } from './services/token.service'
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { MessageComponent } from './inbox/message/message.component';
import { MessagehistoryComponent } from './inbox/messagehistory/messagehistory.component';
import { LobbyComponent } from './lobby/lobby.component';
import { ConfirmModalComponent } from './ordersform/confirm-modal/confirm-modal.component';

registerLocaleData(localeFr, 'fr');

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UserComponent,
    HeaderComponent,
    HomeComponent,
    MapComponent,
    InboxComponent,
    MenuComponent,
    OrdersFormComponent,
    InfoComponent,
    RulesComponent,
    MessageComponent,
    MessagehistoryComponent,
    LobbyComponent,
    ConfirmModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    CustomMaterialModule,
    FlexLayoutModule,
    HttpClientModule
  ],
  providers: [
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    { provide: LOCALE_ID, useValue: "fr" },
    AuthenticationService, 
    JwtHelperService,
    CampaignsService,
    GamesService,
    MapsService,
    MessagesService,
    OrdersService,
    PlayersService,
    OptionsService,
    ThemeService, 
    TokenService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
