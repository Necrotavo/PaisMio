import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpErrorResponse, HttpRequest } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';

import { User } from '../models/user';
import { LoginUser } from '../models/loginUser';

const HttpOptions = {
  headers: new HttpHeaders({'Content-type': 'application/json'})
};

const loginURL = 'https://www.spepaismio.tk/WS_Cliente.svc/ListarClientes';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  private handleErrors<T>(operation = 'operation', result?: T){
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }

  loginUser(loginUser: LoginUser): Observable<User> {
    return this.http.post<User>(loginURL, loginUser, HttpOptions).pipe(
      tap((i: User) => console.log(`logged in as user w/ id=${i.correo}`)),
      catchError(this.handleErrors<User>(`loginUser`))
    );
  }

}
