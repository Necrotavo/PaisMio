import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { User } from 'src/models/user';
import { UserChangePass } from 'src/models/userChangePass';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {

  constructor(private apiService: ApiService) { }

  /** Object Declaration */
  user: UserChangePass;
  passEqual = false;

  /** Models */
  localUser = new User('', '', '', '', '', '');
  userChangePass = new UserChangePass('', '', '', '');

  /** Data return objects */
  objUser: boolean;

  ngOnInit(): void {
    this.localUser = JSON.parse(localStorage.getItem('user logged'));
    this.userChangePass.correo = this.localUser.correo;
  }

  /** Used to post a password change */
  postChangePass() {
    this.apiService.changePassword(this.userChangePass).subscribe(
      data => {
        this.objUser = data;
        this.showMessage(this.objUser);
      }
    );
  }

  showMessage(isValid: boolean) {
    if (isValid) {
      Swal.fire({
        icon: 'success',
        title: '!Listo!',
        text: 'Cambio de contraseña efectuado',
        showConfirmButton: false,
        timer: 1500
      });
    } else {
      Swal.fire({
        icon: 'warning',
        title: '!Ups!',
        text: 'Ocurrió algún error, vuelve a intentarlo',
        showConfirmButton: false,
        timer: 1500
      });
    }
  }

  /** Used to compare the password */
  comparePass() {
    if (this.userChangePass.newPass === this.userChangePass.newPassR) {
      return true;
    } else {
      return false;
    }
  }

  /** Used to validate and check the new password */
  checkNewPassR(){
    if (this.userChangePass.newPassR === '' || this.userChangePass.newPass === ''
    || this.userChangePass.newPassR === null || this.userChangePass.newPass === null){
      return false;
    }else{
      return true;
    }
  }
}
