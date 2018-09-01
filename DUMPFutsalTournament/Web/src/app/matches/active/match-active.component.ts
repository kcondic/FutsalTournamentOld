import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatchService } from '../match.service';
import { Match } from '../../infrastructure/classes/match';
import { interval, Subscription } from 'rxjs';

@Component({
	templateUrl: './match-active.component.html'
})
export class MatchActiveComponent implements OnInit {
	match: Match;
	minute: number = 0;
	source = interval(10000);
	hasLoaded: boolean = false;
	timeSubscription: Subscription;

	constructor(private service: MatchService) { }

	 ngOnInit() {
		this.getActiveMatch();
		this.timeSubscription = this.source.subscribe(() => {
			this.getActiveMatch();
		});
	 }

	 ngOnDestroy() {
		 this.timeSubscription.unsubscribe();
	 }

	 getActiveMatch() : void {
		this.service.getActiveMatch().subscribe(activeMatch => {
			this.match = activeMatch;
			this.hasLoaded = true;
			if (this.match) {
				this.minute = this.match.minute;
			}
		});
	 }
}