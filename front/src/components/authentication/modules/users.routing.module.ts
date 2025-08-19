import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ProfileComponent } from "../profile/profile.component";
import { ListAdmComponent } from "../adm/list/list-adm.component";


const routes: Routes = [
  { path: 'profile', component: ProfileComponent },
  { path: 'adm-list/:id', component: ListAdmComponent },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})

export class UsersRoutingModule {


}
