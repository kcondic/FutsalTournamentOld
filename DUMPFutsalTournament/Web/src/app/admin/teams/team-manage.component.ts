import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AdminService } from '../admin.service';
import { Team } from '../../infrastructure/classes/team';

@Component({
	templateUrl: './team-manage.component.html'
})
export class TeamManageComponent implements OnInit {
	teams: Team[];

	constructor(private router: Router, private service: AdminService) {
		router.events.subscribe((val) => {
			if (val instanceof NavigationEnd)
				this.getTeams();
		});
	}

	ngOnInit() {
		this.getTeams();
	}

	getTeams() {
		this.service.getAllTeams()
			.subscribe(teamData => this.teams = teamData);
	}

	delete(id: number) {
		if (confirm("Jeste li sigurni da želite izbrisati tu ekipu?"))
			this.service.deleteTeam(id).subscribe(() => {
				const removeIndex = this.teams.findIndex(team => team.teamId === id);
				if (removeIndex !== -1)
					this.teams.splice(removeIndex, 1);
			});
	}
}