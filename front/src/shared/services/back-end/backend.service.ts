import { HttpClient, HttpParams, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { take } from "rxjs/operators";

import { IBackEndService } from "./ibackend.service";

Injectable({
  providedIn: 'root'
})


export abstract class BackEndService<T> implements IBackEndService<T>{

  companyId = localStorage.getItem('companyId')
    ? JSON.parse(localStorage.getItem('companyId')!)
    : '';

  userId = localStorage.getItem('userId')
    ? JSON.parse(localStorage.getItem('userId')!)
    : '';

  constructor(
    protected _http: HttpClient,
    protected _BackEnd?: string,
  ) { }


  add$<T>(record: T, url: string): Observable<T> {
    return this._http.post<T>(`${this._BackEnd}/${url}`, record);
  }

  addRange$<T>(record: T[], url: string): Observable<T> {
    return this._http.post<T>(`${this._BackEnd}/${url}`, record);
  }

  updateRange$<T>(record: T[], url: string): Observable<T> {
    return this._http.put<T>(`${this._BackEnd}/${url}`, record);
  }


  delete$<T>(url?: string, id?: number): Observable<T> {
    if(url){
      return this._http.delete<T>(`${this._BackEnd}/${url}/${id}`).pipe(take(1));
    }
    else{
      return this._http.delete<T>(`${this._BackEnd}/${id}`).pipe(take(1));
    }
  }

  loadAllPaged$<T>(url:string, params:HttpParams): Observable<HttpResponse<T>> {
    return this._http.get<T>(`${this._BackEnd}/${url}`, { observe: 'response', params }).pipe(take(1));
  }

  loadAllWithParams$<T>(url:string, params:HttpParams): Observable<T[]> {
    return this._http.get<T[]>(`${this._BackEnd}/${url}`, { observe: 'body', params }).pipe(take(1));
  }

  loadAll$<T>(url: string): Observable<T[]> {
    return this._http.get<T[]>(`${this._BackEnd}/${url}`).pipe(take(1));
  }

  loadByName$<T>(url: string, name: string): Observable<T> {
    return this._http.get<T>(`${this._BackEnd}/${url}/${name}`).pipe(take(1));
  }

  loadById$<T>(url: string, id: string): Observable<T> {
    return this._http.get<T>(`${this._BackEnd}/${url}/${id}`).pipe(take(1));
  }


  update$<T>(url?: string, record?: any, companyId?:number): Observable<T> {
    return this._http.put<T>(`${this._BackEnd}/${url}/${record.id}` , record).pipe(take(1));
  }

  deleteFake$<T>(url?: string, record?: any): Observable<T> {
    return this._http.put<T>(`${this._BackEnd}/${url}/${record.id}` , record).pipe(take(1));
  }






  // update$<T>(url?: string, record?: any, companyId?:number): Observable<T> {


  //   if(companyId){
  //     return this._http.put<T>(`${this._BackEnd}/${url}/${record.companyId}` , record).pipe(take(1));
  //   }
  //   if(url){
  //     return this._http.put<T>(`${this._BackEnd}/${url}/${record.id}` , record).pipe(take(1));
  //   }
  //   else{
  //     return this._http.put<T>(`${this._BackEnd}/${record.id}`, record).pipe(take(1));
  //   }
  // }





}
