import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'Dating App';

  users: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.http.get('http://localhost:5000/api/appusers').subscribe({
      next: (values) => {
        this.users = values;
      },
      error: (err) => {
        console.error(err);
      },
    });
  }
}
