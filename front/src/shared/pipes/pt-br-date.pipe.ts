import { Pipe, PipeTransform } from "@angular/core";
import * as moment from "moment";

@Pipe({
  name: 'ptBrDate',
  standalone: true
})
export class PtBrDatePipe implements PipeTransform {
  transform(value: any, ...args: any[]) {
    const formattedDate = moment(value).format('DD/MM/YYYY');
  //  const minValue = new Date('0001-01-01T00:00:00');
   const minValue = new Date('0001-01-01T00:00:00.000Z');;
    switch (args[0]) {
      case 'Date':
        return formattedDate;
      case 'wasPaidCollectDeliver': {
        if (new Date(value).getFullYear() == minValue.getFullYear())
          return 'Aberto';
        else
        return formattedDate;
      }
      //const formattedDate = moment(value).format('DD/MM/YYYY');
    }
    return value;
  }
}
