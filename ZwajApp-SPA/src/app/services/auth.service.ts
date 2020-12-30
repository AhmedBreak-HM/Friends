import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/Auth';

  constructor(private http: HttpClient) { }
  logIn(model: any) {
    return this.http.post(`${this.baseUrl}/login`, model).pipe(
      map((res: any) => {
        const user = res;
        user ? localStorage.setItem('token', user.token) : '';
        // if (user) { localStorage.setItem('token', user.token); }
      })
    );
  }
  register(model: any) {
    return this.http.post(`${this.baseUrl}/register`, model);
  }

}
