import { Component, OnInit } from '@angular/core';
import { TeamService } from '../team.service';
import { Team } from '../../infrastructure/classes/team';

@Component({
	templateUrl: './team-list.component.html'
})
export class TeamListComponent implements OnInit {
	teams: Team[];
	hasLoaded: boolean = false;

	constructor(private service: TeamService) { }

	ngOnInit() {
		 this.service.getAllTeams()
			  .subscribe(allTeams => {
				   this.teams = allTeams
					   .sort((team1, team2) => team1.name < team2.name ? -1 : 1);
				 this.hasLoaded = true;
			 });
	}
}