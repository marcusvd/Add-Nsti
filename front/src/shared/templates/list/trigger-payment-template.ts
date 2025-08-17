import { NavigationExtras, Router } from "@angular/router";

import { PtBrCurrencyPipe } from "src/shared/pipes/pt-br-currency.pipe";
import { PtBrDatePipe } from "src/shared/pipes/pt-br-date.pipe";
import { FinancingsLoansExpensesDto } from "../../dto/financings-loans-expenses-dto";


export class TriggerPaymentTemplate {

  constructor(

    private _router: Router,
    private _ptBrDatePipe: PtBrDatePipe,
    private _ptBrCurrencyPipe: PtBrCurrencyPipe,
  ) {
  }

  public entityToPay: FinancingsLoansExpensesDto = null;

  callRoute(entity: FinancingsLoansExpensesDto) {

    const objectRoute: NavigationExtras = {
      state: {
        entity: {
       //   'screenInfoFields': this.makeInfoScreenData(entity),
          'entity': entity,
          hideShowScreenDataInfo: true,
        }
      }
    };

    this._router.navigate(['/financial/payment-financings-loans'], objectRoute);
  }

}
