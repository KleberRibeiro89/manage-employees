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

  signIn(request: SignInRequest):Observable<any>{
    return this.http.post(`${environment.apiUrl}`, request);
  }
}
