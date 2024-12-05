import { Component, OnDestroy, OnInit } from '@angular/core';
import { AccountService } from '../../services/account/account.service';
import { Account } from '../../models/account';
import { Subscriber, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit, OnDestroy {
  private subcription!: Subscription;

  model: any = {};

  constructor(
    private accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {}

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
      next: (account) => {
        this.toastr.success('Successfully logged in', 'Login');
        this.router.navigateByUrl('/');
      },
      error: (err) => {
        this.toastr.error('Failed to login', 'Login');
      },
    });
  }
}
