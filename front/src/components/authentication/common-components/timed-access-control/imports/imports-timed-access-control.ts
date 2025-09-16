import { MatDatepickerModule } from '@angular/material/datepicker'; // Necessário para o timepicker
import { MatNativeDateModule } from '@angular/material/core'; // Necessário para o timepicker
import { MatFormFieldModule } from '@angular/material/form-field';

import { CommonModule } from '@angular/common';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { NgxMaskModule } from 'ngx-mask';
import { TimeGComponent } from 'shared/components/time-g/time-g.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BtnGComponent } from 'shared/components/btn-g/btn-g.component';



export const ImportsTimedAccessControl: any[] = [
  CommonModule,
  MatInputModule,
  ReactiveFormsModule,
  MatNativeDateModule,
  MatFormFieldModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatCardModule,
  NgxMaterialTimepickerModule,
  NgxMaskModule,
  TimeGComponent,
  BtnGComponent
]

