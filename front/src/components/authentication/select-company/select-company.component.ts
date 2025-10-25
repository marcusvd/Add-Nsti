
import { Component, Inject, inject, Input, OnDestroy, OnInit } from '@angular/core';


import { ActivatedRoute, Router } from '@angular/router';
import { DefaultCompImports } from 'components/imports/default-comp-imports';
import { Subscription } from 'rxjs';
import { CompanyAuth } from "components/company/dtos/company-auth";
import { AuthLoginImports } from '../imports/auth.imports';
import { SelectCompanyHelper } from './select-company-helper';
import { MatSelectModule } from '@angular/material/select';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { DeleteDialogComponent } from 'shared/components/delete-dialog/delete-dialog.component';
import { MatOptionSelectionChange } from '@angular/material/core';
import { WarningsService } from 'components/warnings/services/warnings.service';
import { TruncatePipe } from 'shared/pipes/truncate.pipe';
import { CompanyService } from 'components/company/services/company.service';



@Component({
  selector: 'select-company',
  templateUrl: './select-company.component.html',
  styleUrls: ['./select-company.component.scss'],
  standalone: true,
  imports: [
    DefaultCompImports,
    AuthLoginImports,
    MatSelectModule,
    MatDialogModule,
  ], providers: [
    // MatDialogRef
  ]
})
export class SelectCompanyComponent extends SelectCompanyHelper implements OnInit, OnDestroy {

  private _companyServices = inject(CompanyService);
  private _actRoute = inject(ActivatedRoute);
  private _warningsService = inject(WarningsService);

  @Input() idInput: number = 0;
  idDialog!: number;
  private _subGetCompaniesByUserId!: Subscription;
  _companiesAuth: CompanyAuth[] = [];
  selectedId: number = 0;

  ngOnInit(): void {

    const id = this.selectId();

    this.selectedId = id;

    if (id > 0) {

      this._subGetCompaniesByUserId = this._companyServices.getCompaniesByUserId$(id)
        .subscribe(
          {
            next: (x: CompanyAuth[]) => x.forEach(y => {
              this._companiesAuth.push(y)
            }),
            error: (error: any) => console.log(error)
          }
        )
    }

    //change screen to full
    const elem = document.documentElement;
    elem.requestFullscreen().then((x) => x)

  }


  ngOnDestroy(): void {
    this._companiesAuth = [];
    this._subGetCompaniesByUserId.unsubscribe();
  }

  onSelectionChange($event: MatOptionSelectionChange<CompanyAuth>) {

    localStorage.removeItem("selectedCompany");
    localStorage.setItem("selectedCompany", JSON.stringify($event.source.value));

  }



  private selectId(): number {

    const paramsId = this._actRoute.snapshot.params['id'] as number;
    const idDialog = this.idDialog;
    const idInput = this.idInput;

    let id = 0;

    if (paramsId > 0)
      id = paramsId

    if (idDialog > 0)
      id = idDialog

    if (idInput > 0)
      id = idInput
    return id;
  }

  clickedYes(id: number, yes: string) {
    this._warningsService.openSnackBar(`SEJA BEM-VINDO À ${this.companyName}`, 'warnings-success');
    this.callRouter('/users');
  }
  clickedNo(no: string) {
    // this._dialogRef.close(no);
  }


}
