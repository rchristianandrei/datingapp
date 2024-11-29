import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account/account.service';
import { map } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

export const authGuard: CanActivateFn = (route, state) => {
  const account = inject(AccountService);
  const toastr = inject(ToastrService);
  const router = inject(Router);

  return account.account$.pipe(
    map((value) => {
      if (value == null) {
        toastr.error("You're not allowed to go there");
        router.navigateByUrl('/login');
      }

      return value != null;
    })
  );
};
