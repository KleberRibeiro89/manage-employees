import { Component } from '@angular/core';
import { LoginService } from '../../services/login.service';
import { Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-new-password',
  imports: [HttpClientModule],
  providers:[LoginService],
  templateUrl: './new-password.component.html',
  styleUrl: './new-password.component.scss'
})
export class NewPasswordComponent {

  constructor(
    private service: LoginService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.service.newPassword().subscribe(
      (response: boolean) => {
        if (!response) {
          this.router.navigate(['home']);
        }
      },
      (error) => {

      }
    );
  }
}
