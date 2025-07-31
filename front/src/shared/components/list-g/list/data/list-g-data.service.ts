import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { FormControl } from "@angular/forms";
import { BehaviorSubject } from "rxjs";
// import { PaginationDto } from "src/shared/entities-dtos/pagination-dto";
import { BackEndService } from "../../../../../shared/services/back-end/backend.service";

@Injectable({ providedIn: 'root' })
export class ListGDataService extends BackEndService<any> {

  private entitiesFromDb = new BehaviorSubject<any[]>([]);
  entities$ = this.entitiesFromDb.asObservable();

  lengthPaginator = new BehaviorSubject<number>(0);

  // totalEntities: number = 0;

  // pgIsBackEnd: boolean = false;

  // entitiesFromDbToMemoryTotal = new BehaviorSubject<any[]>([]);
  // entitiesFromDbToMemory = new BehaviorSubject<any[]>([]);
  // entitiesFromDbToMemory$ = this.entitiesFromDbToMemory.asObservable();

  constructor( ) {
    super()
    // super(_http, null)

  }
  // constructor(
  //   override _http: HttpClient
  // ) {
  //   super(_http, 'environment._BACK_END_ROOT_URL')
  //   // super(_http, null)

  // }

  paramsTo(pageIndex: number, pgSize: number, predicate?: number, $event?: FormControl, terms?: any, orderBy?: any) {
    let params = new HttpParams();
    params = params.append('pgnumber', pageIndex);
    params = params.append('pgsize', pgSize);
    params = params.append('predicate', predicate ?? JSON.parse(localStorage.getItem('companyId') ?? ''));
    params = params.append('term', $event?.value ?? '');
    params = params.append('filterterms', JSON.stringify(terms) ?? '');
    params = params.append('orderby', JSON.stringify(orderBy) ?? '');
    return params;
  }

  //BackEnd below
  pagination: any = {};

  getAllEntitiesPaged(backEndUrl: string, params: HttpParams) {
    this.loadAllPaged$<any[]>(backEndUrl, params)
      .subscribe((entities: any) => {
        this.pagination = JSON.parse(entities.headers.get('Pagination'));
        this.lengthPaginator.next(this.pagination.totalCount);
        this.entitiesFromDb.next(entities.body);
      })
  }

  // searchQueryHendler(backEndUrl?: string, params: HttpParams) {
  //   this.loadAllPaged$<any[]>(backEndUrl ?? '', params).subscribe(
  //     (x: any) => {
  //       x);
  //       // this.pagination = JSON.parse(x.headers.get('Pagination'));
  //       // this.entitiesFromDb.next(x.body);
  //       // this.lengthPaginator.next(this.pagination.totalCount);
  //     }
  //   )
  // }
  //BackEnd above


  // getAllEntitiesNoPagedWithParams(backEndUrl: string, params: HttpParams) {
  //   this.loadAllWithParams$<any[]>(backEndUrl, params)
  //     .subscribe((entities: any) => {
  //       this.entitiesFromDbToMemory.next(entities);
  //     })
  // }

  getAllEntitiesInMemoryPaged$(backEndUrl: string, id: string) {
    return this.loadById$<any[]>(backEndUrl, id);
  }
  getAllEntitiesInMemoryPaged(backEndUrl: string, id: string) {
    this.loadById$<any[]>(backEndUrl, id)
      ?.subscribe((entities: any) => {
        this?.entitiesFromDb?.next(entities);
      })
  }

  // searchQueryHendler($event: FormControl, backEndUrl: string, params: HttpParams) {
  //   this.queryField = $event;
  //   this.queryField.valueChanges.pipe(
  //     map(x => x.trim()),
  //     debounceTime(500),
  //     distinctUntilChanged(),
  //     switchMap(() => this.loadAllPaged$<any[]>(backEndUrl, params)),
  //   ).subscribe(
  //     (x:any) => {

  //       this.entitiesFromDb.next(x.body);
  //       this.lengthPaginator.next(x.body.lengthPaginator);
  //     }
  //   );
  // }

}

// @Injectable()
// export class GetTotalEntitiesResolver extends BackEndService<any> implements Resolve<number> {

//   constructor(
//     override _http: HttpClient,
//   ) { super(_http, environment._BACK_END_ROOT_URL) }

//   resolve(
//     route: ActivatedRouteSnapshot,
//     state: RouterStateSnapshot
//   ): Observable<any>{
//     let id = route.params['id'];
//     return this.loadById$<number>(route.data['url'],id)
//   }
// }
