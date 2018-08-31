import { Component, OnInit } from '@angular/core';
import { MatchService } from '../match.service';
import { Match } from '../../infrastructure/classes/match';
import { interval } from 'rxjs';

@Component({
	templateUrl: './match-active.component.html'
})
export class MatchActiveComponent implements OnInit {
	match: Match;
	minute: number = 0;
	second: number = 0;
	source = interval(1000);
	hasLoaded: boolean = false;

	constructor(private service: MatchService) { }

	 ngOnInit() {
		this.getActiveMatch();
		this.source.subscribe(() => {
			this.getActiveMatch();
		});
	 }

	 getActiveMatch() : void {
		this.service.getActiveMatch().subscribe(activeMatch => {
			this.match = activeMatch;
			this.hasLoaded = true;
			this.second = this.match.second;
			this.minute = this.match.minute;
		});
	 }
}