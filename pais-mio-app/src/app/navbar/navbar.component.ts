import { Component, OnInit, OnDestroy } from '@angular/core';
import { DataService } from '../data.service';
import { Order } from 'src/models/order';
import { User } from 'src/models/user';
import { ApiService } from '../api.service';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { Observable } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  order: Order;
  orderList: Order[];
  userList: User[];
  count: number;
  activeMessage: string;
  activeRole: string;
  dispach: boolean;

  userIn = new User('', '', '', '', '', '');

  isLoggedIn$: Observable<boolean>;
  isAdmin$: Observable<boolean>;
  isSupervisor$: Observable<boolean>;

  constructor(private data: DataService, private apiService: ApiService, private router: Router,
              private authService: AuthService) { }

  ngOnInit(): void {

    this.kickUser();

    this.data.activeOrder.subscribe(order => this.order = order);

    this.isLoggedIn$ = this.authService.isLoggedIn;
    this.isAdmin$ = this.authService.isAdmin;
    this.isSupervisor$ = this.authService.isSupervisor;

    /** Checks user status for navbar */
    this.authService.isLoggedInMethod();

    /** Gets all Orders on Init */
    this.apiService.getOrder().subscribe(
      data => {
        this.orderList = data;
        this.count = this.orderList.length;
        if (this.count === 1) {
          this.activeMessage = this.count + ' Pedido activo';
        } else if (this.count === 0) {
          this.activeMessage = 'No hay pedidos activos';
        } else {
          this.activeMessage = this.count + ' Pedidos activos';
        }
      }
    );

    /** order is dispach */
    this.data.isDispach.subscribe((dispach) => {
      this.dispach = dispach;
      this.checkReload();
      this.navbarReloadOrder();
    });
  }

  navbarReloadOrder() {
    this.apiService.getOrder().subscribe(
      data => {
        this.orderList = data;
        this.count = this.orderList.length;
        if (this.count === 1) {
          this.activeMessage = this.count + ' Pedido activo';
        } else if (this.count === 0) {
          this.activeMessage = 'No hay pedidos activos';
        } else {
          this.activeMessage = this.count + ' Pedidos activos';
        }
      }
    );
  }

  checkReload() {
    if (this.dispach) {
      console.log('SE HA DESPACHADO UNA ORDEN');
    } else {
      console.log('DISPACH ES FALSE');
    }
  }

  newOrder(i: number) {
    this.data.changeOrder(this.orderList[i]);
    localStorage.setItem('active order', JSON.stringify(this.orderList[i]));
  }

  checkUserRole() {
    this.userIn = JSON.parse(localStorage.getItem('user logged'));
    this.activeRole = this.userIn.rol;
  }

  logout() {
    this.authService.logout();
  }

  /** Used to kick any user that has been disabled */
  kickUser() {

    this.userIn = JSON.parse(localStorage.getItem('user logged'));

    for (const i of this.userList) {
      if (this.userIn.correo === i.correo) {
         this.userIn = i;
         if (this.userIn.estado === 'DESHABILITADO') {
          let timerInterval;
          Swal.fire({
            title: 'Error: este usuario ha sido deshabilitado',
            html: 'Sera redirigido a la de ingreso en <b></b> milisegundos.',
            timer: 7000,
            timerProgressBar: true,
            onBeforeOpen: () => {
              Swal.showLoading();
              timerInterval = setInterval(() => {
                const content = Swal.getContent();
                if (content) {
                  const b = content.querySelector('b');
                  if (b) {
                    b.textContent = Swal.getTimerLeft().toString();
                  }
                }
              }, 100);
            },
            onClose: () => {
              clearInterval(timerInterval);
              this.authService.logout();
              this.router.navigateByUrl('/');
            }
          }).then((result) => {
            /* Read more about handling dismissals below */
            if (result.dismiss === Swal.DismissReason.timer) {
              console.log('I was closed by the timer');
            }
          });
        }
      }
    }

  }

  updateUserList(){
    this.apiService.getUser().subscribe(
      data => {
        this.userList = data;
      }
    );
  }

}
