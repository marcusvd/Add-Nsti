import { Component, inject, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Base } from 'shared/extends/forms/base';
import { ListGDataService } from "../list/data/list-g-data.service";
import { FieldsInterface } from '../list/interfaces/fields-interface';
import { OrderbyInterface } from '../list/interfaces/orderby-interface';

@Component({
  selector: 'base-list',
  template: `
  `
})

export class BaseList extends Base {

  _listGDataService = inject(ListGDataService);
  _router = inject(Router);
  screenWidth: number = window.innerWidth;
  fields: FieldsInterface[] = []
  pageSize: number = 20;
  @ViewChild('paginatorAbove') paginatorAbove!: MatPaginator
  @ViewChild('paginatorBelow') paginatorBelow!: MatPaginator


  pageChange(entities: any[], $event: PageEvent) {

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

}

