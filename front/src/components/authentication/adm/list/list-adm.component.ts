
import { Component, inject, OnInit } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { ActivatedRoute, RouterModule } from '@angular/router';


import { environment } from 'environments/environment';
import { TruncatePipe } from 'shared/pipes/truncate.pipe';
import { AdmService } from '../../services/adm.service';
import { ListControlAdm } from '../helpers/list-control-adm';
import { ImportsListAdm } from '../imports/imports-list-adm';


@Component({
  selector: 'list-adm',
  templateUrl: './list-adm.component.html',
  styleUrls: ['./list-adm.component.scss'],
  standalone: true,
  imports: [
    ImportsListAdm,
    RouterModule,
    MatTabsModule,
  ],
  providers: [
    AdmService,
    TruncatePipe
  ]
})
export class ListAdmComponent extends ListControlAdm implements OnInit {

  private readonly _actRouter = inject(ActivatedRoute);
  private backend = `${environment._BACK_END_ROOT_URL}/_Businesses/GetBusinessFullAsync`

  addRoute!: string


  back() {
    window.history.back();
  }

  ngOnInit(): void {
    const id = this._actRouter.snapshot.params['id'] as number;
    this.addRoute = '/users/add-company/' + id
    this.startSupply(this.backend, id)
  }

}
