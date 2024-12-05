import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/account/account.service';
import { Account } from './models/account';
import { UserService } from './services/user/user.service';

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
  }

  CheckLoggedIn() {
    let userString = sessionStorage.getItem(this.accountService.sessionKey);
    let user: Account | null = null;
    if (userString) user = JSON.parse(userString ?? '');
    this.accountService.currentUserSource.next(user);
  }
}
