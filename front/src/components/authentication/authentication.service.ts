// import { HttpClient } from '@angular/common/http';
// import { Injectable } from '@angular/core';
// import { Router } from '@angular/router';
// import { take} from 'rxjs/operators';
// import { environment } from 'src/environments/environment';
// import { BackEndService } from 'src/shared/services/back-end/backend.service';
// import { CommunicationAlerts } from 'src/shared/services/messages/snack-bar.service';
// import { MatDialog as MatDialog } from '@angular/material/dialog';
// import { BehaviorSubject } from 'rxjs';
// import { FormGroup } from '@angular/forms';
// import { UserToken } from '../dto/user-token';
// import { MyUser } from '../dto/my-user';
// import { RegisterComponent } from '../register/register.component';
// import { LoginComponent } from '../login/login.component';
// import { AuthWarningsComponent } from '../warnings/auth-warnings.component';
// import { ForgotPasswordComponent } from '../forgot-password/forgot-password.component';
// import { ConfirmEmail } from '../dto/confirm-email';
// import { RetryConfirmPassword } from '../dto/retry-confirm-password';
// import { ResetPassword } from '../dto/reset-password';
// import { ForgotPassword } from '../dto/forgot-password';
// import { T2Factor } from '../dto/t2-factor';
// import { ResendEmailConfirmDialogComponent } from '../resend-email-confirm-dialog/resend-email-confirm-dialog.component';

// @Injectable({
//   providedIn: 'root'
// })

// export class AuthenticationService extends BackEndService<MyUser> {

//   private currentUserSubject: BehaviorSubject<UserToken> = new BehaviorSubject<UserToken>(JSON.parse(localStorage.getItem("myUser")));
//   public currentUser: UserToken;
//   public _errorMessage: BehaviorSubject<string> = new BehaviorSubject<string>(null);

//   constructor(
//     override _http: HttpClient,
//     private _router: Router,
//     private _dialog: MatDialog,
//     private _communicationsAlerts: CommunicationAlerts,
//   ) {
//     super(_http, environment.auth)
//     this.currentUserSubject?.next(JSON.parse(localStorage.getItem("myUser")))
//     this.currentUser = this.currentUserSubject?.value
//   }

//   get CompanyId() {
//     return this.currentUser.companyId;
//   }


//   openAuthWarnings(data: any) {
//     const btn1: string = data.btn1;
//     const btn2: string = data.btn2;
//     const title: string = data.title;
//     const messageBody: string = data.messageBody;
//     const next: boolean = data.next;
//     const action: string = data.action;
//     const dialogRef = this._dialog.open(AuthWarningsComponent, {
//       width: '250px',
//       height: 'auto',
//       disableClose: true,
//       data: {
//         title: title,
//         messageBody: messageBody,
//         btn1: btn1,
//         btn2: btn2,
//       }
//     });
//     dialogRef.afterClosed().subscribe(result => {
//       if (next) {
//         if (action === 'openLogin') {
//           this._router.navigateByUrl('login');
//           //
//           // setTimeout(() => {
//           // }, 3000);
//         }
//       }
//     })


//   }


