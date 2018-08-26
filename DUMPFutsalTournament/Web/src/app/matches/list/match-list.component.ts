import { Component, OnInit } from '@angular/core';
import { MatchService } from '../match.service';
import { Match } from '../../infrastructure/classes/match';

@Component({
	templateUrl: './match-list.component.html'
})
export class MatchListComponent implements OnInit {
	matches: Match[];

	constructor(private service: MatchService) { }

	ngOnInit() {
		 this.service.getAllMatches()
			 .subscribe(allMatches => this.matches = allMatches);
	}
}