import { AddressDto } from "shared/components/address/dtos/address-dto";
import { ContactDto } from "shared/components/contact/dtos/contact-dto";
import { ItemsInterface } from "shared/components/list-g/list/interfaces/items-interface";
import { RootBase } from "shared/entities-dtos/root-base";

export class ListEmployeeDto {
  id!: ItemsInterface;
  name!: ItemsInterface;
  abbreviatedName!: ItemsInterface;
  cel!: ItemsInterface;
  description!: ItemsInterface;
}
