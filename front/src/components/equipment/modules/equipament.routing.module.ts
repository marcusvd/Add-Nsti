import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ListEquipmentComponent } from "../components/list/list-equipment.component";



const routes: Routes = [
  // { path: 'add', component: CustomerCreateComponent },
  // { path: 'view/:id', component: CustomerViewComponent },
  // { path: 'edit/:id', component: CustomerEditComponent },
  // { path: 'list/:id', component: CustomersListComponent }
  { path: 'list', component: ListEquipmentComponent }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})

export class EquipmentRoutingModule {

}
