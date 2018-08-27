import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatchService } from '../match.service';
import { Match } from '../../infrastructure/classes/match';
import { MatchEvent } from '../../infrastructure/classes/matchevent';
import { MatchEventType } from '../../infrastructure/enums/matcheventtype';
import { MatchTypeTranslation } from '../../infrastructure/translations/matchtypetranslation';
import { ClosePopupService } from '../../common/close-popup.service';

@Component({
	selector: 'match-detail',
	templateUrl: './match-detail.component.html'
})
export class MatchDetailComponent implements OnInit {
	@Input() match: Match;
	localMatch: Match;
	matchTypeTranslator: MatchTypeTranslation;

	 constructor(private route: ActivatedRoute,
		 private service: MatchService, private closePopup: ClosePopupService)
	{
		this.matchTypeTranslator = new MatchTypeTranslation();
	}

	ngOnInit() {
		 if (!this.match)
			 this.service.getMatch(this.route.snapshot.params['id'])
				   .subscribe(matchData => this.localMatch = matchData);
	}

	isEventForHomeOrAway(event: MatchEvent, homeTeamId: number): string {
		if (event.player.team.teamId === homeTeamId)
			return 'home-event';
		return 'away-event';
	}

	getEventTypeClass(matchEventType: number): string {
		return MatchEventType[matchEventType];
	}

	getMatchTypeTranslation(matchTypeEnumValue: MatchEventType): string {
		return this.matchTypeTranslator.GetMatchTypeTranslation(matchTypeEnumValue);
	}

	 close() {
		this.closePopup.close(this.route.parent);
	}
}