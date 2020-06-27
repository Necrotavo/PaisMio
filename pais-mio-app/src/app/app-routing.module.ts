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
import { TierOneGuard } from './tier-one.guard';
import { TierTwoGuard } from './tier-two.guard';
import { TierThreeGuard } from './tier-three.guard';
import { ReportViewComponent } from './report-view/report-view.component';


const routes: Routes = [
  {path: '', component: IndexPmAppComponent , canActivate: [TierThreeGuard]},
  {path: 'index-pm-app', component: IndexPmAppComponent, canActivate: [TierThreeGuard]},
  {path: 'admin-view', component: AdminViewComponent, canActivate: [TierOneGuard]},
  {path: 'change-password', component: ChangePasswordComponent, canActivate: [TierThreeGuard]},
  {path: 'inventory-control', component: InventoryControlComponent, canActivate: [TierTwoGuard]},
  {path: 'order-view', component: OrderViewComponent, canActivate: [TierThreeGuard]},
  {path: 'recover-password', component: RecoverPasswordComponent, canActivate: [TierThreeGuard]},
  {path: 'report-view', component: ReportViewComponent, canActivate: [TierOneGuard]},
  {path: 'report-comparative', component: ReportComparativeComponent, canActivate: [TierOneGuard]},
  {path: 'report-monthly', component: ReportMonthlyComponent, canActivate: [TierOneGuard]},
  {path: 'sign-in', component: SignInComponent},
  {path: 'sign-up', component: SignUpComponent, canActivate: [TierThreeGuard]},
  {path: 'user-view', component: UserViewComponent, canActivate: [TierThreeGuard]},
  {path: '**', component: ErrorPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
