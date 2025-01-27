import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from '../../environment';
import { PositionResponse } from "../models/responses/position.model";
import { EmployeeRequest } from "../models/requests/employee.request";
import { EmployeeResponse } from "../models/responses/employee.response";
import { UpdateEmployeeRequest } from "../models/requests/update-employee.request";

@Injectable({
  providedIn: 'root'
})

export class RegistrationService {

  _headers = new HttpHeaders({
    'Content-Type': 'application/json', // Tipo de conteúdo (geralmente JSON)
    'Authorization': `Bearer ${localStorage.getItem('token')}` // Se a API requer autenticação
  });

  constructor(
    private http: HttpClient) { }

  getPositions(): Observable<PositionResponse[]> {
    return this.http.get<PositionResponse[]>(`${environment.apiUrl}Registration/positions`, { headers: this._headers });
  }

  addEmployee(employeeRequest: EmployeeRequest): Observable<object> {

    return this.http.post(`${environment.apiUrl}registration/employee`, employeeRequest, { headers: this._headers });
  }

  getEmployee(id: string): Observable<EmployeeResponse> {
    return this.http.get<EmployeeResponse>(`${environment.apiUrl}registration/employee/${id}`, { headers: this._headers });
  }

  updateEmployee(request: UpdateEmployeeRequest): Observable<object> {
    return this.http.put(`${environment.apiUrl}registration/employee`, request, { headers: this._headers });
  }
}
