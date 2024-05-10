import { user } from "./user.model";
export class poste {
    id: number = 0;
    idOwner: string | undefined = '';
    titre: string = '';
    description: string = '';
    montant: number = 0;
    secteur: string = '';
    image: string = '';
    numLikes: number = 0;
    datePoste: Date = new Date();
}