import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { User } from '../../models/user';
import { LoginUser } from '../../models/loginUser';


@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit {

  loginUser = new LoginUser('', '');
  objLogin: User;

  constructor(private auth: AuthService) { }

  ngOnInit(): void {
  }

  login(){
    return this.auth.loginUser(this.loginUser).subscribe(
      data => {
        this.objLogin = data;
      }
    );
  }

}
