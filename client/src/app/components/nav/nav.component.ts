import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { AccountService } from '../../services/account/account.service';
import { Observable, SubscribableOrPromise, Subscription } from 'rxjs';
import { Account } from '../../models/account';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent implements OnInit, OnDestroy {
  title = 'Dating App';

  username = '';

  private subscription!: Subscription;

  constructor(
    protected accountService: AccountService,
    private userService: UserService
  ) {}

  //#region Lifecycle
  ngOnInit(): void {
    this.subscription = this.accountService.account$.subscribe({
      next: (value) => {
        if (!value) return;
        this.username = value.username;
      },
    });
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
  //#endregion

  logout() {
    this.accountService.logout();
    this.userService.logout();
  }
}
