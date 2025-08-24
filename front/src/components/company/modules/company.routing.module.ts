import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";


import { CompanysListComponent } from "../components/list/companys-list.component";
import { CompanyAddComponent } from "../components/add/company-add.component";
// import { CompanyViewComponent } from "../components/view/company-view.component";
import { CompanyEditComponent } from "../components/edit/company-edit.component";



const routes: Routes = [
  { path: 'add', component: CompanyAddComponent },
  // { path: 'view/:id', component: CompanyViewComponent },
  { path: 'edit/:id', component: CompanyEditComponent },
  // { path: 'list/:id', component: CompanysListComponent }
  { path: 'list', component: CompanysListComponent }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})

export class CompanyRoutingModule {

}
