import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatDividerModule } from "@angular/material/divider";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatTabsModule } from "@angular/material/tabs";
import { RouterModule } from "@angular/router";
import { BtnGComponent } from "shared/components/btn-g/btn-g.component";
import { CaptchaComponent } from "shared/components/captcha/captcha.component";
import { DefaultComponent } from "shared/components/default-component/default-component";
import { ListCardGComponent } from "shared/components/list-g/list-card/list-card-g.component";

export const ImportsListAdm: any[] = [
  CommonModule,
  MatCardModule,
  MatFormFieldModule,
  ReactiveFormsModule,
  MatIconModule,
  MatInputModule,
  MatButtonModule,
  MatDividerModule,
  MatTabsModule,
  RouterModule,
  CaptchaComponent,
  ListCardGComponent,
  DefaultComponent,
  BtnGComponent,
]
