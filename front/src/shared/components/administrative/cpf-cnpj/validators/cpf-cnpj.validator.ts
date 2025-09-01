
import { FormGroup } from '@angular/forms';
import { cpf } from 'cpf-cnpj-validator';
import { cnpj } from 'cpf-cnpj-validator';

export class CpfCnpjValidator {

  public static invalidCpfMsg = 'CPF, inválido!';
  public static invalidCnpjMsg = 'CNPJ, inválido!';

  static isValidCpfCnpj(x: string): boolean {

    const numbers = x?.replace(/\D/g, '');

    if (numbers.length > 11) {
      return cnpj.isValid(numbers)
    }
    else {
      return cpf.isValid(numbers)
    }

  }

  static isValid(x: string, form: FormGroup, controlName: string) {

    const numbers = x.replace(/\D/g, '');

    if (numbers.length > 11) {
      if (!cnpj.isValid(numbers)) {
        form.get(controlName)?.setErrors({ 'invalid-cnpj': true })
        return { entity: 'cnpj', result: false };
      }
      else {
        form.get(controlName)?.setErrors(null)
        return { entity: 'cnpj', result: true };
      }
    } else {

      if (!cpf.isValid(numbers)) {
        form.get(controlName)?.setErrors({ 'invalid-cpf': true })
        return { entity: 'cpf', result: false };
      }
      else {
        form.get(controlName)?.setErrors(null)
        return { entity: 'cpf', result: true };
      }
    }

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

  static isValidCpf(form: FormGroup, ctrl: string) {
    return form.get(ctrl)?.hasError('invalid-cpf')
      ? `CPF, inválido.` : null;
  }
  static isValidCnpj(form: FormGroup, ctrl: string) {
    return form.get(ctrl)?.hasError('invalid-cnpj')
      ? `CNPJ, inválido.` : null;
  }


}
