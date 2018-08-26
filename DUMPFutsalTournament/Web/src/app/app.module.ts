import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { PageNotFoundComponent } from './not-found.component'
import { AppRoutingModule } from './app-routing.module';
import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';

@NgModule({
  declarations: [
	AppComponent,
	PageNotFoundComponent,
	LoginComponent
  ],
  imports: [
	BrowserModule,
	FormsModule,
	HttpClientModule,
	LoginRoutingModule,
     AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }