import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
	selector: 'admin-navigation',
	templateUrl: './admin-navigation.component.html'
})
export class AdminNavigationComponent {
	constructor(private router: Router) { }

	logout() {
		localStorage.removeItem('token');
		this.router.navigateByUrl('/matches/active');
	}
}