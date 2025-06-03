import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: 'entity-type',
  standalone: true
})

export class EntityTypePipe implements PipeTransform {


  transform(value: number) {

    if (value == 0)
      return 'PJ';

    if (value == 1)
      return 'PF';

    if (value < 0 || value > 1)
      return 'invalid option.';

    return value;
  }

}
