import { Component, Input } from '@angular/core';
import { DefaultComponentImports } from './imports/default-component.imports';

@Component({
  selector: 'default-component',
  standalone: true,
  imports: [
    DefaultComponentImports
  ],
  templateUrl: './default-component.html',
  styleUrl: './default-component.scss'
})
export class DefaultComponent {

  @Input({ required: true }) digitTitleComp!: string;
  @Input({ required: true }) textTitleComp!: string;

  @Input() main_container: string = 'space-around mb-14';
  @Input() hideDivider: boolean = false;

  @Input({ required: true }) textSubTitleComp!: string;
  @Input({ required: true }) iconSubTitleComp!: string;

}



