import { HttpClient, HttpParams, HttpResponse } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { take } from "rxjs/operators";

import { IBackEndService } from "./ibackend.service";
import { MatSnackBar } from "@angular/material/snack-bar";
import { NavigationExtras, Router } from "@angular/router";
import { WarningsService } from "components/warnings/services/warnings.service";

Injectable({
  providedIn: 'root'
})


export abstract class BackEndService<T> implements IBackEndService<T> {

  companyId = localStorage.getItem('companyId')
    ? JSON.parse(localStorage.getItem('companyId')!)
    : '';

  userId = localStorage.getItem('userId')
    ? JSON.parse(localStorage.getItem('userId')!)
    : '';

  constructor() { }

  private _http = inject(HttpClient);
  private _snackBar = inject(MatSnackBar);
    _router = inject(Router);

  add$<T>(record: T, url: string): Observable<T> {
    return this._http.post<T>(`${url}`, record);
  }

  addRange$<T>(record: T[], url: string): Observable<T> {
    return this._http.post<T>(`${url}`, record);
  }

  updateRange$<T>(record: T[], url: string): Observable<T> {
    return this._http.put<T>(`${url}`, record);
  }


  delete$<T>(url?: string, id?: number): Observable<T> {
    if (url) {
      return this._http.delete<T>(`${url}/${id}`).pipe(take(1));
    }
    else {
      return this._http.delete<T>(`${id}`).pipe(take(1));
    }
  }

  loadAllPaged$<T>(url: string, params: HttpParams): Observable<HttpResponse<T>> {
    return this._http.get<T>(`${url}`, { observe: 'response', params }).pipe(take(1));
  }

  loadAllWithParams$<T>(url: string, params: HttpParams): Observable<T[]> {
    return this._http.get<T[]>(`${url}`, { observe: 'body', params }).pipe(take(1));
  }

  loadAll$<T>(url: string): Observable<T[]> {
    return this._http.get<T[]>(`${url}`).pipe(take(1));
  }

  loadByName$<T>(url: string, name: string): Observable<T> {
    return this._http.get<T>(`${url}/${name}`).pipe(take(1));
  }

  loadById$<T>(url: string, id: string): Observable<T> {
    return this._http.get<T>(`${url}/${id}`).pipe(take(1));
  }


  update$<T>(url?: string, record?: any): Observable<T> {
    return this._http.put<T>(`${url}/${record.id}`, record).pipe(take(1));
  }
  
  updateV2$<T>(url?: string, record?: any): Observable<T> {
    return this._http.put<T>(`${url}`, record).pipe(take(1));
  }

  deleteFake$<T>(url?: string, record?: any): Observable<T> {
    return this._http.put<T>(`${url}/${record.id}`, record).pipe(take(1));
  }


  callRouter(url: string, entity?: any) {

    const objectRoute: NavigationExtras = {
      state: {
        entity
      }
    };

    if (entity)
      this._router?.navigate([url], objectRoute);
    else
      this._router?.navigateByUrl(url);

  }




  openSnackBar(message: string, style: string, action: string = 'Fechar', duration: number = 5000, horizontalPosition: any = 'center', verticalPosition: any = 'top') {
    this._snackBar?.open(message, action, {
      duration: duration, // Tempo em milissegundos (5 segundos)
      panelClass: [style], // Aplica a classe personalizada
      horizontalPosition: horizontalPosition, // Centraliza horizontalmente
      verticalPosition: verticalPosition, // Posição vertical (pode ser 'top' ou 'bottom')
    });
  }

}
