
import { FormGroup, Validators } from '@angular/forms';
import { DefaultMessages } from 'shared/components/validators/default-messages';
import { ValidatorMessages } from 'shared/components/validators/validators-messages';
import { Base } from './base';


export class BaseForm extends Base {

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

  formErrorAndTouched = (form: FormGroup, field: string, error: string) => {
    // console.log(form.value, field, error);
    return form?.get(field)?.hasError(error) && form?.get(field)?.touched;
  }

  sanitizeFormFields(form: FormGroup): void {
    Object.keys(form.controls).forEach(field => {
      const control = form.get(field);
      if (control instanceof FormGroup) {
        this.sanitizeFormFields(control);
      } else if (control && (control.value === null || control.value === undefined)) {
        control.setValue('');
      }
    })
  }


}

