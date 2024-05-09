import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Investisseur } from '../models/Investisseur.model';


@Injectable({
    providedIn: 'root'
})
export class InvestisseurService {
    private baseURL = "http://localhost:8080/MICROS-AUTH-GUSER";

    constructor(private httpClient: HttpClient ) { }

    CreateInvestisseur(investisseur: Investisseur): Observable<Object> {
        // Corrected the URL by enclosing it in quotes and fixing the path
        return this.httpClient.post(`${this.baseURL}/api/Account/registerInvestisseur`, investisseur);
    }
    GetProfileInv(id:string):Observable<Investisseur>{
        return this.httpClient.get<Investisseur>(`${this.baseURL}/Investisseur/affichageInvestisseur/${id}`);
    }
    UpdateInvest(id:string,investisseur: Investisseur):Observable<Object>{
      return this.httpClient.put(`${this.baseURL}/Investisseur/UpdateInvestisseur/${id}`,investisseur);
    }


}
