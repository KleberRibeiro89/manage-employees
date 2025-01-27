import { HttpClientModule, HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Component } from '@angular/core';
import { UpdateEmployeeRequest } from '../../models/requests/update-employee.request';
import { FormsModule } from '@angular/forms';
import { RegistrationService } from '../../services/registration.service';
import { Subscription } from 'rxjs';
import { PositionResponse } from '../../models/responses/position.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-employee',
  imports: [FormsModule, HttpClientModule, CommonModule, RouterModule],
  providers: [RegistrationService],
  templateUrl: './update-employee.component.html',
  styleUrl: './update-employee.component.scss'
})
export class UpdateEmployeeComponent {
  constructor(
    private service: RegistrationService,
    private route: ActivatedRoute,
    private router: Router) { }

  model: UpdateEmployeeRequest = new UpdateEmployeeRequest();
  id = '';
  routeParamsSubscription: Subscription | undefined;
  positions: PositionResponse[] = [];

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.service.getPositions()
      .subscribe(
        (result) => {
          this.positions = result;
        },
        (error) => { },
        () => { }
      );


    this.service.getEmployee(this.id)
      .subscribe(
        (response) => {
          this.model.id = response.id;
          this.model.dateOfBirth = response.dateOfBirth;
          this.model.firstName = response.firstName;
          this.model.lastName = response.lastName;
          this.model.positionEmployeeId = response.positionEmployeeId;
          this.model.email = response.email;
          this.model.managerId = response.managerId;
          this.model.dateOfBirth2 = response.dateOfBirth.toString().split('T')[0];
          this.model.docNumber = response.docNumer;
          this.model.phones = response.phones;
          console.log(response);
        },
        (error) => { console.log(error) },
        () => { }
      );
  }

  update(): void {
    console.log(this.model);
    this.service.updateEmployee(this.model)
      .subscribe(
        (response) => {
          alert('alterado com sucesso');
          this.router.navigate(['home']);

        },
        (error) => {
          alert(error.error);

        },
        () => { },
      )
  }

  delete(id: string): void {

    this.service.deleteEmployee(id)
      .subscribe(
        (response) => {
          alert('excluido com sucesso');

          this.router.navigate(['home']);

        },
        (error: HttpErrorResponse) => {
          alert(error.error);

        },
        () => { }
      )
  }
}
