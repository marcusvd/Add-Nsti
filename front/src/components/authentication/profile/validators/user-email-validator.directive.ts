import { Directive, forwardRef } from '@angular/core';
import { NG_ASYNC_VALIDATORS } from '@angular/forms';

@Directive({
  selector: '[userEmailValidator]',
  standalone: true,
  providers:[
    {
      provide:NG_ASYNC_VALIDATORS,
      useExisting:forwardRef(()=> UserEmailValidatorDirective),
      multi:true
    }
  ]
})
export class UserEmailValidatorDirective {

  constructor() { }

}
