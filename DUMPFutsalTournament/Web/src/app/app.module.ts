import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { PageNotFoundComponent } from './not-found.component'
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  declarations: [
	AppComponent,
	PageNotFoundComponent
  ],
  imports: [
	BrowserModule,
	HttpClientModule,
     AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
