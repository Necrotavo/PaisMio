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
import { RouterGuardGuard } from './router-guard.guard';
import { ReportViewComponent } from './report-view/report-view.component';


const routes: Routes = [
  {path: '', component: IndexPmAppComponent , canActivate: [RouterGuardGuard]},
  {path: 'index-pm-app', component: IndexPmAppComponent, canActivate: [RouterGuardGuard]},
  {path: 'admin-view', component: AdminViewComponent, canActivate: [RouterGuardGuard]},
  {path: 'change-password', component: ChangePasswordComponent, canActivate: [RouterGuardGuard]},
  {path: 'inventory-control', component: InventoryControlComponent, canActivate: [RouterGuardGuard]},
  {path: 'order-view', component: OrderViewComponent, canActivate: [RouterGuardGuard]},
  {path: 'recover-password', component: RecoverPasswordComponent, canActivate: [RouterGuardGuard]},
  {path: 'report-view', component: ReportViewComponent, canActivate: [RouterGuardGuard]},
  {path: 'report-comparative', component: ReportComparativeComponent, canActivate: [RouterGuardGuard]},
  {path: 'report-monthly', component: ReportMonthlyComponent, canActivate: [RouterGuardGuard]},
  {path: 'sign-in', component: SignInComponent},
  {path: 'sign-up', component: SignUpComponent, canActivate: [RouterGuardGuard]},
  {path: 'user-view', component: UserViewComponent, canActivate: [RouterGuardGuard]},
  {path: '**', component: ErrorPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
