import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";


import { CustomersListComponent } from "../components/list/customers-list.component";
import { CustomerAddComponent } from "../components/add/customer-add.component";
// import { CustomerViewComponent } from "../components/view/customer-view.component";
import { CustomerEditComponent } from "../components/edit/customer-edit.component";



const routes: Routes = [
  { path: 'add', component: CustomerAddComponent },
  // { path: 'view/:id', component: CustomerViewComponent },
  { path: 'edit/:id', component: CustomerEditComponent },
  // { path: 'list/:id', component: CustomersListComponent }
  { path: 'list', component: CustomersListComponent }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})

export class CustomerRoutingModule {

}
