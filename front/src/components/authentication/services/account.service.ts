
import { Injectable } from '@angular/core';


import { BackEndService } from '../../../shared/services/back-end/backend.service';
import { MyUser } from '../dtos/my-user';


@Injectable({
  providedIn: 'root'
})

export class AccountService extends BackEndService<MyUser> {

  constructor() {super()}

  getUserByName$(url: string, name: string) {
    return this.loadByName$<MyUser>(url, name);
  }

  getUserByEmail$(url: string, email: string) {
    return this.loadByName$<MyUser>(url, email);
  }

}
