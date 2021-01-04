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
  displayName: string;

  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.displayName = this.authService.decodedUser().unique_name;
  }
  logIn() {
    this.authService.logIn(this.model).subscribe(
      next => {
        this.alertify.success('تم تسجيل الدخول بنجاح');
        this.displayName = this.authService.decodedUser().unique_name;
      },
      error => this.alertify.error('فشل فى الدخول')
      );
  }
  loggedIn(): boolean {
    return ! this.authService.isUser();
  }
  loggedOut() {
    localStorage.removeItem('token');
  }


}
