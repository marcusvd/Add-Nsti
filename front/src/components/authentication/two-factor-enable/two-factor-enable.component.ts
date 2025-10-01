import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ActivatedRoute } from '@angular/router';
import { BaseForm } from 'shared/inheritance/forms/base-form';
import { AccountService } from '../services/account.service';
import { ApiResponse, AuthenticatorSetupResponse, EnableAuthenticatorResponse } from './dtos/authenticator-setup-response';
import { BtnGComponent } from 'shared/components/btn-g/btn-g.component';
import { ToggleAuthenticatorRequestViewModel } from '../dtos/t2-factor';
import { NgxMaskModule } from "ngx-mask";

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
  btnType!: string;
  lockIcon!: string;

  constructor(
    private _accountService: AccountService,
    private _activatedRoute: ActivatedRoute,
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


  action(): void {

    if (this.formMain.invalid) return;
    this.loading = true;
    this.error = '';
    const request: ToggleAuthenticatorRequestViewModel = { ...this.formMain.value };
    request.enabled = !this.setupData?.isTwoFactorEnabled;

    this.btnType = request.enabled ? 'green' : 'red';
    this.lockIcon = request.enabled ? 'lock_close' : 'lock_open';
    this._accountService.enableAuthenticator(request).subscribe({
      next: (res) => {
        this.response = res;
        this.loading = false;
        this.toggleBtn.emit(request.enabled);
        this.start();
      },
      error: () => {
        this.error = 'Código inválido ou erro ao ativar 2FA.';
        this.loading = false;
      }
    });
  }

}




