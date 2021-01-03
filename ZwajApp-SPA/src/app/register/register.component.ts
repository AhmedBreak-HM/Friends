import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { error } from 'util';
import { AlertifyService } from '../services/alertify.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  @Input() ValuesFromHome: any;
  @Output() cancelRegister = new EventEmitter();
  model: any = {username: '', password: ''};

  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }
  register() {
    this.authService.register(this.model).subscribe( () => console.log('register don!'),
     error => this.alertify.error(error));
  }
  cancel() {
    this.cancelRegister.emit(false);
  }

}
