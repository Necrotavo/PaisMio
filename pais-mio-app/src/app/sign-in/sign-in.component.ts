import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { User } from '../../models/user';
import { LoginUser } from '../../models/loginUser';
import { ApiService } from '../api.service';


@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit {

  constructor(private apiService: ApiService) { }

  loginUser = new LoginUser('', '');
  objLogin: User = new User('', '', '', '', '', 'default');

  /**constructor(private auth: AuthService) { }*/

  ngOnInit(): void {
  }

  login(){
    return this.apiService.userLogin(this.loginUser).subscribe(
      data => {
        this.objLogin = data;
      }
    );
  }

}
