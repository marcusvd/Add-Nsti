import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';


@Component({
  selector: 'btn-g-dynamic',
  templateUrl: './btn-g-dynamic.component.html',
  styleUrls: ['./btn-g-dynamic.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
  ]
})

export class BtnGDynamicComponent {

  action: boolean = false;
  select = new FormControl();

  @Output() btn = new EventEmitter<void>();
  @Output() outAction = new EventEmitter<boolean>();

  @Input({ required: true }) name: string = '';
  @Input() actClosed: string = 'keyboard_arrow_up';
  @Input() actOpened: string = 'keyboard_arrow_down';
  @Input() btnClassList = '!bg-main-color !text-white !w-[150px]';
  @Input() isDisabled: boolean = false;

  filterMtd() {
    this.action = !this.action;
    this.outAction.emit(this.action)
  }

  btnGMtd() {
    this.btn.emit();
  }

}
