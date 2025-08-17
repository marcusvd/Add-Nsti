
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatRadioModule } from '@angular/material/radio';
import { RouterModule } from '@angular/router';


// import { BtnGComponent } from '../../../../../../../shared/components/btn-g/btn-g.component';
// import { TitleComponent } from '../../../../../../../shared/components/title/default-title/title.component';
// import { ListGComponent } from '../../../../../../../shared/components/list-g/list/list-g.component';
// import { PtBrDatePipe } from '../../../../../../../shared/pipes/pt-br-date.pipe';
// import { PtBrCurrencyPipe } from '../../../../../../../shared/pipes/pt-br-currency.pipe';
import { ListFinancingsLoansExpensesService } from '../services/list-financings-loans-expenses.service';
import { MatDividerModule } from '@angular/material/divider';

export const ImportsListTemplate: any[] = [
  CommonModule,
  FormsModule,
  MatCardModule,
  MatPaginatorModule,
  MatButtonModule,
  MatMenuModule,
  RouterModule,
  MatIconModule,
  MatCheckboxModule,
  MatRadioModule,
  MatDividerModule,
  // TitleComponent,
  // BtnGComponent,
  // ListGComponent
]

export const ProvidersListTemplate: any[] = [
  // PtBrDatePipe,
  // PtBrCurrencyPipe,
  ListFinancingsLoansExpensesService,
]
