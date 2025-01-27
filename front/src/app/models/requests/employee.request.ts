
export class EmployeeRequest {
  firstName: string = '';
  lastName: string = '';
  email: string = '';
  docNumer: string = '';
  password: string = '';
  dateOfBirth: Date = new Date();
  managerId: string = '00000000-0000-0000-0000-000000000000';
  newPasswordRequired: boolean = false;
  positionEmployeeId: string = '00000000-0000-0000-0000-000000000000';
  phones: string[] = [];
}
