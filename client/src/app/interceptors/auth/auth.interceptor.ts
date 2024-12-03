import { HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { AccountService } from '../../services/account/account.service';
import { take } from 'rxjs/operators';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const account = inject(AccountService);

  let clonedReq: HttpRequest<unknown> = req;

  account.account$.pipe(take(1)).subscribe({
    next: (value) => {
      const token = value?.token ?? '';
      if (!token) return;
      clonedReq = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      });
    },
  });

  return next(clonedReq);
};
