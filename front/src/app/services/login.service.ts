import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environment';
import { SignInRequest } from '../models/requests/singIn.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient) { }
  token = '';
  _headers = new HttpHeaders({
    'Content-Type': 'application/json', // Tipo de conteúdo (geralmente JSON)
    'Authorization': `Bearer ${localStorage.getItem('token')}` // Se a API requer autenticação
  });


  signIn(request: SignInRequest): Observable<any> {
    return this.http.post(`${environment.apiUrl}Security/sign-in`, request);
  }

  newPassword(): Observable<any> {
    console.log('headers', this._headers);
    return this.http.get(`${environment.apiUrl}Security/new-password`, { headers: this._headers });
  }
}
