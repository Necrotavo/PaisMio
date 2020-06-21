import { Component, OnInit } from '@angular/core';
import { User } from 'src/models/user';



@Component({
  selector: 'app-user-view',
  templateUrl: './user-view.component.html',
  styleUrls: ['./user-view.component.scss']
})
export class UserViewComponent implements OnInit {

  userIn = new User('', '', '', '', '', '');

  constructor() { }

  ngOnInit(): void {

    this.userIn = JSON.parse(localStorage.getItem('user logged'));

  }

}
