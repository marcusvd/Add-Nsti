import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";



export function atLeastOneContactValidator(): ValidatorFn {

  return (control: AbstractControl): ValidationErrors | null => {
    const group = control as any;
    
    const cel = group.get('cel')?.value;
    const zap = group.get('zap')?.value;
    const landline = group.get('landline')?.value;

    const isValid =
      (cel && cel.trim().length >= 11)
      ||
      (zap && zap.trim().length >= 10)
      ||
      (landline && landline.trim().length >= 10)


    if (!isValid) {
      group.get('cel')?.setErrors({ atleastone: true });
      group.get('zap')?.setErrors({ atleastone: true });
      group.get('landline')?.setErrors({ atleastone: true });
    }
    else {
      group.get('cel')?.setErrors(null);
      group.get('zap')?.setErrors(null);
      group.get('landline')?.setErrors(null);
    }


    return null;


  }


}
