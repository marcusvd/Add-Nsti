import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ProfileComponent } from "../profile/profile.component";
import { ListAdmComponent } from "../adm/list/list-adm.component";
import { PanelAdmComponent } from "../adm/panel-adm/panel-adm.component";


const routes: Routes = [
  { path: 'adm-list/:id', component: ListAdmComponent },
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
