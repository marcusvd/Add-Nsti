import { Injectable } from '@angular/core';
import { NgForm } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class CaptchaService {

  private result: boolean = false;
  token: string | undefined;
  constructor() {

  }

  get resultForm() {
    return this.result;
  }


}
