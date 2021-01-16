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
import { SubscribeComponent } from './subscribe/subscribe.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'subscribe', component: SubscribeComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthenticationService], data: { isPlaying: true } },
  { path: 'inbox', component: InboxComponent, canActivate: [AuthenticationService], data: { isPlaying: true } },
  { path: 'map', component: MapComponent, canActivate: [AuthenticationService], data: { isPlaying: true } },
  { path: 'orders', component: OrdersFormComponent, canActivate: [AuthenticationService], data: { isPlaying: true } },
  { path: 'user', component: UserComponent, canActivate: [AuthenticationService], data: { isPlaying: true } },
  { path: 'lobby', component: LobbyComponent, canActivate: [AuthenticationService], data: { isPlaying: false } },
  { path : '', component : LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
