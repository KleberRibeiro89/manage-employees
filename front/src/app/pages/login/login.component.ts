import { serverRoutes } from './../../app.routes.server';
import { SignInRequest } from './../../models/requests/singIn.model';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LoginService } from '../../services/login.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-login',
  imports: [FormsModule, HttpClientModule],
  providers: [LoginService],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  model: SignInRequest = new SignInRequest();
  constructor(private service: LoginService) {
  }

  signIn(): void {
    this.service.signIn(this.model);
  }
}
