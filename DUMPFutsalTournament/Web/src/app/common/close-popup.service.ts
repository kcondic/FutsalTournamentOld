import { Injectable } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Injectable()
export class ClosePopupService {
	constructor(private router: Router) { }

	 close(routeParent: ActivatedRoute) {
		  this.router.navigate([{ outlets: { popup: null } }], { relativeTo: routeParent });
	 }
}