import { Component, EventEmitter, Input, Output } from '@angular/core';

import { BaseForm } from 'shared/extends/forms/base-form';
import { ImportsTimeG } from './imports/imports-time-g';
import { FormGroup } from '@angular/forms';


@Component({
  selector: 'time-g',
  standalone: true,

  imports: [ImportsTimeG],

  templateUrl: './time-g.component.html',
  styleUrl: './time-g.component.scss'
})

export class TimeGComponent extends BaseForm  {

  @Input() formFieldCssClass: string = 'w-full flex justify-center';
  @Input({required:true}) override formMain = new  FormGroup({});
  @Input({required:true}) formCtrlName: string = 'start';
  @Input({required:true}) lblTimer: string = 'Inicio';
  @Input({required:true}) timeFormat_24_12: number = 24;

  @Output() outTimeSelected = new EventEmitter<string>();

  selectedTime: string ='00:00';

  onTimeSelected = (time: string): void => {

    this.selectedTime = time
    this.outTimeSelected?.emit(this?.selectedTime);
  };

}

