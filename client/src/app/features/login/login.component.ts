import { Component, OnDestroy, OnInit } from '@angular/core';
import { AccountService } from '../../services/account/account.service';
import { User } from '../../models/user';
import { Subscriber, Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit, OnDestroy {
  private subcription!: Subscription;

  model: any = {};

  constructor(private accountService: AccountService, private router: Router) {}

  //#region Lifecycle
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
  //#endregion

  login() {
    this.accountService.login(this.model).subscribe({
      next: (value) => {
        this.router.navigateByUrl('/');
      },
      error: (err) => {
        console.error('Failed to login');
      },
    });
  }
}
