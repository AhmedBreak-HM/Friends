import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../services/alertify.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  model: any = {userName: '', Password: ''};

  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }
  logIn() {
    this.authService.logIn(this.model).subscribe(
      next => this.alertify.success('تم تسجيل الدخول بنجاح'),
      error => this.alertify.error('فشل فى الدخول')
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
