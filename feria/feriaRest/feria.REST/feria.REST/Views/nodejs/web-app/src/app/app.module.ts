import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms'
import { HttpClientModule, HttpClient } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AdminComponent } from './admin/admin.component';
import { RegisterComponent } from './admin/grower/register/register.component';
import { UpdateComponent } from './admin/grower/update/update.component';
import { GrowerService } from './services/grower.service';



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AdminComponent,
    RegisterComponent,
    UpdateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [GrowerService],
  bootstrap: [AppComponent]
})
export class AppModule { }
