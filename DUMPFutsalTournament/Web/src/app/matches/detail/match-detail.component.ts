import { switchMap } from 'rxjs/operators'
import { Observable } from 'rxjs';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { MatchService } from '../match.service';
import { Match } from '../../infrastructure/classes/match';
import { MatchEvent } from '../../infrastructure/classes/matchevent';
import { MatchEventType } from '../../infrastructure/enums/matcheventtype';
import { MatchTypeTranslation } from '../../infrastructure/translations/matchtypetranslation';

@Component({
	selector: 'match-detail',
	templateUrl: './match-detail.component.html'
})
export class MatchDetailComponent implements OnInit {
	@Input() match: Match;
	match$: Observable<Match>;
	matchTypeTranslator: MatchTypeTranslation;

	constructor(private route: ActivatedRoute, private service: MatchService)
	{
		this.matchTypeTranslator = new MatchTypeTranslation();
	}

	ngOnInit() {
		if(!this.match)
			this.match$ = this.route.paramMap.pipe(
				 switchMap((params: ParamMap) => {
					return this.service.getMatch(+params.get('id'));
				})			
		     );
	}

	isEventForHomeOrAway(event: MatchEvent, homeTeamId: number): string {
		if (event.player.team.teamId === homeTeamId)
			return 'home-event';
		return 'away-event';
	}

	getEventTypeClass(matchEventType: number): string {
		return MatchEventType[matchEventType];
	}

	getMatchTypeTranslation(matchTypeEnumValue): string {
		return this.matchTypeTranslator.GetMatchTypeTranslation(matchTypeEnumValue);
	}
}