
import { FormGroup } from '@angular/forms';
import { cpf } from 'cpf-cnpj-validator';
import { cnpj } from 'cpf-cnpj-validator';

export class CpfCnpjValidator {



  static isValidCpfCnpj(x: string, cpfOrCnpj: string): boolean {

    const numbers = x?.replace(/\D/g, '');

    if (cpfOrCnpj == 'cpf')
      return cpf.isValid(numbers)

    if (cpfOrCnpj == 'cnpj')
      return cnpj.isValid(numbers)

    return false;
  }
  static isValid(x: string, cpfOrCnpj: string, form: FormGroup, controlName: string) {

    if (cpfOrCnpj === 'cnpj')
      if (!this.isValidCpfCnpj(x, cpfOrCnpj))
        form.get(controlName)?.setErrors({ 'invalid-cnpj': true })
      else
        return true

    if (cpfOrCnpj === 'cpf')
      if (!this.isValidCpfCnpj(x, cpfOrCnpj))
        form.get(controlName)?.setErrors({ 'invalid-cpf': true })
      else
        return true;

    return false;
  }

  static isValidEntity(x: string, form: FormGroup, controlName: string) {

    const cleaned = x.replace(/\D/g, '');

    if (cleaned.length > 11) {
      if (!this.isValidCpfCnpj(x, 'cnpj')) {
        form.get(controlName)?.setErrors({ 'invalid-cnpj': true })
        return false;
      }
      else {
        form.get(controlName)?.setErrors(null)
        return true
      }
    } else {

      if (!this.isValidCpfCnpj(x, 'cpf'))
        form.get(controlName)?.setErrors({ 'invalid-cpf': true })
      else
        return true;
    }



    return true
  }

  static formatCpfCnpj(x: string, cpfOrCnpj: string): string {

    const numbers = x.replace(/\D/g, '');

    if (cpfOrCnpj == 'cpf')
      return cpf.format(numbers)

    if (cpfOrCnpj == 'cnpj')
      return cnpj.format(numbers)

    return 'Doesnt work';
  }

  static generateCpfCnpj(x: string, cpfOrCnpj: string): string {

    if (x == 'cpf')
      return cpf.generate()

    if (x == 'cnpj')
      return cnpj.generate()

    return 'Doesnt work';
  }



}
