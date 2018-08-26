import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AdminService } from '../../admin.service';
import { Player } from '../../../infrastructure/classes/player';
import { Team } from '../../../infrastructure/classes/team';
import { ClosePopupService } from '../../../common/close-popup.service';

@Component({
	templateUrl: './player-add-edit.component.html'
})
export class PlayerAddEditComponent implements OnInit {
	playerId: number;
	isEdit: boolean = false;
	player: Player;
	teams: Team[];

	 constructor(private route: ActivatedRoute, private service: AdminService,
		 private closePopup: ClosePopupService) { }

	 ngOnInit() {
		this.playerId = this.route.queryParams['id'];
		  if (this.playerId) {
			  this.service.getPlayer(this.playerId)
				.subscribe(playerData => this.player = playerData);
			  this.isEdit = true;
		  }
		  else
			   this.player = new Player();
		 this.service.getAllTeams().subscribe(teamData => this.teams = teamData);
	 }

	 close() {
		 this.closePopup.close(this.route.parent);
	 }

	 save() {
		 if (this.isEdit)
			 this.service.editPlayer(this.player);
		 else
			 this.service.addPlayer(this.player);
		 this.close();
	 }
}