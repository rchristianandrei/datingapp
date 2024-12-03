import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user/user.service';
import { User } from '../../models/user';
import { take } from 'rxjs';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css',
})
export class ProfileComponent implements OnInit {
  user!: User;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService.user$.pipe(take(1)).subscribe({
      next: (user) => {
        console.log(user);
        if (user) this.user = user;
      },
    });
  }
}
