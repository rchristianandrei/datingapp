import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../../models/user';
import { ReplaySubject } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly baseURL = `${environment.apiUrl}/users`;

  readonly sessionKey = 'user';

  private currentUserSource = new ReplaySubject<User | null>(1);
  user$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}

  getUser(username: string) {
    return this.http.get<User>(this.baseURL + '/' + username);
  }

  setUser(user: User) {
    this.currentUserSource.next(user);
    if (!sessionStorage.getItem(this.sessionKey))
      sessionStorage.setItem(this.sessionKey, JSON.stringify(user));
  }

  logout() {
    this.currentUserSource.next(null);
    sessionStorage.removeItem(this.sessionKey);
  }
}
