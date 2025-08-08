import { ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { RouterModule } from "@angular/router";
import { CaptchaComponent } from "shared/components/captcha/captcha.component";

export const AuthLoginImports: any[] = [
  ReactiveFormsModule,
  MatButtonModule,
  MatInputModule,
  MatFormFieldModule,
  RouterModule,
  CaptchaComponent
]
