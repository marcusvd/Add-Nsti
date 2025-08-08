import { Routes } from '@angular/router';
import { SideNavComponent } from '../shared/components/side-nav/components/side-nav.component';
import { LoginComponent } from 'components/authentication/login/login.component';
import { authGuard } from 'components/authentication/guards/auth-guard';
import { UnknownRouteComponent } from 'shared/components/unknown-route/unknown-route.component';
import { RegisterComponent } from 'components/authentication/register/register.component';
import { ConfirmEmailComponent } from 'components/authentication/confirm-email/confirm-email.component';
import { ForgotPasswordComponent } from 'components/authentication/forgot-password/forgot-password.component';
import { PasswordResetComponent } from 'components/authentication/password-reset/password-reset.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'confirm-email', component: ConfirmEmailComponent },
  { path: 'forgot', component: ForgotPasswordComponent },
  { path: 'password-reset', component: PasswordResetComponent },
  { path: 'unknown-route', component: UnknownRouteComponent },
  {
    path: '', component: SideNavComponent,
    canActivate: [authGuard],
    children: [
      // {
      //   path: 'auth',
      //   loadChildren: () => import('../components/authentication/modules/auth.routing.module').then(x => x.RegisterRoutingModule),
      // },
      {
        path: 'employees',
        loadChildren: () => import('../components/employees/modules/employees.routing.module').then(x => x.EmployeesRoutingModule),
      },
      {
        path: 'customers',
        loadChildren: () => import('../components/customer/modules/customer.routing.module').then(x => x.CustomerRoutingModule),
      },
      {
        path: 'equipments',
        loadChildren: () => import('../components/equipment/modules/equipament.routing.module').then(x => x.EquipmentRoutingModule),
      },
    ]
  },
  { path: '**', redirectTo: 'unknown-route' }
]
