import { HttpClientModule, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RegistrationService } from '../../services/registration.service';
import { EmployeeResponse } from '../../models/responses/employee.response';
import { MenuComponent } from '../../components/menu/menu.component';

@Component({
  selector: 'app-home',
  imports: [RouterModule, CommonModule, HttpClientModule, RouterModule],
  providers: [RegistrationService],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  constructor(
    private service: RegistrationService,
    private router: Router) { }
  employees: EmployeeResponse[] = [];
  ngOnInit(): void {
    this.service.getEmployees()
      .subscribe(
        (response) => {
          this.employees = response;
        },
        (error:HttpErrorResponse) => {
          alert(error.error);

         },
        () => { }
      );
  }

  update(id:string){
    this.router.navigate([`/update-employee/${id}`])
  }

}
