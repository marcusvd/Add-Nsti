import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { NameCpfCnpjComponent } from "shared/components/administrative/name-cpf-cnpj/name-cpf-cnpj.component";
import { ContactComponent } from 'shared/components/contact/component/contact.component';
import { AddressComponent } from 'shared/components/address/component/address.component';

export const ImportsCompany: any[] = [
  ReactiveFormsModule,
  MatDividerModule,
  MatButtonModule,
  NameCpfCnpjComponent,
  ContactComponent,
  AddressComponent,
]
