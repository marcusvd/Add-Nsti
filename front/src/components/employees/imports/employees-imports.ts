

import { PhoneNumberPipe } from "shared/pipes/phone-number.pipe";
import { EmployeesListService } from "../services/employees-list.service";



export const EmployeesImports: any[] = [
]
export const EmployeesListProviders: any[] = [
  PhoneNumberPipe,
  EmployeesListService
]

