import { Component } from '@angular/core';
import { poste } from '../models/poste.model';
import { OnInit , inject} from '@angular/core';
import { AuthService } from '../Services/AuthService.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PosteService } from '../Services/PosteService.service';
import { posteInv } from '../models/posteInv.model';
import { postestar } from '../models/postestar.model';
import { user } from '../models/user.model';
import { InvestisseurService } from '../Services/InvestisseurService.service';
import { StartupService } from '../Services/StartupService.service';
import { switchMap, map } from 'rxjs/operators';
import { Observable, forkJoin, of } from 'rxjs';
import { ChatService } from '../Services/chat.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {
  posteInv: posteInv = new posteInv();
  posteStar: postestar = new postestar();
  role: string = "";
  selectedFile: File | null = null;
  Posts: any;
  nom: any;
  idOwner:string="";
  useremail:string="";
 
  chatService = inject(ChatService);
  constructor(private authservice: AuthService, private router: Router, private route: ActivatedRoute, private posteService: PosteService, private investService: InvestisseurService, private startService: StartupService) { }
  ngOnInit(): void {
    const currentUser = this.authservice.currentUser();
    this.useremail = currentUser ? currentUser.email : '';

    if (this.authservice.currentUserRole() === "RInvestisseur") {
      console.log(this.authservice.currentUserRole())
      this.role = 'Invest';

    }
    else {
      this.role = 'Star';
    }
    this.loadPost();
  }
  setId(idOwner: any) {
  
      this.idOwner = idOwner;
      console.log(this.idOwner);
  //  this.getOwnerName();
  }
  onFileSelected(event: any) {
    // Logique pour gérer la sélection de fichier
    this.selectedFile = event.target.files[0];
    console.log(this.selectedFile );
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
  getPostImageUrl(posteInv: any): string {
    if (posteInv && posteInv.image) {
      return 'data:image/jpeg;base64,' + posteInv.image;
    }
    return 'assets/startups.png'; // Image par défaut si aucune image n'est disponible
  }


loadPost() {
  let postsObservable: Observable<any>;

  if (this.role === "Invest") {
    postsObservable = this.posteService.getPostsForInv().pipe(
      switchMap(posts => {
        const observables: Observable<any>[] = posts.map(post => this.startService.GetProfileStartup(post.idOwner));
        return forkJoin(observables).pipe(
          map(noms => {
            posts.forEach((post, index) => {
              post.nomOwner = noms[index].nomstr;
              post.emailowner=noms[index].email;
            });
            return posts;
          })
        );
      })
    );
  } else {
    postsObservable = this.posteService.getPostsForStar().pipe(
      switchMap(posts => {
        const observables: Observable<any>[] = posts.map(post => this.investService.GetProfileInv(post.idOwner));
        return forkJoin(observables).pipe(
          map(noms => {
            posts.forEach((post, index) => {
              post.nomOwner = noms[index].nom + " " + noms[index].prenom;
              post.emailowner=noms[index].email;
            });
            return posts;
          })
        );
      })
    );
  }

  postsObservable.subscribe(
    (data) => {
      console.log(data);
      this.Posts = data;
    },
    error => {
      console.log(error);
    }
  );
}

  // getOwnerName() :Observable<string>{
  //   if (this.role == "Invest") {
  //     this.startService.GetProfileStartup(this.idOwner).subscribe(
  //       (data) => {
  //         console.log(data);
  //         return data.nomstr;
  //       },
  //       error => {
  //         console.log(error);
  //       }
  //     );
  //   }
  //   else {
  //     this.investService.GetProfileInv(this.idOwner).subscribe(
  //       (data) => {
  //         console.log(data);
  //         return data.nom + " " + data.prenom;
  //       },
  //       error => {
  //         console.log(error);
  //       }
  //     );
  //   }
  //   return this.nom
    
  // }


  valider() {
    const currentUser: user | null = this.authservice.currentUser();
    this.posteStar.idOwner = currentUser?.id;
    this.posteStar.image = "kl";
    this.posteStar.montant = 12;
    if (this.selectedFile) {
      const reader = new FileReader();

      reader.onload = (e: any) => {
        // Convertir l'image en chaîne base64
        const base64Image = e.target.result.split(',')[1];

        // Assigner l'image convertie à la propriété "image" de posteInv
        this.posteInv.image = base64Image;

        console.log(this.posteInv);
        this.posteService.CreatePosteStar(this.posteStar).subscribe(
          data => {
            console.log(data);
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
  joinRoom(user: string, room: string,name :string) {
    console.log(user, room);   
     this.chatService.start(); // Reconnexion à SignalR après avoir rejoint la salle
    this.chatService.joinRoom(user, room)
      .then(() => {
        sessionStorage.setItem("me", user);
        sessionStorage.setItem("contact", name);
        
       
       
        this.router.navigate(['conversation/chat',name]);
      }).catch((err) => {
        console.log(err);
      })
  }

}

