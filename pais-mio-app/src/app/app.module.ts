/** Main imports */
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

/** Routing imports */
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IndexPmAppComponent } from './index-pm-app/index-pm-app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { RecoverPasswordComponent } from './recover-password/recover-password.component';
import { ReportComparativeComponent } from './report-comparative/report-comparative.component';
import { ReportMonthlyComponent } from './report-monthly/report-monthly.component';
import { AdminViewComponent } from './admin-view/admin-view.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { InventoryControlComponent } from './inventory-control/inventory-control.component';
import { OrderViewComponent } from './order-view/order-view.component';
import { UserViewComponent } from './user-view/user-view.component';
import { ErrorPageComponent } from './error-page/error-page.component';

/** Extra modules imports */
import {FormsModule, ReactiveFormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import { ClientComponent } from './client/client.component';
import { ApiService } from './api.service';
import { Ng2SearchPipeModule } from 'ng2-search-filter';

@NgModule({
  declarations: [
    AppComponent,
    IndexPmAppComponent,
    NavbarComponent,
    SignInComponent,
    SignUpComponent,
    RecoverPasswordComponent,
    ReportComparativeComponent,
    ReportMonthlyComponent,
    AdminViewComponent,
    ChangePasswordComponent,
    InventoryControlComponent,
    OrderViewComponent,
    UserViewComponent,
    ErrorPageComponent,
    ClientComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    Ng2SearchPipeModule
  ],
  providers: [ApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
