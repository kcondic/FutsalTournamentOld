import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../infrastructure/classes/user';
import { AuthService } from './auth.service';

@Component({
	template: `
    <h2>LOGIN</h2>
    <form class="form flex column" (ngSubmit)="login()">
	    <input type="text" placeholder="Korisničko ime" [(ngModel)]="username" name="username" />
	    <input type="password" placeholder="Lozinka" [(ngModel)]="password" name="password" />
	    <button type="submit">Login</button>
    </form>`
})
export class LoginComponent {
	username: string;
	password: string;

	constructor(public authService: AuthService, public router: Router) { }

	ngOnInit() {
		 if (this.authService.isAuthenticated())
			 this.router.navigateByUrl('/admin/matches');
	}

	login() {
	 this.authService.login(new User(this.username, this.password))
		  .subscribe(
		  token => {
		     localStorage.setItem('token', token);
			this.router.navigateByUrl('/admin/matches');
		},
		error => {
		  alert(`Neuspješna prijava! ${error.message}`);
		});
	}
}