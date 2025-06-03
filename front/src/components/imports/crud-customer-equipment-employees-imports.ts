import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatPaginatorModule } from '@angular/material/paginator';
import { AddressComponent } from '../../shared/components/address/component/address.component';
import { NameCpfCnpjComponent } from '../../shared/components/administrative/name-cpf-cnpj/name-cpf-cnpj.component';
import { BtnGDynamicComponent } from '../../shared/components/btn-g-dynamic/btn-g-dynamic.component';
import { ContactComponent } from '../../shared/components/contact/component/contact.component';
import { ListGComponent } from '../../shared/components/list-g/list/list-g.component';
import { CustomerFilterListGComponent } from 'components/customer/components/list/customer-filter-list/customer-filter-list.component';

export const AddImports: any[] = [
  ReactiveFormsModule,
  MatDividerModule,
  MatButtonModule,
  NameCpfCnpjComponent,
  ContactComponent,
  AddressComponent,
]

export const ListImports: any[] = [
  MatPaginatorModule,
  ListGComponent,
  BtnGDynamicComponent,
  MatDividerModule,
  CustomerFilterListGComponent
]

export const EditImports: any[] = [
  ReactiveFormsModule,
  MatDividerModule,
  NameCpfCnpjComponent,
  ContactComponent,
  AddressComponent,
]
