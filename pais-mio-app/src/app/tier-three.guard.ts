import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from 'src/models/user';

@Injectable({
  providedIn: 'root'
})
export class TierThreeGuard implements CanActivate {

  userIn = new User('', '', '', '', '', '');

  constructor(private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    this.userIn = JSON.parse(localStorage.getItem('user logged'));
    if (this.userIn === null) {
      this.router.navigateByUrl('/sign-in');
      return false;
    } else {
      console.log('Guard: ' + this.userIn.rol);
      return true;
    }
  }
}
