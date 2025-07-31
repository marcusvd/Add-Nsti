
import { HttpClient } from '@angular/common/http';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { map, Observable, of } from 'rxjs';
import { DeleteServices } from 'shared/components/delete-dialog/services/delete.services';
import { BaseList } from '../../../shared/components/list-g/extends/base-list';
import { ListGDataService } from '../../../shared/components/list-g/list/data/list-g-data.service';
import { OnClickInterface } from '../../../shared/components/list-g/list/interfaces/on-click-interface';
import { PhoneNumberPipe } from '../../../shared/pipes/phone-number.pipe';
import { CustomerDto } from '../components/commons-components/dtos/customer-dto';
import { EntityTypePipe } from '../components/commons-components/pipes/entity-type.pipe';
import { ListCustomerDto } from '../components/list/dtos/list-customer.dto';
import { CustomerListService } from '../services/customer-list.service';


export class ListControlCustomers extends BaseList {

  entities$!: Observable<ListCustomerDto[]>;
  entities: ListCustomerDto[] = [];
  entitiesFiltered$!: Observable<ListCustomerDto[] | undefined>;
  entitiesFiltered: ListCustomerDto[] = [];
  length = 0;
  showHideFilter = false;
  term!: string;
  controllerUrl: string = "environment._CUSTOMERS.split('/')[4]";
  backEndUrl: string = `${this.controllerUrl}/GetAllCustomersPagedAsync`;

  constructor(
    override _router: Router,
    public _http: HttpClient,
    protected _entityTypePipe: EntityTypePipe,
    protected _phoneNumberPipe: PhoneNumberPipe,
    protected _customerServices: CustomerListService,
    protected _deleteServices: DeleteServices,
  ) {
    super(
      new ListGDataService(),
      _router,
    )
  }
  // constructor(
  //   override _router: Router,
  //   public _http: HttpClient,
  //   protected _entityTypePipe: EntityTypePipe,
  //   protected _phoneNumberPipe: PhoneNumberPipe,
  //   protected _customerServices: CustomerListService,
  //   protected _deleteServices: DeleteServices,
  // ) {
  //   super(
  //     new ListGDataService(),
  //     _router,
  //   )
  // }

  labelHeadersMiddle = () => {
    return [
      { key: '', style: 'cursor: pointer;' },
      { key: 'Nome', style: 'cursor: pointer;' },
      { key: 'Entidade', style: 'cursor: pointer;' },
      { key: 'Cel', style: 'cursor: pointer;' }
    ]
  }

  fieldsHeadersMiddle = () => {
    return [
      { key: 'id', style: '' },
      { key: 'name', style: '' },
      { key: 'entityTypeToView', style: '' },
      { key: 'contact', style: '' }
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

  onClickOrderByFields(field: string, entities$: Observable<ListCustomerDto[] | undefined>) {

    switch (field) {
      case 'name':

        this.entities$ = this.orderByFrontEnd(entities$, { key: field, value: '' }) as Observable<ListCustomerDto[]>;
        break;

      case 'assured':
        this.entities$ = this.orderByFrontEnd(entities$, { key: field, value: '' }) as Observable<ListCustomerDto[]>;
        break;

      case 'responsible':
        this.entities$ = this.orderByFrontEnd(entities$, { key: field, value: 0 }) as Observable<ListCustomerDto[]>;
        break;
    }

  }

  onClickButton(field: string) {
  }

  onClickIcons(obj: OnClickInterface) {
    if (obj.action.split('|')[0] == 'edit')
      this.callRouter(`/customers/edit/${obj.entityId}`);

    if (obj.action.split('|')[0] == 'delete')
      this.deleteFake(obj.entityId);



    // ex_callRouteWithObject('/side-nav/stock-product-router/detailed-product', this.products.find(x => x.id == obj.entityId), this._router)
  }


  deleteFake = (id: number) => {
    const entity = this.entities.find(x => x.id.key == id.toString()) ?? new ListCustomerDto();

    const result = this._deleteServices.delete(parseInt(entity.id.key), entity.name.key)
    // const result = this._deleteServices.delete(this.entities.find(x => x.id.key == id.toString()))

    result.subscribe(result => {
      if (result.id != null) {
        // this._customerServices.deleteFakeDisable(result.id.key);

        this.entitiesFiltered$ = this.entitiesFiltered$.pipe(
          map(x => x?.filter(y => y.id.key != result.id))
        )
      }

    })
  }


  supplyItemsGrid = (customerList: ListCustomerDto[], customer: CustomerDto) => {

    const items: ListCustomerDto = new ListCustomerDto();

    Object.assign(items, {

      id: {
        key: customer.id.toString(),
        display: 'icons',
        // icons: ['edit', 'delete'],
        icons: ['edit|', 'delete|color:rgb(158, 64, 64);margin-left:10px;'],
        styleInsideCell: `color:rgb(11, 112, 155); cursor: pointer; font-size:20px;`,
        styleCell: '',
        route: ''
      },

      name: {
        key: customer.name,
        styleCell: 'width:100%;',

      },

      entityType: {
        key: customer.entityType,
        styleCell: 'width:100%;',
      },
      entityTypeToView: {
        key: this._entityTypePipe.transform(customer.entityType),
        styleCell: 'width:100%;',
      },

      contact: {
        key: this._phoneNumberPipe.transform(customer.contact.cel),
        styleCell: 'width:100%;',
      }
    })

    customerList.push(items);

    return customerList;
  }




  // startSupply(): Subscription | undefined {

  //   let entities: CustomerListDto[] = [];

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


  startSupply() {

    let entities: ListCustomerDto[] = [];

    const customers: Observable<CustomerDto[]> = of(this._customerServices.getCustomersMoc());

    return customers.pipe(map(x => {

      x.forEach(y => {
        this.entities = this.supplyItemsGrid(entities, y);
        this.entities$ = of(this.entities);
        this.entitiesFiltered$ = this.entities$;
      })

    })).subscribe();

  }


  getCurrent = () => {
    this.entitiesFiltered$ = of(this.entities.slice(0, this.pageSize));
  }
}