//     setItemLocalStorage(item: any, name: string) {
//     localStorage.setItem(name, JSON.stringify(item));
//   }
//   register(user: MyUser, form: FormGroup) {
//     this.add$<MyUser>(user, 'register').pipe(take(1))
//       .subscribe({
//         next: (user: MyUser) => {
//           this._communicationsAlerts.defaultSnackMsg('7', 0, null, 4);
//           this.openAuthWarnings({
//             btn1: 'Fechar', btn2: '', title: 'AVISO:',
//             messageBody: "Verifique seu e-mail para confirmar seu registro. Caixa de entrada, Spam ou lixo eletrônico. Obrigado!",
//             next: true, action: 'openLogin'
//           })
//         }, error: (err: any) => {
//           const erroCode: string = err.error.Message.split('|');
//           switch (erroCode[0]) {
//             case '1.1': {
//               this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
//               // this._communicationsAlerts.communicationCustomized({
//               //   'message': erroCode[1],
//               //   'action': '',
//               //   'delay': '3',
//               //   'style': 'red-snackBar-error',
//               //   'positionVertical': 'center',
//               //   'positionHorizontal': 'top',
//               // });
//               this._errorMessage.next(erroCode[1])
//               form.controls['email'].setErrors({ errorEmailDuplicated: true })
//               break;
//             }
//             case '1.2': {
//               console.log(err);
//               this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
//               // this._communicationsAlerts.communicationCustomized({
//               //   'message': erroCode[1],
//               //   'action': '',
//               //   'delay': '3',
//               //   'style': 'red-snackBar-error',
//               //   'positionVertical': 'center',
//               //   'positionHorizontal': 'top',
//               // });
//               this._errorMessage.next(erroCode[1])
//               form.controls['userName'].setErrors({ errorUserNameDuplicated: true })
//               break;
//             }
//             case '200.0': {
//               console.log(err);
//               this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
//               // this._communicationsAlerts.communicationCustomized({
//               //   'message': erroCode[1],
//               //   'action': '',
//               //   'style': 'red-snackBar-error',
//               //   'delay': '3',
//               //   'positionVertical': 'center',
//               //   'positionHorizontal': 'top',
//               // });
//               this.openAuthWarnings({ btn1: 'Fechar', btn2: '', title: 'Erro de autenticação', messageBody: erroCode[1] })
//               break;
//             }
//             case '1.7': {
//               console.log(err);
//               this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
//               // this._communicationsAlerts.communicationCustomized({
//               //   'message': erroCode[1],
//               //   'action': '',
//               //   'style': 'red-snackBar-error',
//               //   'delay': '3',
//               //   'positionVertical': 'center',
//               //   'positionHorizontal': 'top',
//               // });

//               this.openAuthWarnings({ btn1: 'Fechar', btn2: '', title: 'Erro de autenticação', messageBody: erroCode[1] })
//               break;
//             }
//           }
//         }
//       })
//     return this._errorMessage
//   }
//   login(user: MyUser) {

//     this.add$<MyUser>(user, 'login').subscribe({
//       next: (user: MyUser) => {

//         this.currentUserSubject.next(user);
//         this.currentUser = user;

//         if (user.authenticated) {
//           // this._errorMessage.complete();
//           // this._errorMessage.next(null);
//           if (user.action == "TwoFactor") {

//             this._router.navigateByUrl('two-factor');

//           }
//           this.setItemLocalStorage(user.id, "userId");
//           this.setItemLocalStorage(user, "myUser");
//           this.setItemLocalStorage(user.companyId, "companyId");
//           // this.getStockIdByCompanyId();

//           this._router.navigateByUrl('/');

//           this._communicationsAlerts.defaultSnackMsg('4', 0, null, 4);


//         }
//         else {
//         }

//       }, error: (err: any) => {
//         const erroCode: string = err.error.Message.split('|');
//         switch (erroCode[0]) {
//           case '1.0': {
//              this.resendEmailConfim(user);
//             this._errorMessage.next(erroCode[1])
//             break;
//           }
//           case '1.4': {
//             this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
//             this._errorMessage.next(erroCode[1])
//             break;
//           }
//           case '1.11': {
//             this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
//             this.openAuthWarnings({ btn1: 'Fechar', btn2: '', title: 'Erro de autenticação', messageBody: erroCode[1] })
//             break;
//           }
//           case '1.6': {
//             this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
//             this._errorMessage.next(erroCode[1])
//             break;
//           }
//         }

//       }

//     })
//     return this._errorMessage;
//   }
//   logOut() {
//     this._router.navigateByUrl('/login')
//     // this.openDialogLogin();
//     localStorage.clear();
//     this._communicationsAlerts.defaultSnackMsg('5', 0, null, 4);
//     this.currentUserSubject.complete();
//     this.currentUserSubject.next(null);
//     this.currentUser = null;
//   }

