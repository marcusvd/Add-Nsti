import { Component, inject, OnInit } from '@angular/core';
import { ImportsTimeInterval } from './imports/imports-time-interval';

import { MatDatepickerModule } from '@angular/material/datepicker'; // Necessário para o timepicker
import { MatNativeDateModule } from '@angular/material/core'; // Necessário para o timepicker
import { MatFormFieldModule } from '@angular/material/form-field';

import { CommonModule } from '@angular/common';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { BaseForm } from 'shared/inheritance/forms/base-form';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';


@Component({
  selector: 'time-interval-g',
  standalone: true,

  imports: [
    CommonModule,
    MatInputModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCardModule,
    NgxMaterialTimepickerModule
  ],

  templateUrl: './time-interval-g.component.html',
  styleUrl: './time-interval-g.component.scss'
})

export class TimeIntervaGComponent extends BaseForm implements OnInit {

  ngOnInit(): void {
    this.formMain = this._fb.group({
      start: ['', []],
      end: ['', []]
    })
  }

  startTime!: string;
  endTime!: string;

  private _fb = inject(FormBuilder)

  onTimeSelected = (time: any, type: 'start' | 'end'): void =>type === 'start' ? this.startTime = time : this.endTime = time;

}

