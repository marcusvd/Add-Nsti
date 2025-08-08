import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function PasswordValidator(): ValidatorFn {

  return (control: AbstractControl): ValidationErrors | null => {

    const group = control as any;

    const password = group.get('password');
    const userName = group.get('userName');

    if (!password.value) return null;

    if ((password.value as string).toLocaleLowerCase().includes('password') || (password.value as string).toLocaleLowerCase().includes('senha')) {
      password?.setErrors({ 'wordPwd': true })
      return { 'wordPwd': true }
    }

    if ((password.value as string).toLocaleLowerCase() == (userName.value as string)) {
      password?.setErrors({ 'usrPwdIsEqual': true })
      return { 'usrPwdIsEqual': true }
    }

    return null;
  }

}
