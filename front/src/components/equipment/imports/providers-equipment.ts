import { PhoneNumberPipe } from 'shared/pipes/phone-number.pipe';
import { AddressService } from '../../../shared/components/address/services/address.service';
import { ContactService } from '../../../shared/components/contact/services/contact.service';

export const EquipmentAddProviders: any[] = [
  ContactService,
  AddressService,
]

export const EquipmentListProviders: any[] = [
  PhoneNumberPipe,
]

export const EquipmentEditProviders: any[] = [
  ContactService,
  AddressService
]
