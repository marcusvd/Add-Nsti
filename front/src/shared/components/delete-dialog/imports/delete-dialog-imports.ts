
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogActions, MatDialogClose, MatDialogModule, MatDialogTitle } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';


import { BtnGComponent } from '../../btn-g/btn-g.component';

export const DeleteGDialogImports: any[] = [
  CommonModule,
  MatDialogModule,
  MatButtonModule,
  MatCardModule,
  MatButtonModule,
  MatIconModule,
  MatDialogActions,
  MatDialogClose,
  MatDialogTitle,
  BtnGComponent
]
