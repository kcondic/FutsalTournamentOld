import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AdminService } from '../../admin.service';
import { Match } from '../../../infrastructure/classes/match';
import { Team } from '../../../infrastructure/classes/team';
import { MatchType } from '../../../infrastructure/enums/matchtype';
import { ClosePopupService } from '../../../common/close-popup.service';
import { MatchTypeTranslationService } from '../../../common/match-type-translation.service';

@Component({
	templateUrl: './match-add-edit.component.html'
})
export class MatchAddEditComponent implements OnInit {
	matchId: number;
	isEdit: boolean = false;
     match: Match;
	teams: Team[];
	homeTeams: Team[];
	awayTeams: Team[];
	matchTypes = MatchType;
	matchTypeKeys: string[];
	hour: number;
	minute: number;
	hasLoaded: boolean = false;

	constructor(private route: ActivatedRoute, private service: AdminService, private closePopup: ClosePopupService,
				private matchTypeTranslation: MatchTypeTranslationService) { }

	 ngOnInit() {
		this.route.queryParams.subscribe(params => this.matchId = params['id']);
		if (this.matchId) {
			this.service.getMatch(this.matchId)
				 .subscribe(matchData => {
					this.match = matchData;
					this.hour = new Date(this.match.timeOfMatch).getHours();
					this.minute = new Date(this.match.timeOfMatch).getMinutes();
					this.getAndInitializeTeams();
					this.hasLoaded = true;
				});
			this.isEdit = true;
		}
		else {
			this.match = new Match();
			this.hour = 20;
			this.minute = 0;
			this.getAndInitializeTeams();
			this.hasLoaded = true;
		}
		this.matchTypeKeys = Object.keys(this.matchTypes).filter(f => !isNaN(Number(f)));
	}

	getMatchTypeTranslation(matchTypeEnumValue: MatchType): string {
		return this.matchTypeTranslation.getMatchTypeTranslation(matchTypeEnumValue);
	}

	getAndInitializeTeams() {
		this.service.getAllTeams().subscribe(teamData => {
			this.teams = teamData;
			this.match.homeTeam = this.teams[0];
			this.match.awayTeam = this.teams[1];
			this.updateHomeTeams();
			this.updateAwayTeams();
		});
	}

	updateHomeTeams() {
		this.homeTeams = this.teams.filter(team => team.teamId !== this.match.awayTeam.teamId);
	}

	updateAwayTeams() {
		this.awayTeams = this.teams.filter(team => team.teamId !== this.match.homeTeam.teamId);
	}

	close() {
		this.closePopup.close(this.route.parent);
	}

	save() {
		const dateOfMatch = new Date(this.match.timeOfMatch);
		this.match.timeOfMatch = new Date(dateOfMatch.getFullYear(), dateOfMatch.getMonth(), 
									dateOfMatch.getDate(), this.hour, this.minute);
		if (this.isEdit)
			this.service.editMatch(this.match).subscribe(() => this.close());
		else
			this.service.addMatch(this.match).subscribe(() => this.close());
	}
}