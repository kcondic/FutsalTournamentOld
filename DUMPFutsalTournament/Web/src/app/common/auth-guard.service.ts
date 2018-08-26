import { Injectable } from '@angular/core';
import { Router, CanActivate, CanActivateChild, CanLoad } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {
	constructor(private service: AuthService, private router: Router) { }

	canActivate(): boolean {
		return this.service.isAuthenticated();
	}

	canActivateChild(): boolean {
		return this.canActivate();
	}

	 canLoad(): boolean {
		const isAuthenticated = this.service.isAuthenticated();
		if (!isAuthenticated)
			 this.router.navigateByUrl('/login');
		return isAuthenticated;
	}
}