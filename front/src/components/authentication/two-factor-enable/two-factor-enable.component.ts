import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgxMaskModule } from "ngx-mask";
import { BtnGComponent } from 'shared/components/btn-g/btn-g.component';
import { BaseForm } from 'shared/extends/forms/base-form';
import { ToggleAuthenticatorRequestViewModel } from '../dtos/t2-factor';
import { AccountService } from '../services/account.service';
import { ApiResponse, AuthenticatorSetupResponse, EnableAuthenticatorResponse } from './dtos/authenticator-setup-response';

@Component({
  selector: 'two-factor-enable',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    BtnGComponent,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatListModule,
    MatProgressSpinnerModule,
    NgxMaskModule
  ],
  templateUrl: './two-factor-enable.component.html',
  styleUrl: './two-factor-enable.component.scss'
})
export class TwoFactorEnableComponent extends BaseForm implements AfterViewInit {

  @Output() toggleBtn = new EventEmitter<boolean>();
  setupData!: AuthenticatorSetupResponse;
  response?: ApiResponse<EnableAuthenticatorResponse>;
  loading = false;
  error = '';
  success = '';
  btnType!: string;
  lockIcon!: string;

  constructor(
    private _accountService: AccountService,
    private _fb: FormBuilder
  ) {
    super()

    this.formMain = this._fb.group({
      code: ['', [Validators.required, Validators.pattern(/^\d{6}$/)]],
      enabled: [false, Validators.required]
    });

  }

  ngAfterViewInit(): void {
    this.start();
  }

  start() {
    this._accountService.GetAuthenticatorSetup()
      .subscribe({
        next: (data: any) => {
          // console.log(data)
          this.setupData = data.data;

          this.btnType = !this.setupData?.isTwoFactorEnabled ? 'green' : 'red';
          this.lockIcon = !this.setupData?.isTwoFactorEnabled ? 'lock_close' : 'lock_open';

          this.loading = false;
        },
        error: () => {
          this.error = 'Erro ao carregar dados do autenticador.';
          this.loading = false;
        }
      })
  }

  hasErrorEnableDisable2FA: number = 0;


  action(): void {
    this.formMain.get('code')?.markAsTouched();
    if (this.formMain.invalid) return;

    this.loading = true;
    this.error = '';
    const request: ToggleAuthenticatorRequestViewModel = { ...this.formMain.value };
    request.enabled = !this.setupData?.isTwoFactorEnabled;

    this.btnType = request.enabled ? 'green' : 'red';
    this.lockIcon = request.enabled ? 'lock_close' : 'lock_open';

    this._accountService.enableAuthenticator(request).subscribe({
      next: (res) => {
        this.hasErrorEnableDisable2FA = res.errors.length;
        this.response = res;
        this.loading = false;

        if (res.success && request.enabled) {
          this.toggleBtn.emit(request.enabled);
          this.success = '2FA ativado com sucesso!';
          this.openSnackBar(this.success, 'warnings-success');
        }
        if (res.success && !request.enabled) {
          this.toggleBtn.emit(request.enabled);
          this.success = '2FA desativado com sucesso!';
          this.openSnackBar(this.success, 'warnings-success');
        }

        if (res.errors.length > 0) {
          console.log(this.error)
          this.success = '';
          this.error = 'Código inválido / Usuário não autenticado';
          this.openSnackBar(this.error, 'warnings-error');
        }

        this.start();
      },
      error: () => {

        console.log('erro');

        this.error = 'Código inválido / Usuário não autenticado';

        this.loading = false;

      }
    });
  }

}




