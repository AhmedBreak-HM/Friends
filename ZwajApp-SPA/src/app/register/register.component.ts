import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
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

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }
  register() {
    this.authService.register(this.model).subscribe( () => console.log('register don!'), () => console.log(' error!'));
  }
  cancel() {
    this.cancelRegister.emit(false);
  }

}
