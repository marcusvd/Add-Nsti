

import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';


import { CurrencyMaskModule } from 'ng2-currency-mask';
import { BtnGComponent } from '../../../../shared/components/btn-g/btn-g.component';
import { TitleComponent } from '../../../../shared/components/title/default-title/title.component';



export const DefaultComponentImports: any[] = [
  CommonModule,
  MatFormFieldModule,
  MatInputModule,
  MatIconModule,
  MatCheckboxModule,
  MatCardModule,
  ReactiveFormsModule,
  MatSelectModule,
  MatDatepickerModule,
  CurrencyMaskModule,
  MatDividerModule,
  TitleComponent,
  BtnGComponent
]
