/** Main imports */
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

/** Routing imports */
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IndexPmAppComponent } from './index-pm-app/index-pm-app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { RecoverPasswordComponent } from './recover-password/recover-password.component';
import { AdminViewComponent } from './admin-view/admin-view.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { InventoryControlComponent } from './inventory-control/inventory-control.component';
import { OrderViewComponent } from './order-view/order-view.component';
import { UserViewComponent } from './user-view/user-view.component';
import { ErrorPageComponent } from './error-page/error-page.component';

/** Extra modules imports */
import {FormsModule, ReactiveFormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import { ApiService } from './api.service';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { AuthService } from './auth.service';
import { ReportViewComponent } from './report-view/report-view.component';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';

@NgModule({
  declarations: [
    AppComponent,
    IndexPmAppComponent,
    NavbarComponent,
    SignInComponent,
    RecoverPasswordComponent,
    AdminViewComponent,
    ChangePasswordComponent,
    InventoryControlComponent,
    OrderViewComponent,
    UserViewComponent,
    ErrorPageComponent,
    ReportViewComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    Ng2SearchPipeModule,
    AutocompleteLibModule
  ],
  providers: [ApiService, AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
