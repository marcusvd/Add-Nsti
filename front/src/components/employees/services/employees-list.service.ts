import { Injectable } from '@angular/core';
import { DataGeneratorService } from '../../../shared/helpers/data-generator-moc/data-generator.service'
import { EmployeeDto } from '../components/commons-components/dtos/employees-dto';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeesListService {

  constructor(private dataGeneratorMocServices: DataGeneratorService) { }


  getData(isProd: boolean):Observable<EmployeeDto[]>  {
    if (isProd)
      return this.getEmployees();
    else
      return this.devModeGetDataFake();
  }


  devModeGetDataFake(): Observable<EmployeeDto[]> {
    return of(this.dataGeneratorMocServices.fakeEmployees)
  }

  getEmployees() {
    return of([])
  }


}
