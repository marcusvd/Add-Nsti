import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ListEmployeesComponent } from "../components/list/list-employees.component";



const routes: Routes = [
  // { path: 'add', component: CustomerCreateComponent },
  // { path: 'view/:id', component: CustomerViewComponent },
  // { path: 'edit/:id', component: CustomerEditComponent },
  // { path: 'list/:id', component: CustomersListComponent }
  { path: 'list', component: ListEmployeesComponent }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})

export class EmployeesRoutingModule {

}
