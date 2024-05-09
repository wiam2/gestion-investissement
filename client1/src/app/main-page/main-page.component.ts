import { Component } from '@angular/core';
import { poste } from '../models/poste.model';
import { OnInit } from '@angular/core';
import { AuthService } from '../Services/AuthService.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PosteService } from '../Services/PosteService.service';
import { posteInv } from '../models/posteInv.model';
import { postestar } from '../models/postestar.model';
import { user } from '../models/user.model';
@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent  implements OnInit{
  posteInv : posteInv = new posteInv();
  posteStar : postestar=new postestar();
  role : string="";
  selectedFile: File | null = null;

  constructor(private authservice:AuthService ,private router:Router ,  private route: ActivatedRoute,private posteService: PosteService) {}
  ngOnInit(): void {
   
    if(this.authservice.currentUserRole()==="RInvestisseur"){
      console.log(this.authservice.currentUserRole())
      this.role = 'Invest';
    
  }
  else {
    this.role='Star';
  }
}
  
onFileSelected(event: any) {
  // Logique pour gérer la sélection de fichier
  this.selectedFile = event.target.files[0];
}
submit() {
  const currentUser: user | null = this.authservice.currentUser();

  this.posteInv.idOwner = currentUser?.id;
  this.posteInv.typeInvestissement = "jvk";
  this.posteInv.montant = 12;

  if (this.selectedFile) {
    const reader = new FileReader();

    reader.onload = (e: any) => {
      // Convertir l'image en chaîne base64
      const base64Image = e.target.result.split(',')[1];
      
      // Assigner l'image convertie à la propriété "image" de posteInv
      this.posteInv.image = base64Image;

      console.log(this.posteInv);

      // Envoi du posteInv avec l'image au service
      this.posteService.CreatePosteInv(this.posteInv).subscribe(
        data => {
          console.log(data); // Afficher la réponse du serveur en cas de succès
        },
        error => {
          console.error('Error creating post:', error); // Afficher l'erreur en cas d'échec
        }
      );
    };

    // Lire le fichier sélectionné en tant que données URL
    reader.readAsDataURL(this.selectedFile);
  } else {
    // Afficher un message d'erreur si aucun fichier n'a été sélectionné
    console.error('No file selected');
  }
}

  valider(){
    const currentUser : user | null = this.authservice.currentUser();
    this.posteInv.idOwner=currentUser?.id;
   this.posteInv.image="kl";
   this.posteInv.typeInvestissement="jvk";
   this.posteInv.montant=12;
    this.posteService.CreatePosteStar(this.posteStar).subscribe(
      data => {
        console.log(data);
      },
      error => {
        console.error('Error creating post:', error);
      }
    );
  }

}

