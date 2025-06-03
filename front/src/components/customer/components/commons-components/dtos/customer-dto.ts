
import { RootBase } from 'shared/entities-dtos/root-base';
import { EntityTypeEnumDto } from '../../commons-components/dtos/entity-type.enum-dto';
import { AddressDto } from 'shared/components/address/dtos/address-dto';
import { ContactDto } from 'shared/components/contact/dtos/contact-dto';


export class CustomerDto extends RootBase {

  constructor() {super()}

  name!: string;
  tradeName!: string;
  cnpj!: string;
  entityType!: EntityTypeEnumDto
  description!: string;
  address!: AddressDto;
  contact!: ContactDto;

}

