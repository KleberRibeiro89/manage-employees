import { SignInRequest } from './../../models/requests/singIn.model';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LoginService } from '../../services/login.service';
import { HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule, HttpClientModule],
  providers: [LoginService],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  model: SignInRequest = new SignInRequest();
  isModalVisible = false;
  constructor(
    private service: LoginService,
    private router: Router) {
  }

  signIn(): void {

    this.service
      .signIn(this.model)
      .subscribe(
        (response) => {
          localStorage.setItem('token', response.token);
          this.router.navigate(['new-password']);
        },
        (error) => {
          console.error('Erro:', error);
        },
        () => {
          console.log("complete")
          this.isModalVisible = false;
        }
      );
  }
}
