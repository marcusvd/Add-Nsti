import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: 'phoneNumber',
  standalone: true
})

export class PhoneNumberPipe implements PipeTransform {


  eleven(value: string) {

    const phoneNumber = value

    const areaCode = phoneNumber.charAt(0) + phoneNumber.charAt(1);
    const firstNumberDigit = phoneNumber.charAt(2);

    const mountedNumber = `(${areaCode}) ${firstNumberDigit}-${phoneNumber.slice(3, 7)}-${phoneNumber.slice(7, 13)}`

    return mountedNumber;
  }

  ten(value: string) {

    const phoneNumber = value

    const areaCode = phoneNumber.charAt(0) + phoneNumber.charAt(1);

    const mountedNumber = `(${areaCode}) ${phoneNumber.slice(2, 6)}-${phoneNumber.slice(6, 13)}`

    return mountedNumber;
  }


  transform(value: string) {

    const phoneNumber = value


    if (value == 'Não cadastrado.')
      return value;


    if (value.length == 11)
      return this.eleven(phoneNumber);


    if (value.length == 10)
      return this.ten(phoneNumber);


    return value;
  }

}
