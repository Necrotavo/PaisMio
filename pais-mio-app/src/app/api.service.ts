import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';
import { User } from '../models/user';
import { Input } from '../models/input';


const HttpOptions = {
  headers: new HttpHeaders({'Content-type': 'application/json'})
};
const apiURL = 'http://localhost:3000/api';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  private handleErrors<T>(operation = 'operation', result?: T){
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }

  getUser(): Observable<User[]> {
    return this.http.get<User[]>('${apiURL}')
    .pipe(
      tap(user => console.log('fetch user')),
      catchError(this.handleErrors('getUser', []))
    );
  }

  getInput(): Observable<Input[]> {
    return this.http.get<Input[]>(`${apiURL}`)
    .pipe(
      tap(input => console.log(`fetch input`)),
      catchError(this.handleErrors(`getInput`, []))
    );
  }

  getInputByID(id: string): Observable<Input> {
    const url = `${apiURL}/${id}`;
    return this.http.get<Input>(url).pipe(
      tap(_ => console.log(`fetch input id=${id}`)),
      catchError(this.handleErrors<Input>(`getInputByID id=${id}`))
    );
  }

  addInput(input: Input): Observable<Input> {
    return this.http.post<Input>(apiURL, input, HttpOptions).pipe(
      tap((i: Input) => console.log(`added input w/ id=${i.id}`)),
      catchError(this.handleErrors<Input>(`addInput`))
    );
  }

  updateInput(id: string, input: Input): Observable<any> {
    const url = `${apiURL}/${id}`;
    return this.http.put(url, input, HttpOptions).pipe(
      tap(_ => console.log(`updated input id=${id}`)),
      catchError(this.handleErrors<any>(`updateInput`))
    );
  }

  deleteInput(id: string): Observable<Input> {
    const url = `${apiURL}/${id}`;
    return this.http.delete<Input>(url, HttpOptions).pipe(
      tap(_ => console.log(`deleted input id=${id}`)),
      catchError(this.handleErrors<Input>(`deletedInput`))
    );
  }
}
