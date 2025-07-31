
import { HttpClient } from '@angular/common/http';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { map, Observable, of } from 'rxjs';
import { BaseList } from '../../../../../shared/components/list-g/extends/base-list';
import { ListGDataService } from '../../../../../shared/components/list-g/list/data/list-g-data.service';
import { OnClickInterface } from '../../../../../shared/components/list-g/list/interfaces/on-click-interface';

import { PhoneNumberPipe } from '../../../../../shared/pipes/phone-number.pipe';
import { ListEmployeeDto } from '../dtos/list-employees-dto';
import { EmployeeDto } from '../../commons-components/dtos/employees-dto';
import { EmployeesListService } from 'components/employees/services/employees-list.service';


export class ListControlEmployees extends BaseList {

  entities$!: Observable<ListEmployeeDto[]>;
  entities: ListEmployeeDto[] = [];
  entitiesFiltered$!: Observable<ListEmployeeDto[] | undefined>;
  entitiesFiltered: ListEmployeeDto[] = [];
  length = 0;
  showHideFilter = false;
  term!: string;
  controllerUrl: string = "environment._CUSTOMERS.split('/')[4]";
  backEndUrl: string = `${this.controllerUrl}/GetAllCustomersPagedAsync`;

  constructor(
    override _router: Router,
    public _http: HttpClient,
    protected _phoneNumberPipe: PhoneNumberPipe,
    protected _employeesListServices: EmployeesListService,
  ) {
    super(
      new ListGDataService(),
      _router,

    )
  }
  // constructor(
  //   override _router: Router,
  //   public _http: HttpClient,
  //   protected _phoneNumberPipe: PhoneNumberPipe,
  //   protected _employeesListServices: EmployeesListService,
  // ) {
  //   super(
  //     new ListGDataService(),
  //     _router,

  //   )
  // }

  labelHeadersMiddle = () => {
    return [
      { key: '', style: 'cursor: pointer; ' },
      { key: 'Nome', style: 'cursor: pointer;' },
      { key: 'Cel', style: 'cursor: pointer;' },
      { key: 'Descrição', style: 'cursor: pointer; flex:2;' }
    ]
  }

  fieldsHeadersMiddle = () => {
    return [
      { key: 'id', style: '' },
      { key: 'name', style: '' },
      { key: 'cel', style: '' },
      { key: 'description', style: '' }
    ]
  }

  onPageChange($event: PageEvent) {

    if ($event.previousPageIndex ?? 0 < $event.pageIndex)
      this.entitiesFiltered$ = of(this.pageChange(this.entitiesFiltered, $event)?.filter(x => x != null))

    else if ($event.previousPageIndex ?? 0 > $event.pageIndex)
      this.entitiesFiltered$ = of(this.pageChange(this.entitiesFiltered, $event)?.filter(x => x != null))

    if (this.term) {
      this.entitiesFiltered$ = of(this.pageChange(this.searchListEntities(this.entitiesFiltered, this.term), $event)?.filter(x => x != null))
      this.length = this.searchListEntities(this.entitiesFiltered, this.term).length
    }

  }

  onClickOrderByFields(field: string, entities$: Observable<ListEmployeeDto[] | undefined>) {

    switch (field) {
      case 'name':

        this.entities$ = this.orderByFrontEnd(entities$, { key: field, value: '' }) as Observable<ListEmployeeDto[]>;
        break;

      case 'assured':
        this.entities$ = this.orderByFrontEnd(entities$, { key: field, value: '' }) as Observable<ListEmployeeDto[]>;
        break;

      case 'responsible':
        this.entities$ = this.orderByFrontEnd(entities$, { key: field, value: 0 }) as Observable<ListEmployeeDto[]>;
        break;
    }

  }

  onClickButton(field: string) {
  }

  onClickIcons(obj: OnClickInterface) {
    if (obj.action == 'edit') {

    }



    // ex_callRouteWithObject('/side-nav/stock-product-router/detailed-product', this.products.find(x => x.id == obj.entityId), this._router)
  }

  supplyItemsGrid = (employeesList: ListEmployeeDto[], employee: EmployeeDto) => {

    const items: ListEmployeeDto = new ListEmployeeDto();

    Object.assign(items, {

      id: {
        key: employee.id.toString(),
        display: 'icons',
        icons: ['list', 'edit', 'home'],
        styleInsideCell: `color:rgb(11, 112, 155); cursor: pointer; font-size:20px;`,
        styleCell: '',
        route: ''
      },

      name: {
        key: employee.name,
        styleCell: 'width:100%;',

      },

      cel: {
        key: employee.contact.cel,
        styleCell: 'width:100%;',
      },

      description: {
        key: employee.description,
        styleCell: 'flex:2;',
        styleInsideCell: `flex:2;`
      }
    })

    employeesList.push(items);

    return employeesList;
  }


  startSupply(isProd: boolean) {

    let entities: ListEmployeeDto[] = [];

    const customers: Observable<EmployeeDto[]> = this._employeesListServices.getData(isProd);

    return customers.pipe(map(x => {

      x.forEach(y => {
        this.entities = this.supplyItemsGrid(entities, y);
        this.entities$ = of(this.entities);
        this.entitiesFiltered$ = this.entities$;
      })


    })).subscribe();

  }




  // startSupply(): Subscription | undefined {

  //   let entities: ListEmployeeDto[] = [];

  //   return this._listGDataService?.entities$.subscribe(
  //     {
  //       next: (x: CustomerDto[]) => {
  //         x.forEach(
  //           (y: CustomerDto) => {
  //             this.entities = this.supplyItemsGrid(entities, y);
  //             this.entities$ = of(this.entities);
  //           })

  //         this.getCurrent();
  //       }
  //     }
  //   )


  // }


  getCurrent = () => {
    this.entitiesFiltered$ = of(this.entities.slice(0, this.pageSize));
  }
}

