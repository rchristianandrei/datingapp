import { Component, OnDestroy, OnInit } from '@angular/core';
import { AccountService } from '../../services/account/account.service';
import { User } from '../../models/user';
import { Subscriber, Subscription } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit, OnDestroy {
  private subcription!: Subscription;

  model: any = {};

  constructor(private accountService: AccountService) {}

  //#region Lifecycle
  ngOnInit(): void {
    this.subcription = this.accountService.account$.subscribe({
      next: (value) => {
        if (!value) return;
        console.log('Login Comp: Already logged in!');
      },
    });
  }

  ngOnDestroy(): void {
    this.subcription.unsubscribe();
  }
  //#endregion

  login() {
    this.accountService.login(this.model).subscribe({
      next: (value) => {
        console.log(value);
      },
      error: (err) => {
        console.error('Failed to login');
      },
    });
  }
}
