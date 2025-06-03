import { FormArray, FormGroup } from "@angular/forms";

export class ValidatorMessages {

  private static _characters: string = ' caracteres.';
  private static _quantity: number;
  private static _min: string = 'Mínimo de pelo menos ';
  private static _max: string = 'não pode ultrapassar ';
  private static _minLen: string = 'Preenchimento, mínimo de pelo menos ';
  private static _maxLen: string = 'não pode ultrapassar ';
  private static _req: string = ' Preenchimento obrigatório.';
  private static _opt: string = 'Selecione uma opção, por favor.';
  private static _invalidDate: string = ' Data está no formato incorreto, preencha novamente, FORMATO: dd/mm/aaaa ou selecione uma.';
  private static _atLeastOne: string = ' Pelo menos um dos contatos, deve ser preenchido.';
  private static _email: string = 'E-mail é inválido. Por favor, insira um valido! ';
  private static _emailDuplicated: string = 'E-mail já cadastrado, mude o email e tente novamente.';


  static invalidDate(form: FormGroup, ctrl: string,) {
    return form.get(ctrl)?.hasError('matDatepickerParse')
      ? `${this._invalidDate}` : '';
  }

  static atLeastOne(form: FormGroup, ctrl: string, ctrlToShow: string) {
    return form.get(ctrl)?.hasError('atleastone')
      ? `${this._atLeastOne}` : '';
  }

  static mailField(form: FormGroup | FormArray, ctrl: string) {
    return form.get(ctrl)?.hasError('email')
      ? `${this._email}`
      : form.get(ctrl)?.hasError('errorEmailDuplicated') ? `${this._emailDuplicated}` : ''
  }

  static required(form: FormGroup | FormArray, ctrl: string, ctrlToShow: string) {
    // form)
    return form?.get(ctrl)?.hasError('required')
      ? `${ctrlToShow + ' '}` : form?.get(ctrl)?.hasError('empty')
        ? this._quantity : '';
  }
  // static required(form: FormGroup | FormArray, ctrl: string, ctrlToShow: string) {
  //   // form)
  //   return form?.get(ctrl)?.hasError('required')
  //     ? `${ctrlToShow + ' '}${this._req}` : form?.get(ctrl)?.hasError('empty')
  //       ? this._quantity : '';
  // }

//   static required2(form: FormArray, ctrl: string, ctrlToShow: string) {
//     let ff: string | number = null;
// ff)
//     for (let f of form.controls) {
//       ff = f?.get(ctrl)?.hasError('required')
//         ? `${ctrlToShow + ' '}${this._req}` : f?.get(ctrl)?.hasError('empty')
//           ? this._quantity : '';
//       // f.get('url'))
//     }
//     return ff;

//   }
  static changeSelection(form: FormGroup, ctrl: string, ctrlToShow: string) {
    return form?.get(ctrl)?.hasError('changeOpt') ? `${this._opt}` : '';
  }

  static minMaxLength(form: FormGroup, ctrl: string, ctrlToShow: string, lengthMin?: number, lengthMax?: number) {

    return form.get(ctrl)?.hasError('minlength')
      ? `${ctrlToShow} ${this._minLen}${lengthMin}${this._characters}` : form.get(ctrl)?.hasError('maxlength')
        ? `${ctrlToShow} ${this._maxLen}${lengthMax}${this._characters}` : '';
  }

  static minMax(form: FormGroup, ctrl: string, ctrlToShow: string, valueMin?: number | string, valueMax?: number | string) {
    return form.get(ctrl)?.hasError('min')
      ? `${this._min}${valueMin}.` : form.get(ctrl)?.hasError('max')
        ? `${this._max}${valueMax}.` : null;
  }

  static touchedErrors(groupOrArray: FormGroup, ctrl: string) {
    return groupOrArray.get(ctrl)?.errors
      && groupOrArray.get(ctrl)?.touched
      ? true : false;
  }

  static compareFields(form: FormGroup, ctrl: string) {
    // form)
    return form.get(ctrl)?.hasError('noIqual')
      ? `Senha / Confirmar devem ser idênticos.` : null;
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
