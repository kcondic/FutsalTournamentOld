import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AdminService } from '../admin.service';
import { Player } from '../../infrastructure/classes/player';

@Component({
	templateUrl: './player-manage.component.html'
})
export class PlayerManageComponent implements OnInit {
	players: Player[];

	constructor(private route: ActivatedRoute, private service: AdminService) { }

	ngOnInit() {
		this.service.getAllPlayers()
			.subscribe(playerData => this.players = playerData);
	 }

	 delete(id: number) {
		 if (confirm("Jeste li sigurni da želite izbrisati tog igrača?"))
			 this.service.deletePlayer(id);
	 }
}