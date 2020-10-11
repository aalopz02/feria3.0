import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AdminComponent } from './admin/admin.component';
import { RegisterComponent } from './admin/grower/register/register.component';
import { UpdateComponent } from './admin/grower/update/update.component';


const routes: Routes = [
  {path:  '', component: HomeComponent},
  {path:  'admin', component: AdminComponent},
  {path: 'admin/p-register', component: RegisterComponent},
  {path: 'admin/p-update', component: UpdateComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
