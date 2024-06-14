import {Injectable} from "@angular/core";
import {Observable, Subject} from "rxjs";
import {AlertInterface} from "../alert/alert.interface";

@Injectable({
  providedIn:'root'
})
export class AlertService{
  private alert$=new Subject<AlertInterface>
    setAlert(alert: AlertInterface): void {
        this.alert$.next(alert);
        console.log('Alert set:', alert);
    }

    getAlert(): Observable<AlertInterface> {
        console.log('Alert requested');
        return this.alert$.asObservable();
    }


}
