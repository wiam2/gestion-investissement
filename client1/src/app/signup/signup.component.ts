import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { Investisseur } from '../models/Investisseur.model';
import { user } from '../models/user.model';
import { Router } from '@angular/router';
import { InvestisseurService } from '../Services/InvestisseurService.service';
import { Startup } from '../models/Startup.model';
import { StartupService } from '../Services/StartupService.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',

  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  parentHeight: string = '150vh'; // Hauteur par défaut

  onHeightChange(event: any) {
    this.parentHeight = 'auto';
  }

  choix: string | null = null;
  User: user = new user();
  Investisseur: Investisseur = new Investisseur();
  saveSuccess: boolean = false;
  Startup: Startup = new Startup();

  constructor(private invesstisseurService: InvestisseurService, private startupservice: StartupService, private router: Router) {
  }

  ngOnInit(): void {

    this.Investisseur.email = this.User.email;
    this.Investisseur.password = this.User.password;
    this.Investisseur.confirmPassword = this.User.confirmPassword;

  }
  CreateInvestisseur() {
    this.invesstisseurService.CreateInvestisseur(this.Investisseur).subscribe(
      data => {
        console.log(data);
        this.saveSuccess = true;
        this.router.navigate(['/login']);
      },
      error => {
        console.log(error);
      }
    )
  }
  CreateStarup() {
    this.startupservice.CreateStartup(this.Startup).subscribe(
      data => {
        console.log(data);
        this.saveSuccess = true;
      },
      error => {
        console.log(error);
      }
    )
  }


  onSubmit() {
    this.Investisseur.email = this.User.email;
    this.Investisseur.password = this.User.password;
    this.Investisseur.confirmPassword = this.User.confirmPassword;

    console.log(this.Investisseur)
    this.CreateInvestisseur();

  }
  onSubmit1() {
    this.Startup.email = this.User.email;
    this.Startup.password = this.User.password;
    this.Startup.confirmPassword = this.User.confirmPassword;

    console.log(this.Startup)
    this.CreateStarup();

  }

  redirectToHome() {
    this.router.navigate(['/']);
  }
}
