import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { UserTokenDto } from '../dtos/user-token-dto';


export const authGuard: CanActivateFn = (route, state) => {

  const router = inject(Router);

  const userToken: UserTokenDto = JSON.parse(localStorage.getItem("myUser") ?? '{}');

return userToken.authenticated ? true : router.createUrlTree(['/login']);

  // if (userToken.companyUserAccounts.length > 0)

  // else
  //   return router.createUrlTree(['/first-company-register']);



}
