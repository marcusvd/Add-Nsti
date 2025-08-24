
import { HttpInterceptorFn } from '@angular/common/http';
import { UserTokenDto } from '../dtos/user-token-dto';

export const AuthInterceptor: HttpInterceptorFn = (req, next) => {

  const token = (JSON.parse(localStorage.getItem("myUser") ?? '{}') as UserTokenDto).token;

  if (token) {
    const cloned = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
    return next(cloned);
  }
  
  return next(req);
};
