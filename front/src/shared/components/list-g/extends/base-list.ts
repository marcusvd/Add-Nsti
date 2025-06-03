import { Component, ViewChild } from '@angular/core';
import { MatPaginator as MatPaginator, PageEvent as PageEvent } from '@angular/material/paginator';
import { NavigationExtras, Router } from "@angular/router";
import * as diacritics from 'diacritics';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { ListGDataService } from "../list/data/list-g-data.service";
import { FieldsInterface } from '../list/interfaces/fields-interface';
import { OrderbyInterface } from '../list/interfaces/orderby-interface';

@Component({
  selector: 'list-g',
  template: `
  `
})

export class BaseList {

  companyId = localStorage.getItem('companyId')
    ? JSON.parse(localStorage.getItem('companyId')!)
    : '';

  userId = localStorage.getItem('userId')
    ? JSON.parse(localStorage.getItem('userId')!)
    : '';


  // minValue = new Date('0001-01-01T00:00:00.000Z');
  minDate = new Date('0001-01-01T00:00:00');
  currentDate = new Date();
  currentDateWithoutHours = this.currentDate.setHours(0, 0, 0, 0)
  screenWidth: number = window.innerWidth;
  fields: FieldsInterface[] = []
  pageSize: number = 20;
  @ViewChild('paginatorAbove') paginatorAbove!: MatPaginator
  @ViewChild('paginatorBelow') paginatorBelow!: MatPaginator


  constructor(
    protected _listGDataService?: ListGDataService,
    protected _router?: Router,
  ) { }

  pageChange(entities: any[], $event: PageEvent) {

    // this.paginatorAbove.pageIndex = $event.pageIndex;
    // this.paginatorBelow.pageIndex = $event.pageIndex;

    const pageSize = $event.pageSize;
    const startIndex = $event.pageIndex * pageSize;
    const endIndex = startIndex + pageSize;

    if ($event.previousPageIndex ?? 0 < $event.pageIndex)
      return entities.slice(startIndex, endIndex);

    else if ($event.previousPageIndex ?? 0 > $event.pageIndex)
      return entities.slice(startIndex, endIndex);

    return null;
  }

  screen(event?: Event) {
    const target = event?.target as Window;
    this.screenWidth = target.innerWidth;
    return this.screenWidth
  }

  searchListEntities(entities: any[], term: string): any[] {
    const entitiesToFilter = entities;
    let result: any[] = [];

    result = entitiesToFilter.filter(entity =>
      Object.values(entity).some((value: any) => {

        if (value && typeof value === 'object' && 'key' in value) {
          const stringValue = this.removeAccentsSpecialCharacters(value.key);
          return typeof stringValue === 'string' &&
            stringValue.toLowerCase().replace('.', '').replace(',', '').includes(this.removeAccentsSpecialCharacters(term.toLowerCase()));
        }

        return false;
      })
    );

    return result;
  }

  isdescending = true;
  orderByFrontEnd(entities$: Observable<any[] | undefined>, field: OrderbyInterface) {
    this.isdescending = !this.isdescending;

    const entityFieldProperty = field.key;
    const valueType = typeof field.value;

    if (valueType === 'string') {
      if (this.isdescending)
        return entities$?.pipe(map(h => h?.sort((x, y) => x[entityFieldProperty].key.localeCompare(y[entityFieldProperty].key))));
      else
        return entities$?.pipe(map(h => h?.sort((x, y) => y[entityFieldProperty].key.localeCompare(x[entityFieldProperty].key))));
    }

    if (valueType === 'number') {
      return entities$?.pipe(map(h => h?.sort((x, y) => {
        if (this.isdescending) {
          const numberX: number = this.removeNonNumericAndConvertToNumber(x[entityFieldProperty].key);
          const numberY: number = this.removeNonNumericAndConvertToNumber(y[entityFieldProperty].key);
          return numberX - numberY;
        }
        else {
          const numberX: number = this.removeNonNumericAndConvertToNumber(x[entityFieldProperty].key);
          const numberY: number = this.removeNonNumericAndConvertToNumber(y[entityFieldProperty].key);
          return numberY - numberX;
        }
      })))
    }

    if (valueType === 'object') {
      return entities$?.pipe(map(h => h?.sort((x, y) => {
        if (this.isdescending)
          return new Date(x[entityFieldProperty].key).getTime() - new Date(y[entityFieldProperty].key).getTime();
        else
          return new Date(y[entityFieldProperty].key).getTime() - new Date(x[entityFieldProperty].key).getTime();
      })))
    }
    return null;
  }

  removeNonNumericAndConvertToNumber(str: string): number {
    return +str.replace(/\D/g, '');
  }

  removeAccentsSpecialCharacters(value: string): string {
    if (typeof value !== 'string') {
      return '';
    }
    const noAccents = diacritics.remove(value);//remove accents
    return noAccents.replace(/[^\w\s]/gi, ''); //remove special characters
  }

  callRouter(url: string, entity?: any) {

    const objectRoute: NavigationExtras = {
      state: {
        entity
      }
    };

    this._router?.navigate([url], objectRoute);
  }



}

