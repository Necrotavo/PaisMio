import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpErrorResponse, HttpRequest } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';

import { User } from '../models/user';
import { LoginUser } from '../models/loginUser';
import { Router } from '@angular/router';


const HttpOptions = {
  headers: new HttpHeaders({ 'Content-type': 'application/json' })
};

const userLoginPOST = 'https://www.spepaismio.tk/WS_Usuario.svc/Login';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private adminRole: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private supervisorRole: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  userIn = new User('', '', '', '', '', '');

  /**
  private userName = new BehaviorSubject<string>(localStorage.getItem('logged username'));
  private userRole = new BehaviorSubject<string>(localStorage.getItem('logged role'));
  */

  public userData$: Observable<User>;

  constructor(private http: HttpClient, private router: Router) { }

  private handleErrors<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }

  userLogin(user: LoginUser): Observable<User> {
    return this.userData$ = this.http.post<User>(userLoginPOST, user, HttpOptions).pipe(
      tap((i: User) => console.log('User login success')),
      catchError(this.handleErrors<User>(`userLogin`))
    );
  }

  overloadUser() {
    this.loggedIn.next(true);
    this.userIn = JSON.parse(localStorage.getItem('user logged'));
    console.log('overload');
    if (this.userIn.rol === 'ADMINISTRADOR') {
      console.log('admin overload');
      this.adminRole.next(true);
    } else if (this.userIn.rol === 'SUPERVISOR') {
      console.log('super overload');
      this.supervisorRole.next(true);
    }
  }

  async isLoggedInMethod() {
    this.userData$ = JSON.parse(localStorage.getItem('user logged'));
    console.log('Imprimo: ' + this.userData$);
    if (localStorage.getItem('user logged') === null) {
      this.loggedIn.next(false);
      this.adminRole.next(false);
      this.supervisorRole.next(false);
    } else {
      this.loggedIn.next(true);
      this.userIn = JSON.parse(localStorage.getItem('user logged'));
      console.log('Logged as: ' + this.userIn.rol);
      if (this.userIn.rol === 'ADMINISTRADOR') {
        this.adminRole.next(true);
      } else if (this.userIn.rol === 'SUPERVISOR') {
        this.supervisorRole.next(true);
      }
      this.supervisorRole.next(true);
    }
  }

  async logout() {
    localStorage.removeItem('user logged');
    localStorage.removeItem('logged username');
    localStorage.removeItem('logged role');
    localStorage.removeItem('active order');
    this.loggedIn.next(false);
    this.adminRole.next(false);
    this.supervisorRole.next(false);
    this.router.navigateByUrl('/sign-in');
  }

  get isLoggedIn() {
    return this.loggedIn.asObservable();
  }

  get isAdmin() {
    return this.adminRole.asObservable();
  }

  get isSupervisor() {
    return this.supervisorRole.asObservable();
  }

}
