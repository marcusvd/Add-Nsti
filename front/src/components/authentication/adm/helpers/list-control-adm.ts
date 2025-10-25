
import { map, Observable, of } from 'rxjs';
import { BaseList } from '../../../../shared/components/list-g/extends/base-list';
import { OnClickInterface } from '../../../../shared/components/list-g/list/interfaces/on-click-interface';

import { inject } from '@angular/core';
import { BusinessAuth } from 'components/authentication/dtos/business-auth';
import { CompanyAuth } from "components/company/dtos/company-auth";
import { TruncatePipe } from 'shared/pipes/truncate.pipe';
import { ListAdmDto } from '../list/dtos/list-adm.dto';


export class ListControlAdm extends BaseList {

  private _truncate = inject(TruncatePipe);
  entityBusiness$!: Observable<BusinessAuth>;
  entities$!: Observable<ListAdmDto[]>;
  entitiesFiltered$!: Observable<ListAdmDto[] | undefined>;
  business: BusinessAuth = new BusinessAuth();
  entities: ListAdmDto[] = [];

  labelHeaders = () => {
    return [
      { key: '', style: 'cursor: pointer;' },
      { key: 'Empresa', style: 'cursor: pointer;' },
      { key: 'Usuários', style: 'cursor: pointer;' },
    ]
  }

  fieldsHeaders = () => {
    return [
      { key: 'id', style: '' },
      { key: 'name', style: '' },
      { key: 'usersAmount', style: '' },
    ]
  }

  onClickButton(field: string) {
  }

  onClickIcons(obj: OnClickInterface) {

    console.log(obj.action.split('|')[0])

    if (obj.action.split('|')[0] == 'edit') {
      this.callRouter(`users/edit-company/${obj.entityId}`);
    }
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
        icons: ['edit|-|Alterar', 'delete|color:rgb(158, 64, 64);margin-left:10px;|Excluir', 'person_add|-|Adicionar usuário'],
        styleInsideCell: `color:rgb(11, 112, 155); cursor: pointer; font-size:20px;`,
        iconsLabels: ['edit|-|Alterar', 'delete|color:rgb(158, 64, 64);margin-left:10px;', 'person_add|'],
        styleCell: '',
        route: ''
      },

      name: {
        key: this._truncate.transform(company.name, 33),
        styleCell: 'width:100%;',
      },
      usersAmount: {
        keyN: company.companyUserAccounts.filter(c => c.companyAuthId == company.id).length,
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

        this.entities = this.supplyItemsGrid(entities, y)
        this.entities$ = of(this.entities);

        this.entitiesFiltered$ = this.entities$;
      })

    })).subscribe();

  }



}

