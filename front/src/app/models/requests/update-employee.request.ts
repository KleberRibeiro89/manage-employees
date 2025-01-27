import { PhoneEmployeeResponse } from "../responses/employee.response";

export class UpdateEmployeeRequest {
  id = '00000000-0000-0000-0000-000000000000';
  firstName: string = '';
  lastName: string = '';
  password: string = '';
  dateOfBirth: Date = new Date();
  dateOfBirth2: string = '';
  phone: string = '';
  email: string = '';
  managerId = '00000000-0000-0000-0000-000000000000';
  managerName = '';
  positionEmployeeId = '00000000-0000-0000-0000-000000000000';
  docNumber = '';
  phones: PhoneEmployeeResponse[] = [];
}
