import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpErrorResponse, HttpRequest } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';

import { User } from '../models/user';
import { LoginUser } from '../models/loginUser';
import {Router} from '@angular/router';


const HttpOptions = {
  headers: new HttpHeaders({'Content-type': 'application/json'})
};

const userLoginPOST = 'https://www.spepaismio.tk/WS_Usuario.svc/Login';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public userData$: Observable<User>;

  constructor(private http: HttpClient, private router: Router) { }

  private handleErrors<T>(operation = 'operation', result?: T){
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }

  userLogin(user: LoginUser): Observable<User> {
    return this.userData$ = this.http.post<User>(userLoginPOST, user, HttpOptions).pipe(
      tap((i: User) => console.log(`added user w/ id=${i}`)),
      catchError(this.handleErrors<User>(`addUser`))
    );
  }

  async isLoggedIn() {
    this.userData$ = JSON.parse(localStorage.getItem('user logged'));
    console.log('Imprimo: ' + this.userData$);
    if (localStorage.getItem('user logged')){
    }
  }

  async logout() {
    localStorage.removeItem('user logged');
    this.router.navigateByUrl('/sign-in');
  }

}
