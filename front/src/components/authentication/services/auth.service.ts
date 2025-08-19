// import { Injectable } from "@angular/core";
// import { CommunicationAlerts } from "shared/services/messages/snack-bar.service";
// import { MyUser } from "../dtos/my-user";
// import { MatSnackBar } from "@angular/material/snack-bar";
// import { BackEndService } from "shared/services/back-end/backend.service";
// import { LoginDto } from "../dtos/login-dto";


// @Injectable({ providedIn: 'root' })
// export class AuthService  {

//   constructor(private _snackBar: MatSnackBar) {}


//   login(user: LoginDto) {

//     if (user.password.length > 1) {
//       localStorage.setItem('auth', 'true');
//       localStorage.setItem('userName', user.userName);
//       // this.openSnackBar('Seja bem-vindo, ' + user.userName + '!', 'warnings-success');
//     }
//     else {
//       localStorage.setItem('auth', 'false');
//       // this.openSnackBar('Nome de usuário ou senha inválidos', 'warnings-error');
//       return 'Nome de usuário ou senha inválidos';
//     }
//     return 'Nome de usuário ou senha inválidos';
//   }

//   logOut() {

//     localStorage.setItem('auth', 'false');

//   }

//   isAuthenticated = () => {

//     const success = localStorage.getItem('auth')
//     if (success == 'true')
//       return true;
//     else
//       return false;

//   }





// }
