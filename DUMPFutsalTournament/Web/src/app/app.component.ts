import { Component, Renderer2 } from '@angular/core';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html'
})
export class AppComponent {
	constructor(private renderer: Renderer2) {}

	toggleNavigation() {
		const navigation = document.getElementById('nav');
		const toggleNav = document.getElementById('toggle-nav');

		if (toggleNav.className.includes('toggled'))
		{
			this.renderer.removeClass(toggleNav, 'toggled');
			this.renderer.removeClass(navigation, 'expanded');
		}
		else
		{
			this.renderer.addClass(toggleNav, 'toggled');
			this.renderer.addClass(navigation, 'expanded');
		}
	}
}