import { Component, OnInit } from '@angular/core';
import { GroupsService } from './groups.service'
import { GroupExtended } from '../infrastructure/classes/group_extended';
import { TeamPosition } from '../infrastructure/classes/team_position';
import { GroupedObservable } from 'rxjs';
import { MatchExtended } from '../infrastructure/classes/match_extended';
import { MatchType } from '../infrastructure/enums/matchtype';
import { MatchStage } from '../infrastructure/classes/match_stage';

@Component({
	templateUrl: './groups.component.html'
})
export class GroupsComponent implements OnInit {
	groups: GroupExtended[];
	currentGroup: GroupExtended;
	activeGroupId: number;

	matchStages: MatchStage[];

	constructor(private service: GroupsService) {  }

	ngOnInit() {
		 this.service.getAllGroupsWithStandings()
			 .subscribe(allGroups => {
				 this.groups = allGroups;
				 this.takeGroup(this.groups[0].groupId)
				});

		this.service.getAllBracketMatches()
				.subscribe(allMatches => {
					this.createMatchStage(allMatches);
				})
	}

	createMatchStage (allMatches: MatchExtended[]) : void {
		this.matchStages = [];
		this.matchStages.push(new MatchStage("Četvrtfinale", MatchType.QuarterFinal, allMatches.filter(m => m.matchType == MatchType.QuarterFinal).reverse()))
		this.matchStages.push(new MatchStage("Polufinale", MatchType.SemiFinal, allMatches.filter(m => m.matchType == MatchType.SemiFinal).reverse()))
		this.matchStages.push(new MatchStage("Za treće mjesto", MatchType.ThirdPlace, allMatches.filter(m => m.matchType == MatchType.ThirdPlace).reverse()))
		this.matchStages.push(new MatchStage("Finale", MatchType.Final, allMatches.filter(m => m.matchType == MatchType.Final).reverse()))
		console.log(this.matchStages);
	}

	takeGroup (id: number) : void {
		this.activeGroupId = id;
		this.currentGroup = this.groups.find(g => g.groupId === id);
	}
}
