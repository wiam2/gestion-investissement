import { Component, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthService } from "../Services/AuthService.service";
import { InvestisseurService } from "../Services/InvestisseurService.service";
import { ActivatedRoute, Router } from "@angular/router";
import { EditPosteModalService } from "../Services/EditPosteModalService";
import { PosteService } from '../Services/PosteService.service';

@Component({
  selector: 'app-edit-poste',
  templateUrl: './edit-poste.component.html',
  styleUrls: ['./edit-poste.component.css']
})
export class EditPosteComponent implements OnInit {
  role: string = "";
  @Input() size? = "md";
  @Input() poste: any;
  @Output() closeEvent = new EventEmitter();
  @Output() submitEvent = new EventEmitter();
  selectedFile: File | null = null;
  constructor(private elementRef: ElementRef, private authservice: AuthService, private router: Router, private route: ActivatedRoute, private modalPosteService: EditPosteModalService, private posteService: PosteService) {
  }
  ngOnInit(): void {
    if (this.authservice.currentUserRole() === "RInvestisseur") {

      this.role = 'Invest';
      console.log(this.role);
    }
    else {
      this.role = 'Star';
    }
  }
  onFileSelected(event: any) {
    // Logique pour gérer la sélection de fichier
    this.selectedFile = event.target.files[0];
  }

  close() {
    this.elementRef.nativeElement.remove();
    this.closeEvent.emit();
  }
  submit() {
    if (this.role == 'Invest') {
      if (this.selectedFile) {
        const reader = new FileReader();

        reader.onload = (e: any) => {
          // Convertir l'image en chaîne base64
          const base64Image = e.target.result.split(',')[1];

          // Assigner l'image convertie à la propriété "image" de posteInv
          this.poste.image = base64Image;
          // Envoi du poste avec l'image au service
          this.posteService.UpdatePosteInv(this.poste.id, this.poste).subscribe(
            (data) => {
              console.log(data);
            },
            (error) => console.log(error)
          );
        };
        reader.readAsDataURL(this.selectedFile);
      } else {
        // Afficher un message d'erreur si aucun fichier n'a été sélectionné
        console.error('No file selected');
      }
    } else {
      // Envoi du poste au service sans image
      if (this.selectedFile) {
        const reader = new FileReader();

        reader.onload = (e: any) => {
          // Convertir l'image en chaîne base64
          const base64Image = e.target.result.split(',')[1];

          // Assigner l'image convertie à la propriété "image" de posteInv
          this.poste.image = base64Image;
          // Envoi du poste avec l'image au service
          this.posteService.UpdatePosteStar(this.poste.id, this.poste).subscribe(
            (data) => {
              console.log(data);
            },
            (error) => console.log(error)
          );
        };
        reader.readAsDataURL(this.selectedFile);
      } else {
        // Afficher un message d'erreur si aucun fichier n'a été sélectionné
        console.error('No file selected');
      }
    }
    // Supprimer l'élément natif et émettre l'événement de soumission
    this.elementRef.nativeElement.remove();
    this.submitEvent.emit();
  }






  getPostImageUrl(posteInv: any): string {
    if (posteInv && posteInv.image) {
      return 'data:image/jpeg;base64,' + posteInv.image;
    }
    return 'assets/startups.png'; // Image par défaut si aucune image n'est disponible
  }
}
