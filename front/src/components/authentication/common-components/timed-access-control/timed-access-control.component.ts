import { Component, inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { BaseForm } from 'shared/inheritance/forms/base-form';
import { ImportsTimedAccessControl } from './imports/imports-timed-access-control';
import { AccountService } from 'components/authentication/services/account.service';
import { TimedAccessControlDto } from 'components/authentication/dtos/date-time-access-control-dto';
import { ResponseIdentiyApiDto } from 'components/authentication/dtos/response-identiy-api-dto';
import { TimedAccessControlStartEndPostDto } from 'components/authentication/dtos/date-time-access-control-start-end-post-dto';


@Component({
  selector: 'timed-access-control',
  standalone: true,

  imports: [
    ImportsTimedAccessControl
  ],

  templateUrl: './timed-access-control.component.html',
  styleUrl: './timed-access-control.component.scss'
})

export class TimedAccessControlComponent extends BaseForm implements OnInit {

  @Input({ required: true }) usrId!: number;
  private _fb = inject(FormBuilder);
  private _accountService = inject(AccountService);

  startTime!: string;
  endTime!: string;

  ngOnInit(): void {
    this._accountService.getTimedAccessControlAsync$(this.usrId)
      .subscribe(
        {
          next: ((x: TimedAccessControlDto) => {
            this.formLoad(x);
          })
        }
      )
  }

  formLoad(x: TimedAccessControlDto) {


    const start = new Date(x.start);
    const end = new Date(x.end);

    const H_M_start = start.toLocaleTimeString([], {
      hour: '2-digit',
      minute: '2-digit'
    })

    const H_M_end = end.toLocaleTimeString([], {
      hour: '2-digit',
      minute: '2-digit'
    })

    this.formMain = this._fb.group({
      userId: [this.usrId ?? x.id, [Validators.required]],
      start: [H_M_start ?? '00:00', [Validators.minLength(5), Validators.maxLength(5)]],
      end: [H_M_end ?? '00:00', [Validators.minLength(5), Validators.maxLength(5)]]
    })
  }

  onTimeSelectedStart = (time: string): void => {
    if (time === '')
      this.formMain.get('start')?.patchValue({ start: '00:00' });
  }

  onTimeSelectedEnd = (time: string): void => {
    if (time === '')
      this.formMain.get('end')?.patchValue({ start: '00:00' });
  }

  get formFieldStartValue(): string {
    return this.formMain.get('start')?.value;
  }

  get formFieldEndValue(): string {
    return this.formMain.get('end')?.value;
  }


  save() {

    if (this.alertSave(this.formMain)) {

      const toUpdate: TimedAccessControlStartEndPostDto = this.formMain.value;

      this._accountService.timedAccessControlStartEndPostAsync$(toUpdate)
        .subscribe(
          {
            next: ((x: ResponseIdentiyApiDto) => {

              console.log(x)
              if (x.succeeded) {
                this.openSnackBar('Intervalo de acesso atualizado.', 'warnings-success');
              }
            }),
            error: (error => {
              {
                this.openSnackBar(error, 'warnings-error');
              }
            })

          }
        )

    }
  }


}

