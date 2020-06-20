import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { User } from '../../models/user';
import { LoginUser } from '../../models/loginUser';
import { ApiService } from '../api.service';
import {Router} from '@angular/router';


@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit {

  constructor(private apiService: ApiService, private router: Router) { }

  loginUser = new LoginUser('', '');
  objLogin: User = new User('', '', '', '', '', 'default');
  isCorrect = false;

  /** constructor(private auth: AuthService) { } */

  ngOnInit(): void {
  }

  login(){
    return this.apiService.userLogin(this.loginUser).subscribe(
      data => {
        this.objLogin = data;
        this.isCorrect = true;

        localStorage.setItem('user logged', JSON.stringify(this.objLogin));
        this.router.navigateByUrl('/index-pm-app');
      }
    );
  }

}
