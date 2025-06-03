import { Routes } from '@angular/router';
import { SideNavComponent } from '../shared/components/side-nav/components/side-nav.component';
import { LoginComponent } from 'components/authentication/login/login.component';
import { authGuard } from 'components/authentication/guards/auth-guard';
import { UnknownRouteComponent } from 'shared/components/unknown-route/unknown-route.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'unknown-route', component: UnknownRouteComponent },
  {
    path: '', component: SideNavComponent,
    canActivate: [authGuard],
    children: [
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
  {path: '**', redirectTo: 'unknown-route'}
]
