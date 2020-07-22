import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CustomMaterialModule } from './material/material.module';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './user/user.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { MapComponent } from './map/map.component';
import { InboxComponent } from './inbox/inbox.component';
import { MenuComponent } from './menu/menu.component';
import { OrdersFormComponent } from './ordersform/ordersform.component';
import { InfoComponent } from './info/info.component';
import { RulesComponent } from './rules/rules.component';
import { ThemeService } from './services/theme.service'
import { AuthenticationService } from './services/authentication.service';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';

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
    RulesComponent
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
    ThemeService, 
    AuthenticationService, 
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
