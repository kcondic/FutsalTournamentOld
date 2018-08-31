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
	hasLoaded: boolean = false;

	 constructor(private route: ActivatedRoute, private service: AdminService,
		 private closePopup: ClosePopupService) { }

	 ngOnInit() {
		this.route.queryParams.subscribe(params => this.playerId = params['id']);
		  if (this.playerId) {
			  this.service.getPlayer(this.playerId)
				   .subscribe(playerData => {
					  this.player = playerData;
					  this.hasLoaded = true;
				  });
			  this.isEdit = true;
		  }
		  else
		  {
			  this.player = new Player();
			  this.player.team = null;
			  this.hasLoaded = true;
		  }
		 this.service.getAllTeams().subscribe(teamData => this.teams = teamData);
	 }

	 close() {
		 this.closePopup.close(this.route.parent);
	 }

	 showError() {
		 alert('Igrač nije dodan. Moguće greške: prazno prezime, tim već ima maksimalan broj igrača, nije pronađen igrač za edit.');
	 }

	 save() {
		 if (this.isEdit)
		  this.service.editPlayer(this.player).subscribe(() => this.close(), () => this.showError());
		 else
		  this.service.addPlayer(this.player).subscribe(() => this.close(), () => this.showError());
	 }
}