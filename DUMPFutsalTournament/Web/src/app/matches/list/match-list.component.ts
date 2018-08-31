import { Component, OnInit } from '@angular/core';
import { MatchService } from '../match.service';
import { MatchTypeTranslationService } from '../../common/match-type-translation.service';
import { Match } from '../../infrastructure/classes/match';
import { MatchType } from '../../infrastructure/enums/matchtype';

@Component({
	templateUrl: './match-list.component.html'
})
export class MatchListComponent implements OnInit {
	matches: Match[];
	hasLoaded: boolean = false;

	constructor(private service: MatchService, private matchTypeTranslation: MatchTypeTranslationService) { }

	ngOnInit() {
		 this.service.getAllMatches()
			  .subscribe(allMatches => {
				 this.matches = allMatches;
				 this.hasLoaded = true;
			 });
	}

	getMatchTypeTranslation(matchTypeEnumValue: MatchType): string {
		return this.matchTypeTranslation.getMatchTypeTranslation(matchTypeEnumValue);
	}

	getMatchDates() {
		const timesOfMatches = this.matches.map(match => this.getReadableDate(match.timeOfMatch));
		return timesOfMatches.filter((time, index) => timesOfMatches.indexOf(time) === index);
	}

	 getMatchesForDate(dateString: string) {
		return this.matches.filter(match => this.getReadableDate(match.timeOfMatch) === dateString);
	}

	 getReadableDate(dateData: Date) {
		const date = new Date(dateData);
		  return `${date.getDate()}.${date.getMonth()+1}.`;
	}
}