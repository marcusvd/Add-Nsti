import { Injectable } from "@angular/core";
import { CommunicationAlerts } from "shared/services/messages/snack-bar.service";
import { MyUser } from "../dtos/my-user";
import { MatSnackBar } from "@angular/material/snack-bar";


@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private snackBar: MatSnackBar) { }


  openSnackBar(message: string, style:string, action: string = 'Fechar') {
    this.snackBar.open(message, action, {
      duration: 5000, // Tempo em milissegundos (5 segundos)
      panelClass: [style], // Aplica a classe personalizada
      horizontalPosition: 'center', // Centraliza horizontalmente
      verticalPosition: 'top', // Posição vertical (pode ser 'top' ou 'bottom')
    });
  }
  login(user: MyUser) {

    if (user.password.length > 1) {
      localStorage.setItem('auth', 'true');
      localStorage.setItem('userName', user.userName);
      this.openSnackBar('Seja bem-vindo, ' + user.userName + '!' , 'login-success');
    }
    else {
      localStorage.setItem('auth', 'false');
      this.openSnackBar('Nome de usuário ou senha inválidos', 'login-error');
      return 'Nome de usuário ou senha inválidos';
    }
    return 'Nome de usuário ou senha inválidos';
  }

  logOut() {

    localStorage.setItem('auth', 'false');

  }

  isAuthenticated = () => {

    const success = localStorage.getItem('auth')
    if (success == 'true')
      return true;
    else
      return false;

  }





}