//   resendEmailConfim(user?: MyUser) {

//     const dialogRef = this._dialog.open(ResendEmailConfirmDialogComponent, {
//       width: 'auto',
//       height: 'auto',
//       data: { id: 0, btn1: 'Cancelar', btn2: 'Reenviar', messageBody: `Deseja reenviar e-mail para confirmação de identidade para `, email: `${user.userName}` },
//       autoFocus: true,
//       hasBackdrop: false,
//       disableClose: true,
//       panelClass: 'delete-dialog-class',
//     });

//     dialogRef.afterClosed().subscribe(result => {
//     })
//   }




//   forgotMyPassword(forgotPassword: ForgotPassword) {
//     return this.add$<ForgotPassword>(forgotPassword, 'forgotpassword').pipe(take(1)).subscribe({
//       next: () => {
//         this._communicationsAlerts.defaultSnackMsg('8', 0, null, 4);
//         setTimeout(() => {
//           this._router.navigateByUrl('/login');
//         }, 3000);
//       }, error: (err: any) => {
//         this._communicationsAlerts.defaultSnackMsg('8', 0, null, 4);
//       }
//     })
//   }
//   twoFactor(t2factor: T2Factor) {

//     return this.add$<T2Factor>(t2factor, 'twoFactor').pipe(take(1)).subscribe({
//       next: () => {
//         //this._toastr.success('Autenticação de dois fatores.', 'Sucesso!');
//       }, error: (err: any) => {
//         //this._toastr.error('Token Inválido ou expirado.', 'Falha');
//       }
//     })



//   }
//   confirmEmail(confirmEmail: ConfirmEmail) {
//     return this.add$<ConfirmEmail>(confirmEmail, 'confirmEmailAddress').pipe(take(1)).subscribe({
//       next: () => {
//         this._communicationsAlerts.defaultSnackMsg("E-mail Confirmado com sucesso.", 0, null, 4);
//         setTimeout(() => {
//           this._router.navigateByUrl('/login');
//         }, 3000);

//       }, error: (err: any) => {
//         this._communicationsAlerts.defaultSnackMsg("Falha ao confirmar e-mail.", 1, null, 4);
//       }
//     })

//   }

//   retryConfirmEmailGenerateNewToken(retryConfirmPassword: RetryConfirmPassword) {
//     return this.add$<RetryConfirmPassword>(retryConfirmPassword, 'RetryConfirmEmailGenerateNewToken').pipe(take(1)).subscribe({
//       next: () => {
//         this._communicationsAlerts.defaultSnackMsg("Link para confirmação de e-mail, enviado com sucesso.", 0, null, 4);
//         // setTimeout(() => {
//           //   this._router.navigateByUrl('/login');
//           // }, 3000);
//           //this._toastr.success('Confirmação de email...', 'Solicitação enviada...');
//         }, error: (err: any) => {
//         this._communicationsAlerts.defaultSnackMsg("Falha no envio do link para confirmação de e-mail.", 1, null, 4);

//       }
//     })
//   }
//   reset(resetPassword: ResetPassword) {
//     return this.add$(resetPassword, 'reset').pipe(take(1)).subscribe({
//       next: () => {
//         this._communicationsAlerts.defaultSnackMsg('6', 0, null, 4);

//         this._router.navigate((['/']));
//         this._router.navigateByUrl('/login');
//       }, error: (err: any) => {
//         const erroCode: string = err.error.Message.split('|');
//         switch (erroCode[0]) {
//           case '1.12': {
//             this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
//             // this._communicationsAlerts.communicationCustomized({
//             //   'message': erroCode[1],
//             //   'action': '',
//             //   'style': 'red-snackBar-error',
//             //   'delay': '',
//             //   'positionVertical': 'center',
//             //   'positionHorizontal': 'top',
//             // });
//             // this.openAuthWarnings({ btn1: 'Fechar', btn2: '', messageBody: erroCode[1] })
//             break;
//           }

//         }
//       }
//     })
//   }
// }
