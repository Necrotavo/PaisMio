import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { User } from 'src/models/user';


@Injectable({
  providedIn: 'root'
})


export class RouterGuardGuard implements CanActivate {

  userIn = new User('', '', '', '', '', '');

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    this.userIn = JSON.parse(localStorage.getItem('user logged'));
    if (this.userIn === null) {
      this.router.navigateByUrl('/sign-in');
      return false;
    }
    if (this.userIn.rol === 'ADMINISTRADOR'){
      console.log('Guard: ' + this.userIn.rol);
      return true;
    } else if (this.userIn.rol === 'SUPERVISOR'){
      console.log('Guard: ' + this.userIn.rol);
      this.router.navigateByUrl('/');
      return true;
    }
  }

}
