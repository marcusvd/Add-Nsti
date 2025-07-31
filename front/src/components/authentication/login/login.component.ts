
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';


import { BaseForm } from '../../../shared/inheritance/forms/base-form';
import { DefaultCompImports } from 'components/imports/default-comp-imports';
import { MyUser } from '../dtos/my-user';
import { AuthLoginImports } from '../imports/auth.imports';
import { AuthService } from '../services/auth.service';
import { LoginDto } from '../dtos/login-dto';


@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [
    DefaultCompImports,
    AuthLoginImports
  ]
})
export class LoginComponent extends BaseForm implements OnInit {

  inputEmail(arg0: string) {
    if (arg0.length == 0)
      this.loginErrorMessage = '';
  }

  test() {
    // console.log(this.formMain.get('userName')?.hasError('required'));
    // console.log(this.formMain.get('userName')?.touched);
  }

  constructor(
    public _auth: AuthService,
    private _router: Router,
    private _fb: FormBuilder,


  ) { super() }
  override formMain!: FormGroup;



  loginErrorMessage: string = '';
  login() {

    // this._router.navigateByUrl('/default-route/customers/add');

    const login: LoginDto = this.formMain.value;

    this.loginErrorMessage = this._auth.login(login);
    if (this.alertSave(this.formMain)) {
      if (this._auth.isAuthenticated())
        this._router.navigateByUrl('/');

    }
    // // this.loginErrorMessage = null;
    // // this.loginErrorMessage = '';
    // if (this.alertSave(this.formMain)) {
    //   this._auth.login(login).subscribe((x: string) => {

    //     // this.loginErrorMessage = x;
    //   })
    // }
  }


  pwdType: string = 'password';
  pwdIcon: string = 'visibility_off';


  pwdHideShow() {
    if (this.pwdType === 'password') {
      this.pwdType = 'text';
      this.pwdIcon = 'visibility';
    } else {
      this.pwdType = 'password';
      this.pwdIcon = 'visibility_off';
    }
  }



  formLoad() {
    return this.formMain = this._fb.group({
      userName: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    })
  }

  ngOnInit(): void {
    // this.loginErrorMessage = null;
    // this.loginErrorMessage = '';
    this.formLoad();


  }

}
