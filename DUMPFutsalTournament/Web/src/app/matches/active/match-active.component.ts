import { Component, OnInit } from '@angular/core';
import { MatchService } from '../match.service';
import { Match } from '../../infrastructure/classes/match';

@Component({
	templateUrl: './match-active.component.html'
})
export class MatchActiveComponent implements OnInit {
	match: Match;
	constructor(private service: MatchService) { }

	 ngOnInit() {
		  this.service.getActiveMatch().subscribe(activeMatch => this.match = activeMatch);
	 }
}