import { ItemsInterface } from "../../../../shared/components/list-g/list/interfaces/items-interface";

export class ListTemplateDto {
  id:ItemsInterface;
  status:ItemsInterface;
  expiresView:ItemsInterface;
  category:ItemsInterface;
  name:ItemsInterface;
  installmentsQuantity:ItemsInterface;
  installmentPrice:ItemsInterface;
  userId:ItemsInterface;
  user:ItemsInterface;
  companyId:ItemsInterface;
  company:ItemsInterface;
  categoryExpenseId:ItemsInterface;
  categoryExpense:ItemsInterface;
  subcategoryExpense:ItemsInterface;
  subcategoryExpenseId:ItemsInterface;
  start:ItemsInterface;
  end:ItemsInterface;
  totalPriceToBePaid:ItemsInterface;
  totalPriceFinancingOrLoan:ItemsInterface;
  totalPriceInterest:ItemsInterface;
  totalPercentageInterest:ItemsInterface;
  wasPaid:ItemsInterface | string;
  wasPaidView:ItemsInterface | string;
  deleted:ItemsInterface;
  registered:ItemsInterface;
  description:ItemsInterface;
  linkCopyBill:ItemsInterface;
  uSERLinkCopyBill:ItemsInterface;
  pASSLinkCopyBill:ItemsInterface;
  expires:ItemsInterface;
  amountAlreadyPaid:ItemsInterface;

}
