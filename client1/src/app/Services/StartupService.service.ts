import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Startup } from '../models/Startup.model';
import {Investisseur} from "../models/Investisseur.model";


@Injectable({
    providedIn: 'root'
})
export class StartupService {
    private baseURL = "http://localhost:8080/MICROS-AUTH-GUSER";

    constructor(private httpClient: HttpClient ) { }

    CreateStartup(startup: Startup): Observable<Object> {
        // Corrected the URL by enclosing it in quotes and fixing the path
        return this.httpClient.post(`${this.baseURL}/api/Account/registerStartup`, startup);
    }
    GetProfileStartup(id:string):Observable<Startup>{
        return this.httpClient.get<Startup>(`${this.baseURL}/Startup/affichageStartupbyid/${id}`);
    }
    UpdatStartUp(id:string,startup: Startup):Observable<Object>{
        return this.httpClient.put(`${this.baseURL}/Startup/UpdateStartup/${id}`,startup);
    }

}
