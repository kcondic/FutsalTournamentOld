import { Injectable } from '@angular/core';
import { CanActivate, CanActivateChild, CanLoad } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {
	constructor(private jwtHelper: JwtHelperService) { }

	canActivate(): boolean {
		return this.isAuthenticated();
	}

	canActivateChild(): boolean {
		return this.canActivate();
	}

	canLoad(): boolean {
		return this.isAuthenticated();
	}

	 isAuthenticated(): boolean {
		  const token = localStorage.getItem('token');
		 console.log(true);
		return token !== null && !this.jwtHelper.isTokenExpired(token);
	}
}