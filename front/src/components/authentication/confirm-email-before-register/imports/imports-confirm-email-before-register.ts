import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatDividerModule } from "@angular/material/divider";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { RouterModule } from "@angular/router";
import { BtnGComponent } from "shared/components/btn-g/btn-g.component";
import { CaptchaComponent } from "shared/components/captcha/captcha.component";

export const ImportsConfirmEmailBeforeRegisterHelper: any[] = [
  CommonModule,
  MatCardModule,
  MatFormFieldModule,
  ReactiveFormsModule,
  MatIconModule,
  MatInputModule,
  MatButtonModule,
  MatDividerModule,
  RouterModule,
  CaptchaComponent,
  BtnGComponent
]
