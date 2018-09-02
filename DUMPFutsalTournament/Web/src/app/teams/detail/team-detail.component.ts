import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TeamService } from '../team.service';
import { Team } from '../../infrastructure/classes/team';
import { Match } from '../../infrastructure/classes/match';
import { MatchEvent } from '../../infrastructure/classes/matchevent';
import { MatchEventType } from '../../infrastructure/enums/matcheventtype';
import { ClosePopupService } from '../../common/close-popup.service';
import { MatchTypeTranslationService } from '../../common/match-type-translation.service';

@Component({
	templateUrl: './team-detail.component.html'
})
export class TeamDetailComponent implements OnInit {
	teamId: number;
	team: Team;
	teamMatches: Match[];
	hasLoaded: boolean = false;

	 constructor(private route: ActivatedRoute, private service: TeamService, private closePopup: ClosePopupService,
		 private matchTypeTranslation: MatchTypeTranslationService) { }

	ngOnInit() {
		this.teamId = this.route.snapshot.params['id'];
		this.service.getTeam(this.teamId)
			 .subscribe(teamData => {
				this.team = teamData;
				this.service.getTeamMatches(this.teamId)
					 .subscribe(matchData => {
						this.teamMatches = matchData.sort((match1, match2) => match1.timeOfMatch < match2.timeOfMatch ? -1 : 1);
						this.hasLoaded = true;
					});
			});
	}

	getPlayerAge(dateOfBirth: Date): any {
		if (!dateOfBirth)
			return 'nepoznato';
		const dateDifference = Date.now() - new Date(dateOfBirth).getTime();
		const dateDifferenceDate = new Date(dateDifference);
		return Math.abs(dateDifferenceDate.getUTCFullYear() - 1970);
	}

	getTotalMatchEvents(playerEvents: MatchEvent[]): any {
		return [
			{ number: playerEvents.filter(ev => ev.eventType === MatchEventType.Goal || ev.eventType === MatchEventType.PenaltyGoal).length, nameOfClass: MatchEventType[MatchEventType.Goal] },
			{ number: playerEvents.filter(ev => ev.eventType === MatchEventType.OwnGoal).length, nameOfClass: MatchEventType[MatchEventType.OwnGoal] },
			{ number: playerEvents.filter(ev => ev.eventType === MatchEventType.YellowCard).length, nameOfClass: MatchEventType[MatchEventType.YellowCard] },
			{ number: playerEvents.filter(ev => ev.eventType === MatchEventType.RedCard).length, nameOfClass: MatchEventType[MatchEventType.RedCard] }
		];
	}

	getLastMatchTypeTranslation(): string {
		const lastMatch = this.teamMatches.reduce((prev, curr) => (prev.timeOfMatch > curr.timeOfMatch) ? prev : curr);
		if(lastMatch)
			  return this.matchTypeTranslation.getMatchTypeTranslation(lastMatch.matchType);
		return '';
	}

	close() {
		this.closePopup.close(this.route.parent);
	}
}