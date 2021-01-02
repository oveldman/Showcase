import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Bearer } from '../../models/authentication/bearer'
import { Login } from '../../models/authentication/login'

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient) { }

  login(email: string, password: string ) : Observable<Bearer>  {
    var url = "api/authentication/login";

    let login: Login = {
      Username: email,
      Password: password
    };

    return this.http.post<Bearer>(url, login).pipe(map(bearerResponse => {
      if (bearerResponse.Succes) {
        localStorage.setItem("id_token", bearerResponse.AccessToken); 
      }
      return bearerResponse;
     }));
  }

  logout() {
    localStorage.removeItem("id_token");
  }
}
