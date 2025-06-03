import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SpinnerGComponent } from '../../spinner-g/component/spinner-g.component';
import { MatPaginatorModule } from '@angular/material/paginator';

export const importsModules: any[] = [
  CommonModule,
  MatIconModule,
  MatButtonModule,
  MatMenuModule,
  MatTooltipModule,
  MatDividerModule,
  MatCardModule,
  MatPaginatorModule,
  SpinnerGComponent
]
