import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { User } from '../../models/user';
import { LoginUser } from '../../models/loginUser';
import { Router} from '@angular/router';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit {

  constructor(private auth: AuthService, private router: Router) { }

  loginUser = new LoginUser('', '');
  objLogin: User = new User('', '', '', '', '', 'default');

  /** Validations */
  isWrong = false;

  ngOnInit(): void {
    if (localStorage.getItem('user logged') !== null){
      this.router.navigateByUrl('/');
    }
  }

  login(){
    return this.auth.userLogin(this.loginUser).subscribe(
      data => {
        this.objLogin = data;
        if (!this.objLogin){
          this.isWrong = true;
        } else {
          this.isWrong = false;
          localStorage.setItem('user logged', JSON.stringify(this.objLogin));
          localStorage.setItem('logged username', JSON.stringify(this.objLogin.nombre));
          localStorage.setItem('logged role', JSON.stringify(this.objLogin.rol));
          this.router.navigateByUrl('/');
          this.auth.overloadUser();
        }
      }
    );
  }

  resetIsWrong(){
    this.isWrong = false;
  }
}
