
import { MatDatepickerModule } from '@angular/material/datepicker'; // Necessário para o timepicker
import { MatNativeDateModule } from '@angular/material/core'; // Necessário para o timepicker
import { NativeDateAdapter } from '@angular/material/core';


export const ImportsTimeInterval: any[] = [
  MatDatepickerModule,
  MatNativeDateModule,
  NativeDateAdapter
]

