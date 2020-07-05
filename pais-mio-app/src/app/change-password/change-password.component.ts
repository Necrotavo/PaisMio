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

  /** Models */
  localUser = new User('', '', '', '', '', '');
  userChangePass = new UserChangePass('', '', '', '');

  /** Data return objects */
  objUser: boolean;

  ngOnInit(): void {
    this.localUser = JSON.parse(localStorage.getItem('user logged'));
    this.userChangePass.correo = this.localUser.correo;
  }

  postChangePass(){
    this.apiService.changePassword(this.userChangePass).subscribe(
      data => {
        this.objUser = data;
        console.log(this.objUser);
        this.showMessage(this.objUser);
      }
    );
  }

  showMessage(isValid: boolean){
    if (isValid){
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

  comparePass(){
    if (this.userChangePass.newPass === this.userChangePass.newPassR){
      return true;
    }else{
      return false;
    }
  }
}
