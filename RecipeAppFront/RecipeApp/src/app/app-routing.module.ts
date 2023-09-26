import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegistrationComponent } from './registration/registration.component';
import { LoginComponent } from './login/login.component'; 
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { CookDashboardComponent } from './cook-dashboard/cook-dashboard.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'registration', component: RegistrationComponent }, 
  { path: 'login', component: LoginComponent }, 
  { path: 'admin', component: AdminDashboardComponent},
  { path: 'cook', component: CookDashboardComponent},
  {path: 'user', component:UserDashboardComponent}

  

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
