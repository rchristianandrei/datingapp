import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Account } from '../../models/account';
import { ReplaySubject, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private readonly baseURL = 'http://localhost:5000/api/accounts/';

  readonly sessionKey = 'account';

  currentUserSource = new ReplaySubject<Account | null>(1);
  account$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}

  register(model: any) {
    return this.http.post<Account>(this.baseURL + 'register', model).pipe(
      tap((response) => {
        sessionStorage.setItem(this.sessionKey, JSON.stringify(response));
        this.currentUserSource.next(response);
      })
    );
  }

  login(model: any) {
    return this.http.post<Account>(this.baseURL + 'login', model).pipe(
      tap((response) => {
        sessionStorage.setItem(this.sessionKey, JSON.stringify(response));
        this.currentUserSource.next(response);
      })
    );
  }

  logout() {
    sessionStorage.removeItem(this.sessionKey);
    this.currentUserSource.next(null);
  }
}
