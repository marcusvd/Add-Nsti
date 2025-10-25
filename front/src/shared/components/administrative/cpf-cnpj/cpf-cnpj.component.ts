
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { map } from 'rxjs/operators';


import { AddImportsGShared } from 'shared/components/imports/default-g-shared-imports';
import { BaseForm } from '../../../../shared/extends/forms/base-form';
import { QueryCnpjService } from '../services/queryCnpj.service';
import { BusinessData } from './dto/business-data';
import { DocType } from './dto/doc-type';
import { CpfCnpjImports } from './imports/cpf-cnpj-imports';
import { CpfCnpjValidator } from './validators/cpf-cnpj.validator';


@Component({
  selector: 'cpf-cnpj',
  templateUrl: './cpf-cnpj.component.html',
  styleUrls: ['./cpf-cnpj.component.scss'],
  standalone: true,
  imports: [
    AddImportsGShared,
    CpfCnpjImports
  ]
})
export class CpfCnpjComponent extends BaseForm {

  constructor(
    private _queryCnpjService: QueryCnpjService,
    private _fb: FormBuilder
  ) { super() }

  @Input() override  formMain!: FormGroup;
  @Output() cpfCnpjBusinessData: EventEmitter<BusinessData> = new EventEmitter();
  @Output() isValidCpf: EventEmitter<DocType> = new EventEmitter();
  cpfCnpjValidator = CpfCnpjValidator

  getCnpjData(numbers: string, form: FormGroup, controlName: string) {

    const result = CpfCnpjValidator.isValid(numbers, form, controlName);

    if (result.entity == 'cnpj' && result.result)
      this._queryCnpjService.query(numbers.replace(/\D/g, '')).pipe(map(x => x)).subscribe(
        (businessData) => {
          this.cpfCnpjBusinessData.emit(businessData as BusinessData);
        })

    if (result.entity && result.result)
      this.isValidCpf.emit(result);
  }

}
