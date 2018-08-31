import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AdminService } from '../../admin.service';
import { Team } from '../../../infrastructure/classes/team';
import { Player } from '../../../infrastructure/classes/player';
import { ClosePopupService } from '../../../common/close-popup.service';

@Component({
	templateUrl: './team-add-edit.component.html'
})
export class TeamAddEditComponent implements OnInit {
	teamId: number;
	isEdit: boolean = false;
	team: Team;
	players: Player[];
	hasLoaded: boolean = false;

	constructor(private route: ActivatedRoute, private service: AdminService,
		private closePopup: ClosePopupService) { }

	ngOnInit() {
		this.route.queryParams.subscribe(params => this.teamId = params['id']);
		if (this.teamId) {
			this.service.getTeam(this.teamId)
				 .subscribe(teamData => {
					this.team = teamData;
					this.hasLoaded = true;
				});
			this.isEdit = true;
		}
		else {
			this.team = new Team();
			this.team.players = [];
			this.hasLoaded = true;
		}
		 this.service.getAllPlayers().subscribe(playerData => this.players = playerData.filter(player => player.team === null));
	}

	close() {
		this.closePopup.close(this.route.parent);
	}

	removePlayer(id: number) {
		const removeIndex = this.team.players.findIndex(player => player.playerId === id);
		this.players.push(this.team.players[removeIndex]);
		this.team.players.splice(removeIndex, 1);
	}

	addPlayer(id: number) {
		if (this.team.players.length < 12) {
			const removeIndex = this.players.findIndex(player => player.playerId === id);
			this.team.players.push(this.players[removeIndex]);
			this.players.splice(removeIndex, 1);
		}
		else
			alert('Ekipa već ima maksimalan broj igrača!');
	}

	save() {
		if (this.isEdit)
			this.service.editTeam(this.team).subscribe(() => this.close());
		else
			this.service.addTeam(this.team).subscribe(() => this.close());
	}
}