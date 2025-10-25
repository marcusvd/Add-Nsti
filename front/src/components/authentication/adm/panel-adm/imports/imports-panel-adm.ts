import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatDividerModule } from "@angular/material/divider";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { RouterModule } from "@angular/router";
import { MatTabsModule } from '@angular/material/tabs';


import { BtnGComponent } from "shared/components/btn-g/btn-g.component";
import { CaptchaComponent } from "shared/components/captcha/captcha.component";
import { DefaultComponent } from "shared/components/default-component/default-component";
import { CompanyNamesComponent } from "components/company/components/commons-components/company-names/company-names.component";
import { CpfCnpjComponent } from "shared/components/administrative/cpf-cnpj/cpf-cnpj.component";
import { ListGComponent } from "shared/components/list-g/list/list-g.component";
import { AddressComponent } from "shared/components/address/component/address.component";
import { ContactComponent } from "shared/components/contact/component/contact.component";
import { ContactService } from "shared/components/contact/services/contact.service";
import { AddressService } from "shared/components/address/services/address.service";
import { IsMobileNumberPipe } from "shared/pipes/is-mobile-number.pipe";

export const ImportsPanelAdm: any[] = [
  CommonModule,
  MatCardModule,
  MatFormFieldModule,
  ReactiveFormsModule,
  MatIconModule,
  MatInputModule,
  MatButtonModule,
  MatDividerModule,
  RouterModule,
  CaptchaComponent,
  BtnGComponent,
  MatTabsModule,
  DefaultComponent,
  CompanyNamesComponent,
  CpfCnpjComponent,
  ListGComponent,
  AddressComponent,
  ContactComponent,
]
export const ProvidersPanelAdm: any[] = [
  ContactService,
  AddressService,
  IsMobileNumberPipe
]
