import { NgModule } from '@angular/core';
import { Routes, CanActivate, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { InboxComponent } from './inbox/inbox.component';
import { LoginComponent } from './login/login.component';
import { MapComponent } from './map/map.component';
import { OrdersFormComponent } from './ordersform/ordersform.component';
import { UserComponent } from './user/user.component';
import { AuthenticationService } from './services/authentication.service';
import { LobbyComponent } from './lobby/lobby.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthenticationService] },
  { path: 'inbox', component: InboxComponent, canActivate: [AuthenticationService] },
  { path: 'map', component: MapComponent, canActivate: [AuthenticationService] },
  { path: 'orders', component: OrdersFormComponent, canActivate: [AuthenticationService] },
  { path: 'user', component: UserComponent, canActivate: [AuthenticationService] },
  { path: 'lobby', component: LobbyComponent, canActivate: [AuthenticationService] },
  { path : '', component : LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
