import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { AccountService } from '../../services/account/account.service';
import { Observable, Subscription } from 'rxjs';
import { User } from '../../models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent implements OnInit, OnDestroy {
  title = 'Dating App';

  constructor(protected accountService: AccountService) {}

  //#region Lifecycle
  ngOnInit(): void {}

  ngOnDestroy(): void {}
  //#endregion

  logout() {
    this.accountService.logout();
  }
}
