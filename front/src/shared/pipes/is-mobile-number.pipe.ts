import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: 'is-mobile-number',
  standalone: true
})

export class IsMobileNumberPipe implements PipeTransform {

  transform(phoneNumber: string): { phoneNum: string, isMobile: boolean } {
    {

      const justOnePhoneNumber = phoneNumber.split('/')[0]

      let cleanedNumber = justOnePhoneNumber.replace(/\D/g, '');

      if (cleanedNumber[0] === '0')
        cleanedNumber = cleanedNumber.replace('0', '');

      const localArea = cleanedNumber.slice(0, 2);
      const pureNumber = cleanedNumber.substring(2);

      if (pureNumber.length < 9 &&
        pureNumber.startsWith('6')
        || pureNumber.startsWith('7')
        || pureNumber.startsWith('8')
        || pureNumber.startsWith('9')) {

        const resultMobileHandled = `${localArea}9${pureNumber}`

        return { phoneNum: resultMobileHandled, isMobile: true }
      }

      if (pureNumber.length > 8)
        return this.isMobileNumber(cleanedNumber)

      if (pureNumber.length < 9)
        return this.isMobileNumber(cleanedNumber)

      return { phoneNum: '', isMobile: false };

    }

  }

  private isMobileNumber(phoneNumber: string): { phoneNum: string, isMobile: boolean } {

    const cleanedNumber = phoneNumber.replace(/\D/g, '');

    const phoneNumberRegex = /^(\(?(0?[1-9]{2}\)?\s?)(9\d{4}-\d{4}|\d{4}-\d{4}))$/;

    return { phoneNum: cleanedNumber, isMobile: phoneNumberRegex.test(cleanedNumber) };

  }

}

