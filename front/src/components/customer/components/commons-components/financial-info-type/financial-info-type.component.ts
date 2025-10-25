
import { Component, Input, OnInit } from '@angular/core';
import { BaseForm } from '../../../../../shared/extends/forms/base-form';
import { ValidatorsCustomer } from '../../../validators/customer/validators-customer';
import { ValidatorMessagesCustomer } from '../../../validators/customer/validators-messages-customer';
import { ValidatorMessages } from '../../../../../shared/components/validators/validators-messages';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';

import { ValidatorsCustom } from '../../../../../shared/components/validators/validators-custom';

import { CommonModule } from '@angular/common';
import { MatCheckboxModule as MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule as MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule as MatButtonModule } from '@angular/material/button';
import { MatInputModule as MatInputModule } from '@angular/material/input';
import { CurrencyMaskModule } from 'ng2-currency-mask';


@Component({
  selector: 'financial-info-type',
  templateUrl: './financial-info-type.component.html',
  styleUrls: ['./financial-info-type.component.css'],
  standalone: true,
  imports: [
    CommonModule,

    MatCheckboxModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatInputModule,
    CurrencyMaskModule,
  ]
})
export class FinancialInfoTypeComponent extends BaseForm implements OnInit {

  constructor(

  ) { super() }

  ngOnInit(): void {
  }




}

  private valMessagesCustomer = ValidatorMessagesCustomer;
  get validatorMessagesCustomer() {
  Customer
}

  private valLocal = ValidatorsCustomer;
  get validatorsLocal() {
  return this.valLocal
}



@Input() override formMain!: FormGroup;
@Input() override subForm!: FormGroup;
@Input() additionalCosts!: FormGroup;
assured() {

  if (this.formMain.get('assured')?.value)
    this.assuredEnabled();

  if (!this.formMain.get('assured')?.value)
    this.assuredDisabled();

}



assuredEnabled() {
  this.formMain.controls['payment'].enable();
  this.formMain.controls['expiration'].enable();
  this.additionalCosts.controls['fixedPhysicallyMovingCosts'].enable();
}

assuredDisabled() {
  this.formMain.controls['payment'].disable();
  this.formMain.controls['expiration'].disable();
  this.additionalCosts.controls['fixedPhysicallyMovingCosts'].disable();
  this.formMain.get('payment')?.setValue(0)
  this.formMain.get('expiration')?.setValue(0)
  // this.additionalCosts.get('fixedPhysicallyMovingCosts').setValue(0)
}

}
