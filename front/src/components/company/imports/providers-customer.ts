import { PhoneNumberPipe } from 'shared/pipes/phone-number.pipe';
import { AddressService } from '../../../shared/components/address/services/address.service';
import { ContactService } from '../../../shared/components/contact/services/contact.service';
import { IsMobileNumberPipe } from 'shared/pipes/is-mobile-number.pipe';

export const CompanyAddProviders: any[] = [
  ContactService,
  AddressService,
  IsMobileNumberPipe
]

export const CompanyListProviders: any[] = [
  PhoneNumberPipe,
]

export const CompanyEditProviders: any[] = [
  ContactService,
  AddressService,
  IsMobileNumberPipe
]
