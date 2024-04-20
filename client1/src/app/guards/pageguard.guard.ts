import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../Services/AuthService.service';

@Injectable({
  providedIn: 'root'
})
export class PageGuard implements CanActivate {

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  canActivate(): boolean {
   /* const currentUser = this.authService.currentUser();*/
    if (this.authService.isAuthenticated()) {
      
      return true;
    } else {
      // Redirige vers une page non autorisée ou gère l'accès d'une autre manière
      this.router.navigate(['/login']); // Exemple de redirection vers une page d'autorisation refusée
      return false;
    }
  }
}
