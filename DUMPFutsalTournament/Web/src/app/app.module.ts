import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { PageNotFoundComponent } from './common/not-found.component'
import { AppRoutingModule } from './app-routing.module';
import { LoginRoutingModule } from './common/login-routing.module';
import { LoginComponent } from './common/login.component';
import { JwtModule } from '@auth0/angular-jwt';
import { ClosePopupService } from './common/close-popup.service';
import { MatchTypeTranslationService } from './common/match-type-translation.service';
import { SocketService } from './common/socket.service';
import { SocketIoModule, SocketIoConfig } from 'ng-socket-io';

const config: SocketIoConfig = { url: 'http://localhost:50005' };

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
	AppRoutingModule,
	JwtModule.forRoot({
		 config: {
			  tokenGetter: () => {
				  return localStorage.getItem('token');
			  }
		 }
	}),
	SocketIoModule.forRoot(config)
  ],
  providers: [ ClosePopupService, MatchTypeTranslationService, SocketService ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }