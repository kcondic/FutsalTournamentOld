import { switchMap } from 'rxjs/operators'
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { MatchService } from '../match.service';
import { Match } from '../../infrastructure/classes/match';
import { MatchEvent } from '../../infrastructure/classes/matchevent';

@Component({
	templateUrl: './match-detail.component.html'
})
export class MatchDetailComponent implements OnInit {
	match$: Observable<Match>;

	 constructor(
		 private route: ActivatedRoute,
			  private service: MatchService) { }

	ngOnInit() {
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
}