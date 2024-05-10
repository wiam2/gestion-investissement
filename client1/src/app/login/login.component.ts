import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { user } from '../models/user.model';
import { AuthService } from '../Services/AuthService.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  message: string = '';
  User: user = new user();
  authenticatedUser!: user | null;
  constructor(private authservice: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.authservice.logout();
  }
  forget(password: any) {
    console.log(password);
    this.authservice.forgetPassword(password).subscribe(
      data => {
        console.log(data); // Afficher la réponse du serveur en cas de succès
      },
      error => {
        console.error('erreur', error); // Afficher l'erreur en cas d'échec
      }
    );
  }
  login() {
    const credentials = { email: this.User.email, password: this.User.password };

    this.authservice.login(credentials).subscribe({
      next: (response) => {
        // Récupérer l'utilisateur authentifié
        this.authenticatedUser = this.authservice.currentUser();

        // Vérifier si l'utilisateur est authentifié avant de naviguer vers la page
        if (this.authenticatedUser) {
          console.log(this.authenticatedUser);
          this.router.navigate(['/page']);
        } else {
          // Gérer le cas où l'utilisateur n'est pas authentifié
          this.message = "L'identifiant utilisateur ou le mot de passe est incorrect.";
        }
      },
      error: (err: any) => {
        // Gérer les erreurs de l'authentification
        this.message = "Une erreur s'est produite lors de l'authentification. Veuillez réessayer plus tard.";
        console.error("Erreur lors de l'authentification :", err);
      }
    });
  }



  redirectToSignup() {
    this.router.navigate(['/signup']);
  }




}
