
import { map, Observable, of } from 'rxjs';

import { BaseList } from 'shared/components/list-g/extends/base-list';
import { OnClickInterface } from 'shared/components/list-g/list/interfaces/on-click-interface';
import { ListUserAccountDto } from '../dtos/list-user-account.dto';
import { FormGroup } from '@angular/forms';
import { UserAccountAuthDto } from "../../../../authentication/dtos/user-account-auth-dto";


export class ListPanelControlAdm extends BaseList {

  usersAccounts: UserAccountAuthDto[] = [];
  formMain!: FormGroup;
  entityUserAccounts$!: Observable<UserAccountAuthDto[]>;
  entities$!: Observable<ListUserAccountDto[]>;
  entities: ListUserAccountDto[] = [];
  entitiesFiltered$!: Observable<ListUserAccountDto[] | undefined>;

  sanitizeFormFields(form: FormGroup): void {
    Object.keys(form.controls).forEach(field => {
      const control = form.get(field);
      if (control instanceof FormGroup) {
        this.sanitizeFormFields(control);
      } else if (control && (control.value === null || control.value === undefined)) {
        control.setValue('');
      }
    })
  }

  labelHeaders = () => {
    return [
      { key: '', style: 'cursor: pointer;max-width:30px;' },
      { key: 'Usuário', style: 'cursor: pointer;' },
      { key: 'E-mail', style: 'cursor: pointer;' },
      { key: 'Email confirmado', style: 'cursor: pointer;' }
    ]
  }

  fieldsHeaders = () => {
    return [
      { key: 'id', style: 'max-width:30px;' },
      { key: 'userName', style: '' },
      { key: 'email', style: '' },
      { key: 'emailConfirm', style: '' },
    ]
  }

  onClickButton(field: string) {
  }

  onClickIcons(obj: OnClickInterface) {

    console.log(obj.entityId)

    if (obj.action.split('|')[0] == 'edit') {
      this.callRouter(`/users/edit-user/${obj.entityId}`);
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


  private supplyItemsGrid = (userAccountList: ListUserAccountDto[], userAccount: UserAccountAuthDto) => {
    console.log(userAccount);
    const items: ListUserAccountDto = new ListUserAccountDto();

    Object.assign(items, {

      id: {
        key: userAccount.id,
        display: 'icons',
        icons: ['edit|-|Alterar'],
        styleInsideCell: `max-width:30px; color:rgb(11, 112, 155); cursor: pointer; font-size:20px;`,
        styleCell: 'max-width:30px; display:flex; justify-content: center;'
      },

      userName: {
        key: userAccount.userName,
        styleCell: 'width:100%;',
      },
      email: {
        key: userAccount.email,
        styleCell: 'width:100%;',
      },
      emailConfirm: {
        key: userAccount.emailConfirmed ? 'SIM' : 'NÃO',
        styleInsideCell: userAccount.emailConfirmed ? ' background-color: rgb(0, 156, 222) !important;color:white; font-weight: bold;' : 'background-color: rgb(222, 52, 0) !important;color:white; font-weight: bold;',
        styleCell: 'width:100%',
      },

    })

    userAccountList.push(items);

    return userAccountList;
  }

  startSupply(url: string, id: number) {

    let entities: ListUserAccountDto[] = [];

    const usersAccounts: Observable<UserAccountAuthDto[]> = this._listGDataService.loadById$(url, id.toString());
   
    usersAccounts.pipe(map(x => {
      console.log(x)
      this.usersAccounts = x;

      x.forEach(y => {
        this.entities = this.supplyItemsGrid(entities, y)
      })

      this.entities$ = of(this.entities);

      this.entitiesFiltered$ = this.entities$;

    })).subscribe();

  }



}

