
import { inject } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';


import { BaseForm } from 'shared/extends/forms/base-form';

import { IsUserRegisteredValidator } from 'components/authentication/validators/is-user-registered-validator';
import { PasswordConfirmationValidator } from 'components/authentication/validators/password-confirmation-validator';
import { PasswordValidator } from 'components/authentication/validators/password-validator';
import { FirstConfirmEmailRegisterDto } from '../dtos/first-confirm-email-register-dto';



export class ConfirmEmailBeforeRegisterHelper extends BaseForm {

  private readonly _fb = inject(FormBuilder);
  private readonly _isUserRegisteredValidator = inject(IsUserRegisteredValidator);

  formLoad(register?: FirstConfirmEmailRegisterDto) {
    return this.formMain = this._fb.group({
      email: new FormControl('', { validators: [Validators.required, Validators.maxLength(50), Validators.email], asyncValidators: [this._isUserRegisteredValidator.validate.bind(this._isUserRegisteredValidator)] }),
    })
  }

}

