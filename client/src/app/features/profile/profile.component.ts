import {
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { UserService } from '../../services/user/user.service';
import { User } from '../../models/user';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../../services/account/account.service';
import { take, Subscription } from 'rxjs';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css',
})
export class ProfileComponent implements OnInit, OnDestroy {
  user!: User;

  owner = false;
  editing = false;

  private routeSub!: Subscription;
  @ViewChild('introduction') private introduction!: ElementRef;
  @ViewChild('interests') private interests!: ElementRef;
  @ViewChild('lookingFor') private lookingFor!: ElementRef;
  @ViewChild('city') private city!: ElementRef;
  @ViewChild('country') private country!: ElementRef;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private userService: UserService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    // Sub to changing parameter
    this.routeSub = this.route.params.subscribe({
      next: (params) => {
        const username = params['username'];

        // Check if username is null
        if (!username) {
          this.router.navigateByUrl('/');
          return;
        }

        // Check if owner
        this.accountService.account$.pipe(take(1)).subscribe({
          next: (user) => {
            if (!user) return;
            this.owner = user.username.toLowerCase() === username.toLowerCase();
          },
        });

        // Get User Info
        this.getUser(username);
      },
    });
  }

  ngOnDestroy(): void {
    this.routeSub.unsubscribe();
  }

  saveUser() {
    this.user.introduction = this.introduction.nativeElement.value;
    this.user.interests = this.interests.nativeElement.value;
    this.user.lookingFor = this.lookingFor.nativeElement.value;
    this.user.city = this.city.nativeElement.value;
    this.user.country = this.country.nativeElement.value;

    this.userService.updateUser(this.user).subscribe({
      next: (value) => {
        this.editing = false;
      },
      error: (err) => {
        console.log(err);
        this.getUser(this.user.username);
        this.toastr.error('Unable to update the information');
      },
    });
  }

  private getUser(username: string) {
    this.userService.getUser(username).subscribe({
      next: (user) => {
        if (!user) this.router.navigateByUrl('/');
        this.user = user;
      },
    });
  }
}
