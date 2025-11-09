import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { ActivatedRoute } from '@angular/router';
import { BaseForm } from 'shared/extends/forms/base-form';
import { AuthenticatorSetupResponse } from './interfaces/authenticator-setup-response';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'two-factor-setup',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatFormFieldModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './two-factor-setup.component.html',
  styleUrl: './two-factor-setup.component.scss'
})
export class TwoFactorSetupComponent extends BaseForm implements AfterViewInit {

  setupData!: AuthenticatorSetupResponse;
  loading = true;
  error = '';

  @Input() set toggleRefresh(toggle: boolean) {
    this.start()
  };

  constructor(
    private _accountService: AccountService,
  ) {
    super()
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
          this.loading = false;
        },
        error: () => {
          this.error = 'Erro ao carregar dados do autenticador.';
          this.loading = false;
        }
      })
  }
}
