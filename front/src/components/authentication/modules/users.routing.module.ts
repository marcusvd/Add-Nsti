import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AddCompanyComponent } from "components/company/components/add/add-company.component";
import { AddUserCompanyComponent } from "../adm/add-user-company/add-user-company.component";
import { ListAdmComponent } from "../adm/list/list-adm.component";
import { PanelAdmComponent } from "../adm/panel-adm/panel-adm.component";
import { ProfileComponent } from "../profile/profile.component";
import { EditUserCompanyComponent } from "../adm/panel-adm/edit-user-company/edit-user-company.component";


const routes: Routes = [
  { path: 'adm-list/:id', component: ListAdmComponent },
  { path: 'add-company/:id', component: AddCompanyComponent },
  { path: 'edit-company/:id', component: PanelAdmComponent },
  { path: 'add-user-company/:id', component: AddUserCompanyComponent },
  { path: 'edit-user/:id', component: EditUserCompanyComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'panel-adm', component: PanelAdmComponent },


]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})

export class UsersRoutingModule {


}
