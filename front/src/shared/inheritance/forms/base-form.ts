
import { FormGroup, Validators } from '@angular/forms';
import * as diacritics from 'diacritics';
import { DefaultMessages } from 'shared/components/validators/default-messages';
import { ValidatorMessages } from 'shared/components/validators/validators-messages';

export class BaseForm {

  companyId = localStorage.getItem('companyId')
    ? JSON.parse(localStorage.getItem('companyId')!)
    : '';

  userId = localStorage.getItem('userId')
    ? JSON.parse(localStorage.getItem('userId')!)
    : '';

  minDate = new Date('0001-01-01T00:00:00');

  currentDate = new Date();
  currentDateWithoutHours = this.currentDate.setHours(0, 0, 0, 0)

  formMainDynamic!: FormGroup;
  formMain!: FormGroup;
  subForm!: FormGroup;

  saveBtnEnabledDisabled: boolean = false;

  defaultMessages = DefaultMessages;
  validatorMessages = ValidatorMessages;

  addValidators(form: FormGroup, fields: string[]) {
    fields.forEach(field => {
      form?.get(field)?.setValidators(Validators.required);
      form?.get(field)?.updateValueAndValidity();
    })
  }

  removeValidators(form: FormGroup, fields: string[]) {
    fields.forEach(field => {
      form?.get(field)?.setValue(null);
      form?.get(field)?.removeValidators(Validators.required);
      form?.get(field)?.removeValidators(Validators.requiredTrue);
      form?.get(field)?.updateValueAndValidity();
    })
  }

  resetFields(form: FormGroup, fields: string[]) {
    fields.forEach(field => {
      form?.get(field)?.reset();
    })
  }

  setFormFieldEnableDisable(form: FormGroup, field: string, action: boolean) {
    if (action)
      form?.get(field)?.enable();
    else
      form?.get(field)?.disable();
  }

  setFormFieldValue(form: FormGroup, field: string, value: any) {
    form?.get(field)?.setValue(value);
  }


  removeNonNumericAndConvertToNumber(str: string): number {
    return +str?.replace(/\D/g, '');
  }

  removeAccentsSpecialCharacters(value: string): string {
    const noAccents = diacritics.remove(value);//remove accents
    return noAccents?.replace(/[^\w\s]/gi, ''); //remove special characters
  }


  formErrorAndTouched = (form: FormGroup, field: string, error: string) => {
    // console.log(form.value, field, error);
    return form?.get(field)?.hasError(error) && form?.get(field)?.touched;
  }




  alertSave(form: FormGroup) {
    if (!form?.valid) {
      alert('Todos os campos com (*) e em vermelho, são de preenchimento obrigatório. Preencha corretamente e tente novamente.')
      form?.markAllAsTouched();
      return false;
    }
    else {
      return true;
    }

  }
}

