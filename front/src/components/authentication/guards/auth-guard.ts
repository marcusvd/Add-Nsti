import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { UserTokenDto } from '../dtos/user-token-dto';


export const authGuard: CanActivateFn = (route, state) => {

  // const authService = inject(AuthService);

  const router = inject(Router);
  
  const isAuthenticated: UserTokenDto = JSON.parse(localStorage.getItem("myUser") ?? '{}');

  return isAuthenticated.authenticated ? true : router.createUrlTree(['/login']);

}
