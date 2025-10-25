import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';


import { BaseForm } from 'shared/extends/forms/base-form';

@Component({
  selector: 'password-field',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule
  ],
  templateUrl: './password.component.html',
  styleUrl: './password.component.scss'
})
export class PasswordFieldComponent extends BaseForm {

  @Input() override formMain = new FormGroup({});
  @Output() pwdField = new EventEmitter<string>();
  @Input() fieldLabel!: string;
  @Input() formCtrlName!: string;
  @Input() errorMsgFieldInvalid!: string;

  pwdType: string = 'password';
  pwdIcon: string = 'visibility_off';


  outPwdField($event: any) {

    const event = $event as InputEvent

    this.pwdField.emit(event.data ?? 'INVALID');

    console.log(this.formMain?.get('confirmPassword'))
  }

  pwdHideShow() {
    if (this.pwdType === 'password') {
      this.pwdType = 'text';
      this.pwdIcon = 'visibility';
    } else {
      this.pwdType = 'password';
      this.pwdIcon = 'visibility_off';
    }
  }


}
