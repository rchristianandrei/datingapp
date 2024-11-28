import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/account/account.service';
import { User } from './models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'Dating App';

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.CheckLoggedIn();
  }

  CheckLoggedIn() {
    let userString = sessionStorage.getItem(this.accountService.sessionKey);
    let user: User | null = null;
    if (userString) user = JSON.parse(userString ?? '');
    this.accountService.currentUserSource.next(user);
  }
}
