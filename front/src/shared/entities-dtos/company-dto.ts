import { AddressDto } from "../components/address/dtos/address-dto";
import { ContactDto } from "../components/contact/dtos/contact-dto";

export class CompanyDto {
   id!:number ;
   name!:string ;
   address!:AddressDto ;
   contact!:ContactDto ;
}
