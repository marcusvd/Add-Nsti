import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function PasswordConfirmationValidator(): ValidatorFn {

  return (control: AbstractControl): ValidationErrors | null => {

    const group = control as any;

    const passwordControl = group?.get('password');
    const pwdConfirmationControl = group?.get('confirmPassword');

    if (!pwdConfirmationControl?.value) return null;

    if (passwordControl.value !== pwdConfirmationControl.value) {
      pwdConfirmationControl?.setErrors({ 'invalidPasswordConfirmation': true })
      return { 'invalidPasswordConfirmation': true }
    }

    return null;

  }

}
