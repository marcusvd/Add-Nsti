
import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { map } from 'rxjs/operators';


import { AddImportsGShared } from 'shared/components/imports/default-g-shared-imports';
import { BaseForm } from '../../../../shared/inheritance/forms/base-form';
import { CpfCnpjValidator } from '../../validators/cpf-cnpj.validator';
import { QueryCnpjService } from '../services/queryCnpj.service';
import { BusinessData } from './dto/business-data';
import { NameCpfCnpjImports } from './imports/name-cpf-cnpj-imports';


@Component({
  selector: 'name-cpf-cnpj',
  templateUrl: './name-cpf-cnpj.component.html',
  styleUrls: ['./name-cpf-cnpj.component.scss'],
  standalone: true,
  imports: [
    AddImportsGShared,
    NameCpfCnpjImports
  ]
})
export class NameCpfCnpjComponent extends BaseForm implements OnChanges {

  constructor(
    private _queryCnpjService: QueryCnpjService
  ) { super() }

  ngOnChanges(changes: SimpleChanges): void {

    if (this.formMain?.get('entityType')?.value === 0) {
      this.checkPjPf = false;
    }
  }

  @Input() override  formMain!: FormGroup;
  @Input() name: boolean = true;
  @Input() btnGetData: boolean = true;
  @Input() formCtrlName = 'entityType';


  checkPjPf: boolean = false;
  cnpjNumbers: string = '';
  cpfNumbers: string = '';

  getCnpjNumbers(numbers: string) {
    this.cnpjNumbers = numbers;
  }

  getCpfNumbers(numbers: string) {
    this.cpfNumbers = numbers;
  }

  isValid(numbers: string, cpfOrCnpj: string, form: FormGroup, controlName: string) {
    return CpfCnpjValidator.isValid(numbers, cpfOrCnpj, form, controlName);
  }

  @Output() cpfCnpjBusinessData: EventEmitter<BusinessData> = new EventEmitter();
  getCnpjData(numbers: string, cpfOrCnpj: string, form: FormGroup, controlName: string) {

    if (this.isValid(numbers, cpfOrCnpj, form, controlName))
      this._queryCnpjService.query(numbers.replace(/\D/g, '')).pipe(map(x => x)).subscribe(
        (businessData) => {
          this.cpfCnpjBusinessData.emit(businessData as BusinessData);
        })
  }

  inputMask(selected: string) {

    if (selected === 'CPF')
      return "000.000.000-00";

    if (selected === 'CNPJ')
      return "00.000.000/0000-00";

    return '';
  }
}
