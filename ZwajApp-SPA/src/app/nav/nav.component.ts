import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  model: any = {userName: '', Password: ''};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }
  logIn() {
    this.authService.logIn(this.model).subscribe(
      next => console.log('تم تسجيل الدخول بنجاح'),
      error => console.log('فشل فى الدخول')
      );
  }
  loggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !! token;
  }
  loggedOut() {
    localStorage.removeItem('token');
    console.log('logout don !');
  }

}
