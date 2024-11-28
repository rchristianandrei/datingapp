import { Component } from '@angular/core';
import { AccountService } from '../../../services/account/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  model: any = {};

  protected UsernameErrMsg = '';
  protected confirmPassErrMsg = '';

  constructor(private accountService: AccountService) {}

  register() {
    if (this.model.password != this.model.confirmPassword) {
      this.confirmPassErrMsg = 'Passwords do not match';
      return;
    }

    this.confirmPassErrMsg = '';

    this.accountService.register(this.model).subscribe({
      next: (value) => {
        console.log('Register Comp: Registered!', value);
      },
      error: (err) => {
        console.error(err);
      },
    });
  }
}
