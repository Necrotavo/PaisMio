import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import Swal from 'sweetalert2';
import { User } from 'src/models/user';

@Component({
  selector: 'app-recover-password',
  templateUrl: './recover-password.component.html',
  styleUrls: ['./recover-password.component.scss']
})
export class RecoverPasswordComponent implements OnInit {

  constructor(private apiService: ApiService) { }

  /**Models */
  correo: string = '';
  userModel = new User('', '', '', '', '', 'OPERARIO');

  /**Objects return */
  objUser: string = "";


  ngOnInit(): void {
  }

  passwordRecover(){
    this.apiService.passwordRecovery(this.userModel).subscribe(  
      data => {
        this.objUser = data;
        Swal.fire({
          icon: 'success',
          title: '!Listo!',
          text: 'una nueva contraseña ha sido enviada a tu correo, revisa tu email por favor',
          showConfirmButton: false,
          timer: 2500
        });
      }
    );
  }
}
