import { FormsModule } from '@angular/forms';
import { Component } from '@angular/core';
import { RegistrationService } from '../../services/registration.service';
import { PositionResponse } from '../../models/responses/position.model';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { EmployeeRequest } from '../../models/requests/employee.request';
import { RouterModule } from '@angular/router';

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
    console.log(this.phoneNumber);
    this.phones.push(this.phoneNumber);

    console.log(this.phones);
  }


  addEmployee() {
    this.model.phones = this.phones;
    console.log('Employee', this.model);
    this.service.addEmployee(this.model)
      .subscribe(
        (response) => { },
        (error) => {
          console.log(error);
        },
        () => { }
      )
  }
}
