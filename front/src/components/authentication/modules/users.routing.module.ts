import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ProfileComponent } from "../profile/profile.component";
import { AdmComponent } from "../adm/adm.component";

const routes: Routes = [
  { path: 'profile', component: ProfileComponent },
  { path: 'adm', component: AdmComponent },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})

export class UsersRoutingModule {


}
