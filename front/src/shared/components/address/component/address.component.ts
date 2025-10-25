import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormGroup } from '@angular/forms';


import { BaseForm } from 'shared/extends/forms/base-form';
import { AddImportsGShared } from '../../../../shared/components/imports/default-g-shared-imports';
import { AddressService } from '../services/address.service';
import { AddressAddImports } from './imports/address-imports';

@Component({
  selector: 'address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss'],
  standalone: true,
  imports: [
    AddImportsGShared,
    AddressAddImports
  ],
  providers: [
    AddressService,
  ]
})
export class AddressComponent extends BaseForm implements OnInit {

  @Input() override formMain!: FormGroup;

  constructor(
    private _addressService: AddressService,
  ) { super(); }


  query(cep: string) {
    this?._addressService?.query(cep);
  }

  ngOnInit(): void {

    this._addressService.formMain = this.formMain;

  }




}
