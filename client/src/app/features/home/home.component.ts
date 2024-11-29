import { Component } from '@angular/core';
import { AccountService } from '../../services/account/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  constructor(protected accountService: AccountService) {}
}
