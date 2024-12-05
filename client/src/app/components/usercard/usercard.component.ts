import { Component, Input } from '@angular/core';
import { User } from '../../models/user';

@Component({
  selector: 'app-usercard',
  templateUrl: './usercard.component.html',
  styleUrl: './usercard.component.css',
})
export class UsercardComponent {
  @Input() user!: User;
}
