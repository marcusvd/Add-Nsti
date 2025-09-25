import { HttpParams, HttpResponse } from "@angular/common/http";
import { Observable } from "rxjs";

export interface IBackEndService<T> {

  add$<T>(record: T, url: string): Observable<T>;
  addRange$<T>(record: T[], url: string): Observable<T>;
  updateRange$<T>(record: T[], url: string): Observable<T>;
  update$<T>(url?: string, record?: any): Observable<T>;
  updateV2$<T>(url?: string, record?: any, id?: any): Observable<T>;
  delete$<T>(url?: string, id?: number): Observable<T>;
  deleteFake$<T>(url?: string, record?: any): Observable<T>
  loadByName$<T>(url: string, name: string): Observable<T>;
  loadById$<T>(url: string, id: string): Observable<T>;
  load$<T>(url: string): Observable<T>;
  loadAll$<T>(url: string): Observable<T[]>;
  loadAllPaged$<T>(url: string, params: HttpParams): Observable<HttpResponse<T>>;
  loadAllWithParams$<T>(url: string, params: HttpParams): Observable<T[]>;


}
