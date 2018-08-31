import { Component, OnInit } from '@angular/core';
import { GroupsService } from '../groups.service';
import { GroupWithStandings } from '../../infrastructure/classes/groupwithstandings';
import { Match } from '../../infrastructure/classes/match';
import { MatchType } from '../../infrastructure/enums/matchtype';
import { MatchTypeTranslationService } from '../../common/match-type-translation.service';
import { TopScorer } from '../../infrastructure/classes/topscorer';

@Component({
	templateUrl: './groups.component.html'
})
export class GroupsComponent implements OnInit {
	groupsWithStandings: GroupWithStandings[];
	currentGroup: GroupWithStandings;
	eliminationMatches: Match[];
	matchTypes = MatchType;
	matchTypeKeys: string[];
	 hasLoaded: boolean = false;
	topScorers: TopScorer[];

	 constructor(private service: GroupsService, private matchTypeTranslation: MatchTypeTranslationService) {  }

	ngOnInit() {
		 this.service.getAllGroupsWithStandings()
			 .subscribe(allGroups => {
				 this.groupsWithStandings =
					 allGroups.sort((group1, group2) => group1.group.name < group2.group.name ? 1 : -1);
				 this.selectGroup(this.groupsWithStandings[0].group.groupId);
				 this.service.getEliminationMatches()
					 .subscribe(allMatches => {
						 this.eliminationMatches = allMatches;
						 this.hasLoaded = true;
					 });
			  });
		 this.service.getTopScorers().subscribe(topScorers => this.topScorers = topScorers);
		 this.matchTypeKeys = Object.keys(this.matchTypes).filter(f => !isNaN(Number(f)) && f !== MatchType[MatchType.Group] && f !== MatchType[MatchType.Revial]);
	}

	selectGroup (id: number) : void {
		this.currentGroup = this.groupsWithStandings.find(g => g.group.groupId === id);
	}

	getPhaseMatches(matchTypeEnumValue: MatchType): Match[] {
		return this.eliminationMatches.filter(match => match.matchType === matchTypeEnumValue);
	}

	getMatchTypeTranslation(matchTypeEnumValue: MatchType): string {
		return this.matchTypeTranslation.getMatchTypeTranslation(matchTypeEnumValue);
	}
}
