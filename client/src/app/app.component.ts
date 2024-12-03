import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/account/account.service';
import { Account } from './models/account';
import { UserService } from './services/user/user.service';
import { User } from './models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'Dating App';

  constructor(
    private accountService: AccountService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.CheckLoggedIn();
    this.CheckUser();
  }

  CheckLoggedIn() {
    let userString = sessionStorage.getItem(this.accountService.sessionKey);
    let user: Account | null = null;
    if (userString) user = JSON.parse(userString ?? '');
    this.accountService.currentUserSource.next(user);
  }

  CheckUser() {
    let userString = sessionStorage.getItem(this.userService.sessionKey);
    if (userString) this.userService.setUser(JSON.parse(userString ?? ''));
  }
}
