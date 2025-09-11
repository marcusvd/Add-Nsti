import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';


import { BaseForm } from 'shared/inheritance/forms/base-form';

@Component({
  selector: 'email-field',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule
  ],
  templateUrl: './email.component.html',
  styleUrl: './email.component.scss'
})
export class EmailComponent extends BaseForm {

  @Input() override formMain = new FormGroup({});


}
