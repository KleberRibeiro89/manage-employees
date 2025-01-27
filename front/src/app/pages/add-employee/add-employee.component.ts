import { FormsModule } from '@angular/forms';
import { Component, ViewChild } from '@angular/core';
import { RegistrationService } from '../../services/registration.service';
import { PositionResponse } from '../../models/responses/position.model';
import { HttpClientModule, HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { EmployeeRequest } from '../../models/requests/employee.request';
import { RouterModule } from '@angular/router';
import { MenuComponent } from "../../components/menu/menu.component";
import { ToastComponent } from '../../components/toast/toast.component';

@Component({
  selector: 'app-add-employee',
  imports: [HttpClientModule, CommonModule, FormsModule, RouterModule],
  providers: [RegistrationService],
  templateUrl: './add-employee.component.html',
  styleUrl: './add-employee.component.scss'
})
export class AddEmployeeComponent {

  constructor(private service: RegistrationService) { }
  positions: PositionResponse[] = [];
  phoneNumber = '';
  phones: string[] = [];
  model: EmployeeRequest = new EmployeeRequest();

  ngOnInit(): void {
    this.service.getPositions()
      .subscribe(
        (result) => {
          this.positions = result;
        },
        (error) => { },
        () => { }
      );
  }

  addPhone(): void {
    this.phones.push(this.phoneNumber);
  }


  addEmployee() {
    this.model.phones = this.phones;
    console.log('Employee', this.model);
    this.service.addEmployee(this.model)
      .subscribe(
        (response) => { },
        (error: HttpErrorResponse) => {

          alert(error.error);


        },
        () => { }
      )
  }
}
