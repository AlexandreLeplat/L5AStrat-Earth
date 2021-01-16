import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TokenService } from '../services/token.service';
import { User, UsersService } from '../services/users.service';

@Component({
  selector: 'app-subscribe',
  templateUrl: './subscribe.component.html',
  styleUrls: ['./subscribe.component.css']
})
export class SubscribeComponent implements OnInit {

  newUserName: string = "";
  newPassword: string = "";
  subscriptionKey: string = "";
  nameError: string = "Champ obligatoire";
  passwordError: string = "Champ obligatoire";
  keyError: string = "Champ obligatoire";

  constructor(private userService: UsersService, private tokenService: TokenService, private router: Router) { }

  ngOnInit(): void {
    if (localStorage.getItem("userToken")) {
      if (localStorage.getItem("isPlaying"))
      {
        this.router.navigate(['/','home']);
      } else {
        this.router.navigate(['/','lobby']);
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/','login']);
  }

  subscribe(): void {
    var newUser: User = {
      id:0,
      name: this.newUserName,
      password: this.newPassword,
      role:0
    };
    this.userService.createUser(newUser, this.subscriptionKey).subscribe(u => {
      this.login();
    }, error => {
      this.nameError = "Champ obligatoire";
      this.passwordError = "Champ obligatoire";
      this.keyError = "Champ obligatoire";
      if (error.status == 400)
      {
        this.nameError = "Nom d'utilisateur ou mot de passe incorrect";
        this.passwordError = "Nom d'utilisateur ou mot de passe incorrect";
        this.newUserName = "";
        this.newPassword = "";
      }
      else if (error.status == 401)
      {
        this.keyError = "Réponse incorrecte";
        this.subscriptionKey = "";
    }
      else if (error.status == 412)
      {
        this.nameError = "Nom d'utilisateur déjà utilisé";
        this.newUserName = "";
      }
      else
      {
        this.passwordError = "Erreur technique à l'inscription";
        this.newPassword = "";
      }
  });
  }

  private login(): void {
    var token = this.tokenService.login(this.newUserName, this.newPassword);
    token.subscribe({
      next: data => { 
        localStorage.setItem("userToken", data.jwt);
        localStorage.setItem("userId", data.userId.toString());
        localStorage.setItem("expiresAt", data.expiration.toString());
        localStorage.removeItem("isPlaying");
        this.router.navigate(['/','lobby']);
      },
      error: error => {
        this.passwordError = "Champ obligatoire";
        if (error.status == 401)
        {
          this.passwordError = "Informations de connexion incorrectes";
          this.newPassword = "";
        }
        else
        {
          this.passwordError = "Erreur technique à l'authentification";
          this.newPassword = "";
        }
      } 
    });
  }
}
