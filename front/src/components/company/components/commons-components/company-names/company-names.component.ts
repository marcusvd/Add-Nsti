
import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';


import { AddImportsGShared } from 'shared/components/imports/default-g-shared-imports';
import { BaseForm } from '../../../../../shared/inheritance/forms/base-form';


@Component({
  selector: 'company-names',
  templateUrl: './company-names.component.html',
  styleUrls: ['./company-names.component.scss'],
  standalone: true,
  imports: [
    AddImportsGShared,
  ]
})
export class CompanyNamesComponent extends BaseForm  {

  @Input() override  formMain!: FormGroup;

  constructor() { super() }

}
