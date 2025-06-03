import { PhoneNumberPipe } from 'shared/pipes/phone-number.pipe';
import { AddressService } from '../../../shared/components/address/services/address.service';
import { ContactService } from '../../../shared/components/contact/services/contact.service';
import { EntityTypePipe } from '../components/commons-components/pipes/entity-type.pipe';
import { CustomerListService } from '../services/customer-list.service';
import { IsMobileNumberPipe } from 'shared/pipes/is-mobile-number.pipe';

export const CustomerAddProviders: any[] = [
  ContactService,
  AddressService,
  IsMobileNumberPipe
]

export const CustomerListProviders: any[] = [
  PhoneNumberPipe,
  EntityTypePipe,
  CustomerListService
]

export const CustomerEditProviders: any[] = [
  ContactService,
  AddressService,
  IsMobileNumberPipe
]
