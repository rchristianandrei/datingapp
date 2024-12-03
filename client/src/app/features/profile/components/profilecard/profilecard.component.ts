import { Component, Input } from '@angular/core';
import { User } from '../../../../models/user';

@Component({
  selector: 'app-profilecard',
  templateUrl: './profilecard.component.html',
  styleUrl: './profilecard.component.css',
})
export class ProfilecardComponent {
  @Input() user!: User;
}
