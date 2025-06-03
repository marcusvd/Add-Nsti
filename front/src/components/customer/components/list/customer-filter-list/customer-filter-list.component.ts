import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { BaseList } from '../../../../../shared/components/list-g/extends/base-list';
import { ex_formControlSearch, ex_search } from '../../../../../shared/helpers/search-field/search-field';
import { ImportsCustomerFilterImports } from './imports/imports-customer-filter-imports';
import { FilterSearch } from './interface/filter-search';

@Component({
  selector: 'customer-filter-list',
  templateUrl: './customer-filter-list.component.html',
  styleUrls: ['./customer-filter-list.component.scss'],
  standalone: true,
  imports: [
    ImportsCustomerFilterImports
  ]
})

export class CustomerFilterListGComponent extends BaseList implements OnInit {

  constructor(private _fb: FormBuilder) {
    super()
  }
  formMain: FormGroup = new FormGroup({});

  entities: FilterSearch[] = [{ key: 'PJ', value: '0' }, { key: 'PF', value: '1' }, { key: 'Selecione', value: 'Selecione' }];
  assureds: FilterSearch[] = [{ key: 'Assegurado', value: 'true' }, { key: 'Não Assegurado', value: 'false' }, { key: 'Selecione', value: 'Selecione' }];

  select = new FormControl();

 @Output() outFieldSearch = new EventEmitter<string>();
  @Output() filterFormOut = new EventEmitter<FormGroup>();
  @Input() showHideFilter!: boolean;
  filterMtd() {
    this.filterFormOut.emit(this.formMain);
    this.formMain.reset();
    this.formLoad();
  }

  entitySelected!: string;
  entitySelect(value: string) {
    this.entitySelected = value;
  }

  assuredSelected!: string;
  assuredSelect(value: string) {
    this.assuredSelected = value;
  }

  formLoad() {
    this.formMain = this._fb.group({
      assured: ['Selecione', []],
      entity: ['Selecione', []]
    })

  }

  ngOnInit(): void {
    this.formLoad();
  }

    formControlSearch = ex_formControlSearch;

    //METHODS
    search = ex_search
}
