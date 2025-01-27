export interface EmployeeResponse {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  docNumer: string;
  password: string;
  dateOfBirth: Date;
  positionEmployeeId: string;
  positionEmployee: string;
  managerId: string;
  phones: PhoneEmployeeResponse[];
}

export interface PhoneEmployeeResponse {
  id: string;
  employeeId: string;
  phoneNumber: string;
}
