import { EventEmitter } from "@angular/core";
import { FormControl } from "@angular/forms";

export const ex_formControlSearch = new FormControl('');

export const ex_search = (input: string, outFieldSearch:EventEmitter<string>, funcFilterTerm: (term:string) => string) => {
  outFieldSearch.emit(funcFilterTerm(input.toLowerCase()));
}
