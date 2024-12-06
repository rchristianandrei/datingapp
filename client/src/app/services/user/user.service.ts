import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../../models/user';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly baseURL = `${environment.apiUrl}/users`;

  constructor(private http: HttpClient) {}

  updateUser(user: User) {
    return this.http.put(this.baseURL + '/', user);
  }

  getUser(username: string) {
    return this.http.get<User>(this.baseURL + '/' + username);
  }

  getUsers() {
    return this.http.get<User[]>(this.baseURL + '/');
  }
}
