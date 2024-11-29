import { Component, OnDestroy, OnInit } from '@angular/core';
import { AccountService } from '../../services/account/account.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit, OnDestroy {
  private subcription!: Subscription;

  model: any = {};

  protected UsernameErrMsg = '';
  protected confirmPassErrMsg = '';

  constructor(private accountService: AccountService, private router: Router) {}

  ngOnInit(): void {
    this.subcription = this.accountService.account$.subscribe({
      next: (value) => {
        if (!value) return;
        this.router.navigateByUrl('/');
      },
    });
  }

  ngOnDestroy(): void {
    this.subcription.unsubscribe();
  }

  register() {
    if (this.model.password != this.model.confirmPassword) {
      this.confirmPassErrMsg = 'Passwords do not match';
      return;
    }

    this.confirmPassErrMsg = '';

    this.accountService.register(this.model).subscribe({
      next: (value) => {
        this.router.navigateByUrl('/');
      },
      error: (err) => {
        console.error(err);
      },
    });
  }
}
