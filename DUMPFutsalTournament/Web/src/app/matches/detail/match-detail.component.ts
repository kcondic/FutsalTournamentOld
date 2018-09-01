import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatchService } from '../match.service';
import { Match } from '../../infrastructure/classes/match';
import { MatchEvent } from '../../infrastructure/classes/matchevent';
import { MatchType } from '../../infrastructure/enums/matchtype';
import { MatchEventType } from '../../infrastructure/enums/matcheventtype';
import { ClosePopupService } from '../../common/close-popup.service';
import { MatchTypeTranslationService } from '../../common/match-type-translation.service';

@Component({
	selector: 'match-detail',
	templateUrl: './match-detail.component.html'
})
export class MatchDetailComponent implements OnInit {
	@Input() match: Match;
	isLocal: boolean = false;
	hasLoaded: boolean = false;

	constructor(private route: ActivatedRoute,
		       private service: MatchService, private closePopup: ClosePopupService,
			  private matchTypeTranslation: MatchTypeTranslationService) { }

	ngOnInit() {
		if (!this.match)
			this.service.getMatch(this.route.snapshot.params['id'])
				.subscribe(matchData => {
					this.match = matchData;
					this.isLocal = true;
					this.hasLoaded = true;
				});
		else
			this.hasLoaded = true;
	}

	getEventTypeClass(matchEventType: number): string {
		return MatchEventType[matchEventType];
	}

	getMatchTypeTranslation(matchTypeEnumValue: MatchType): string {
		return this.matchTypeTranslation.getMatchTypeTranslation(matchTypeEnumValue);
	}

	getPenaltiesString(): string {
		if (this.match.matchType === MatchType.Group || this.match.homeGoals == null || this.match.awayGoals == null || this.match.homeGoals !== this.match.awayGoals)
			 return '';
		const homePenaltiesCount = this.match.matchEvents
			  .filter(matchEvent => matchEvent.eventType === MatchEventType.ShootoutGoal && matchEvent.isForHomeTeam).length;
		const awayPenaltiesCount = this.match.matchEvents
			  .filter(matchEvent => matchEvent.eventType === MatchEventType.ShootoutGoal && !matchEvent.isForHomeTeam).length;
		return `${homePenaltiesCount}:${awayPenaltiesCount}`;
	}

	getMatchEventTypeClass(type: MatchEventType) {
		return MatchEventType[type];
	}

	getSortedMatchEvents(): MatchEvent[] {
		return this.match.matchEvents.sort((eventOne, eventTwo) =>
			eventOne.eventMinute > eventTwo.eventMinute ? 1 : -1);
	}

	close() {
		this.closePopup.close(this.route.parent);
	}
}