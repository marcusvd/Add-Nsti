
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { map } from 'rxjs/operators';


import { AddImportsGShared } from 'shared/components/imports/default-g-shared-imports';
import { BaseForm } from '../../../../shared/inheritance/forms/base-form';

import { QueryCnpjService } from '../services/queryCnpj.service';
import { BusinessData } from './dto/business-data';
import { CpfCnpjImports } from './imports/cpf-cnpj-imports';
import { CpfCnpjValidator } from './validators/cpf-cnpj.validator';
import { DocType } from './dto/doc-type';


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
export class CpfCnpjComponent extends BaseForm implements OnInit {

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
    // if (result.entity == 'cpf' && result.result)
    // if (result.entity == 'cpf')
    //   this.isValidCpf.emit(result)

  }

  ngOnInit(): void {
    //   this.formMain = this._fb.group({
    //     cnpj: ['',[]]
    //   })
    // }

  }
}
