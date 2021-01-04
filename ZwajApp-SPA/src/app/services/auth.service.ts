import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/Auth';
  jwtHelper: JwtHelperService = new JwtHelperService();

  constructor(private http: HttpClient) { }
  logIn(model: any) {
    return this.http.post(`${this.baseUrl}/login`, model).pipe(
      map((res: any) => {
        const user = res;
        user ? localStorage.setItem('token', user.token) : '';
        this.decodedUser();
      })
    );
  }
  register(model: any) {
    return this.http.post(`${this.baseUrl}/register`, model);
  }
  isUser() {
    try {
      const token = localStorage.getItem('token');
      return this.jwtHelper.isTokenExpired(token);
    } catch {
      return true;
    }
  }
  decodedUser(): any {
    const token = localStorage.getItem('token');
    const decodedToken = this.jwtHelper.decodeToken(token);
    return decodedToken;
  }

}
