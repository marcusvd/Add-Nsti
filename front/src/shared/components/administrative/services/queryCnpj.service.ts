import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class QueryCnpjService {

  constructor(private _http: HttpClient) { }

  query(cep: string) {
    //return this._http.get(`https://brasilapi.com.br/api/cnpj/v1/${cep}`);

    return this._http.jsonp(`https://receitaws.com.br/v1/cnpj/${cep}`, 'callback');
  }

}

