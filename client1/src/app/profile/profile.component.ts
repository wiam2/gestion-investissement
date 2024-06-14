import { Component, OnInit, TemplateRef } from '@angular/core';
import { AuthService } from '../Services/AuthService.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Investisseur } from '../models/Investisseur.model';
import { Startup } from '../models/Startup.model';
import { InvestisseurService } from '../Services/InvestisseurService.service';
import { StartupService } from '../Services/StartupService.service';
import { modalService } from "../Services/modalService";
import { EditPosteModalService } from "../Services/EditPosteModalService";
import { DeleteModalService } from "../Services/DeleteModalService";
import { DeletePosteModalService } from "../Services/DeletePosteModalService";
import { ValidateModalSevice } from "../Services/ValidateModalSevice";
import { NotifModalService } from "../Services/NotifModalService";
import { PosteService } from '../Services/PosteService.service';
import { poste } from '../models/poste.model';
import { posteInv } from '../models/posteInv.model';
import { postestar } from '../models/postestar.model';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  role: string|null = '';
  id : string = '';
  investisseur : Investisseur = new Investisseur();
  startup : Startup = new Startup ();
  data : any ;
  Posts: any[] = [];




  constructor(private authservice: AuthService, private InvesService: InvestisseurService, private StartupService: StartupService, private router: Router, private route: ActivatedRoute, private modalService: modalService,
    private modalPosteService: EditPosteModalService,
    private modalDeleteProfile: DeleteModalService,
    private DeletemodalPosteService: DeletePosteModalService,
    private ValidatePosteService: ValidateModalSevice,
    private NotifService: NotifModalService, private postService: PosteService) { }


  ngOnInit(): void {

    this.id = this.route.snapshot.params['id'];
    if (this.authservice.currentUserRole() === "RInvestisseur") {
      console.log(this.authservice.currentUserRole())
      this.role = 'Invest';
      this.InvesService.GetProfileInv(this.id).subscribe(data => {
        this.investisseur = data;

        console.log(data);
        this.loadPost();
      }, error => console.log(error));
                    this.loadPost();



    } else {
      this.StartupService.GetProfileStartup(this.id).subscribe(data => {
        this.startup = data;
        this.role = "Start";
        console.log(data);
        this.loadPost();
      }, error => console.log(error));
      this.loadPost();

    }


  }
  getPostImageUrl(posteInv: any): string {
    if (posteInv && posteInv.image) {
      return 'data:image/jpeg;base64,' + posteInv.image;
    }
    return 'assets/startups.png'; // Image par défaut si aucune image n'est disponible
  }
  // getPostImageUrlStar(posteStar: postestar): string {
  //   if (posteStar && posteStar.image) {
  //     return 'data:image/jpeg;base64,' + posteStar.image;
  //   }
  //   return 'assets/startups.png'; // Image par défaut si aucune image n'est disponible
  // }

  loadPost() {
    if (this.role == 'Invest') {
      this.postService.getPostsInv(this.id).subscribe(
        (data) => {
          console.log(data);
          this.Posts = data;
        },
        error => {
          console.log(error);
        }
      );
    } else {
      this.postService.getPostsStar(this.id).subscribe(
        (data) => {
          console.log(data);
          this.Posts = data;
        },
        error => {
          console.log(error);
        }
      );
    }
  }
  // getHoursElapsed(postDate: Date): string {
  //   const now = new Date(); // Date actuelle
  //   const timeDiff = Math.abs(now.getTime() - postDate.getTime()); // Différence de temps en millisecondes

  //   // Convertir la différence en heures
  //   const hoursDiff = Math.floor(timeDiff / (1000 * 60 * 60));

  //   // Retourner la chaîne formatée
  //   return `${hoursDiff} heures avant`;
  // }



    openModal(modalTemplate: TemplateRef<any>) {
        this.id = this.route.snapshot.params['id'];
        if (this.authservice.currentUserRole() === "RInvestisseur") {
            this.data = this.investisseur;
        } else {
            this.data = this.startup;
        }
        this.modalService
            .open(modalTemplate, this.id, this.data)
            .subscribe((action) => {
                console.log('modalAction', action);
            });
    }

    openModalPoste(modalPosteTemplate: TemplateRef<any>, poste?: any) {
        this.modalPosteService
            .open(modalPosteTemplate, poste)
            .subscribe((action) => {
                console.log('modalAction', action);
            });
    }

    openDeleteModal(modalDeleteTemplate: TemplateRef<any>) {
        this.modalDeleteProfile
            .open(modalDeleteTemplate)
            .subscribe((action) => {
                console.log('modalAction', action);
            });
    }

    openModalPosteDelete(modalPosteTemplate: TemplateRef<any>, idPoste: number) {
        this.DeletemodalPosteService
            .open(modalPosteTemplate, idPoste)
            .subscribe((action) => {
                console.log('modalAction', action);
            });
    }

    openModalPosteValidate(modalPosteTemplate: TemplateRef<any>, idPoste: number) {
        this.ValidatePosteService
            .open(modalPosteTemplate, idPoste)
            .subscribe((action) => {
                console.log('modalAction', action);
            });
    }





}
