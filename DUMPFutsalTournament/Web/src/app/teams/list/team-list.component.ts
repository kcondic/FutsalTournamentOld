import { Component, OnInit } from '@angular/core';
import { TeamService } from '../team.service';
import { Team } from '../../infrastructure/classes/team';

@Component({
	templateUrl: './team-list.component.html'
})
export class TeamListComponent implements OnInit {
	teams: Team[];

	constructor(private service: TeamService) { }

	ngOnInit() {
		 this.service.getAllTeams()
			.subscribe(allTeams => this.teams = allTeams);
	}
}