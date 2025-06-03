import { UntypedFormGroup } from "@angular/forms";



export class ValidatorsCustomer {

  static paymentWhenRequired(formToBeValidate: UntypedFormGroup, valueToBeCompared: number, errorToBeSet: any, controlToBeAccessed: string) {

    const form = formToBeValidate;
    const valueCompare = valueToBeCompared;
    const errorType = errorToBeSet;
    const control = controlToBeAccessed;

    if (form.get(control)?.value != null || form.get(control)?.value != '')
      if (form.get(control)?.value <= valueCompare) {
        form.get(control)?.setErrors(errorType)
      }
  }


}

