import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../infrastructure/classes/user';
import { AuthService } from './auth.service';

@Component({
	template: `
    <h2>LOGIN</h2>
    <form (ngSubmit)="login()">
	    <input type="text" [(ngModel)]="username" name="username" />
	    <input type="password" [(ngModel)]="password" name="password" />
	    <button type="submit">Login</button>
    </form>`
})
export class LoginComponent {
	username: string;
	password: string;

	constructor(public authService: AuthService, public router: Router) { }

	login() {
	 this.authService.login(new User(this.username, this.password))
		  .subscribe(
		  token => {
		     localStorage.setItem('token', token);
			this.router.navigateByUrl('/admin');
		},
		error => {
		  alert(`Neuspješna prijava! ${error.message}`);
		});
	}
}