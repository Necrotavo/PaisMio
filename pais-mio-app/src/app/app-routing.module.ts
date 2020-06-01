import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminViewComponent } from './admin-view/admin-view.component';
import { IndexPmAppComponent } from './index-pm-app/index-pm-app.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { InventoryControlComponent } from './inventory-control/inventory-control.component';
import { OrderViewComponent } from './order-view/order-view.component';
import { RecoverPasswordComponent } from './recover-password/recover-password.component';
import { ReportComparativeComponent } from './report-comparative/report-comparative.component';
import { ReportMonthlyComponent } from './report-monthly/report-monthly.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { UserViewComponent } from './user-view/user-view.component';
import { ErrorPageComponent } from './error-page/error-page.component';


const routes: Routes = [
  {path: '', component: IndexPmAppComponent},
  {path: 'index-pm-app', component: IndexPmAppComponent},
  {path: 'admin-view', component: AdminViewComponent},
  {path: 'change-password', component: ChangePasswordComponent},
  {path: 'inventory-control', component: InventoryControlComponent},
  {path: 'order-view', component: OrderViewComponent},
  {path: 'recover-password', component: RecoverPasswordComponent},
  {path: 'report-comparative', component: ReportComparativeComponent},
  {path: 'report-monthly', component: ReportMonthlyComponent},
  {path: 'sign-in', component: SignInComponent},
  {path: 'sign-up', component: SignUpComponent},
  {path: 'user-view', component: UserViewComponent},
  {path: '**', component: ErrorPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

/** Good practice recommendation
export const routingComponents = [IndexPmAppComponent, AdminViewComponent, ChangePasswordComponent,
  InventoryControlComponent, OrderViewComponent, RecoverPasswordComponent, ReportComparativeComponent,
  ReportMonthlyComponent, SignInComponent, SignUpComponent, UserViewComponent];
*/