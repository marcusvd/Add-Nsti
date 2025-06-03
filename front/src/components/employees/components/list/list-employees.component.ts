import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EmployeesListService } from 'components/employees/services/employees-list.service';
import { Subscription } from 'rxjs';
import { PhoneNumberPipe } from 'shared/pipes/phone-number.pipe';
import { EmployeesListProviders } from '../../../../components/employees/imports/employees-imports';
import { ListImports } from '../../../imports/crud-customer-equipment-employees-imports';
import { DefaultCompImports } from '../../../imports/default-comp-imports';
import { ListControlEmployees } from './helpers/list-control-employees-list';

@Component({
  selector: 'app-list-employees',
  standalone: true,
  imports: [DefaultCompImports, ListImports],
  providers: [EmployeesListProviders],
  templateUrl: './list-employees.component.html',
  styleUrl: './list-employees.component.scss'
})
export class ListEmployeesComponent extends ListControlEmployees implements OnInit, OnDestroy {

  constructor(
    override _router: Router,
    override _http: HttpClient,
    override _phoneNumberPipe: PhoneNumberPipe,
    override _employeesListServices: EmployeesListService,
  ) {
    super(
      _router,
      _http,
      _phoneNumberPipe,
      _employeesListServices
    )
  }

  employeesUnsubscribe: Subscription | undefined;



  ngOnInit(): void {
    const isProd: boolean = false;
    this.employeesUnsubscribe = this.startSupply(isProd);
  }

  ngOnDestroy(): void {
    this.employeesUnsubscribe?.unsubscribe();
  }





}
