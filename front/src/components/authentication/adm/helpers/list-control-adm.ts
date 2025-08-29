
import { PageEvent } from '@angular/material/paginator';
import { map, Observable, of } from 'rxjs';
import { BaseList } from '../../../../shared/components/list-g/extends/base-list';
import { OnClickInterface } from '../../../../shared/components/list-g/list/interfaces/on-click-interface';

import { BusinessAuth } from 'components/authentication/dtos/business-auth';
import { ListAdmDto } from '../list/dtos/list-adm.dto';
import { inject } from '@angular/core';
import { CompanyAuth } from 'components/authentication/dtos/company-auth';


export class ListControlAdm extends BaseList {


  business!: BusinessAuth;

  entityBusiness$!: Observable<BusinessAuth>;

  entities$!: Observable<ListAdmDto[]>;
  entities: ListAdmDto[] = [];
  entitiesFiltered$!: Observable<ListAdmDto[] | undefined>;
  // entitiesFiltered: ListAdmDto[] = [];
  // length = 0;
  // showHideFilter = false;
  // term!: string;
  // controllerUrl: string = "environment._CUSTOMERS.split('/')[4]";
  // backEndUrl: string = `${this.controllerUrl}/GetAllCustomersPagedAsync`;


  // private _admService = inject(AdmService);
  // private _fb = inject(FormBuilder);
  // private _warningsService = inject(WarningsService);
  // private _snackBar = inject(MatSnackBar)

  constructor(
    // override _router: Router,
    // public _http: HttpClient,
    // protected _phoneNumberPipe: PhoneNumberPipe,
    // protected _deleteServices: DeleteServices,
  ) {
    super(
      //
      //
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
  //
  //
  //   )
  // }

  labelHeaders = () => {
    return [
      { key: '', style: 'cursor: pointer;' },
      { key: 'Empresa', style: 'cursor: pointer;' },
      { key: 'Usuários', style: 'cursor: pointer;' },
      // { key: 'Cel', style: 'cursor: pointer;' }
    ]
  }

  fieldsHeaders = () => {
    return [
      { key: 'id', style: '' },
      { key: 'name', style: '' },
      { key: 'usersAmount', style: '' },
      // { key: 'contact', style: '' }
    ]
  }

  // onPageChange($event: PageEvent) {

  //   if ($event.previousPageIndex ?? 0 < $event.pageIndex)
  //     this.entitiesFiltered$ = of(this.pageChange(this.entitiesFiltered, $event)?.filter(x => x != null))

  //   else if ($event.previousPageIndex ?? 0 > $event.pageIndex)
  //     this.entitiesFiltered$ = of(this.pageChange(this.entitiesFiltered, $event)?.filter(x => x != null))

  //   if (this.term) {
  //     this.entitiesFiltered$ = of(this.pageChange(this.searchListEntities(this.entitiesFiltered, this.term), $event)?.filter(x => x != null))
  //     this.length = this.searchListEntities(this.entitiesFiltered, this.term).length
  //   }

  // }

  // onClickOrderByFields(field: string, entities$: Observable<ListAdmDto[] | undefined>) {

  //   switch (field) {
  //     case 'name':

  //       this.entities$ = this.orderByFrontEnd(entities$, { key: field, value: '' }) as Observable<ListAdmDto[]>;
  //       break;

  //     case 'assured':
  //       this.entities$ = this.orderByFrontEnd(entities$, { key: field, value: '' }) as Observable<ListAdmDto[]>;
  //       break;

  //     case 'responsible':
  //       this.entities$ = this.orderByFrontEnd(entities$, { key: field, value: 0 }) as Observable<ListAdmDto[]>;
  //       break;
  //   }

  // }

  onClickButton(field: string) {
  }

  onClickIcons(obj: OnClickInterface) {

    console.log(obj.action.split('|')[0])

    if (obj.action.split('|')[0] == 'edit')
      this.callRouter(`/customers/edit/${obj.entityId}`);

    if (obj.action.split('|')[0] == 'delete')
      this.deleteFake(obj.entityId);


    if (obj.action.split('|')[0] == 'person_add') {
      this.callRouter(`/users/add-user-company/${obj.entityId}`);
    }

  }


  deleteFake = (id: number) => {
    // const entity = this.entities.find(x => x.id.key == id.toString()) ?? new ListAdmDto();

    // const result = this._deleteServices.delete(parseInt(entity.id.key), entity.name.key)
    // // const result = this._deleteServices.delete(this.entities.find(x => x.id.key == id.toString()))

    // result.subscribe(result => {
    //   if (result.id != null) {
    //     // this._customerServices.deleteFakeDisable(result.id.key);

    //     this.entitiesFiltered$ = this.entitiesFiltered$.pipe(
    //       map(x => x?.filter(y => y.id.key != result.id))
    //     )
    //   }

    // })
  }


  supplyItemsGrid = (companyList: ListAdmDto[], company: CompanyAuth) => {

    const items: ListAdmDto = new ListAdmDto();

    Object.assign(items, {

      id: {
        key: company.id,
        display: 'icons',
        icons: ['edit|', 'delete|color:rgb(158, 64, 64);margin-left:10px;', 'person_add|'],
        styleInsideCell: `color:rgb(11, 112, 155); cursor: pointer; font-size:20px;`,
        styleCell: '',
        route: ''
      },

      name: {
        key: company.name,
        styleCell: 'width:100%;',
      },
      usersAmount: {
        key: company.companyUserAccounts.filter(c => c.companyAuthId == company.id).length,

        styleCell: 'width:100%;',
      }
    })

    companyList.push(items);

    return companyList;
  }

  startSupply(url: string, id: number) {

    let entities: ListAdmDto[] = [];

    const business: Observable<BusinessAuth> = this._listGDataService.loadById$(url, id.toString());

    this.entityBusiness$ = business;

    business.pipe(map(x => {
      this.business = x;

      x?.companies.forEach(y => {
        const amountUsers = y.companyUserAccounts;
        // const amountUsers = y.companyUserAccounts.filter(c => c.companyAuthId == y.id);
        this.entities = this.supplyItemsGrid(entities, y)
        this.entities$ = of(this.entities);

        console.log(amountUsers)
        this.entitiesFiltered$ = this.entities$;
      })

    })).subscribe();

  }



}

