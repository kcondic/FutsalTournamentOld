import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AdminService } from '../admin.service';
import { Player } from '../../infrastructure/classes/player';

@Component({
	templateUrl: './player-manage.component.html'
})
export class PlayerManageComponent implements OnInit {
	players: Player[];

	constructor(private router: Router, private service: AdminService) {
		router.events.subscribe((val) => {
			if (val instanceof NavigationEnd)
				this.getPlayers();
		});
	}

	 ngOnInit() {
		this.getPlayers();
	 }

	 getPlayers() {
		 console.log("pozvalo");
		 this.service.getAllPlayers()
			 .subscribe(playerData => this.players = playerData);
	 }

	 delete(id: number) {
		 if (confirm("Jeste li sigurni da želite izbrisati tog igrača?"))
			 this.service.deletePlayer(id).subscribe(() => {
				  const removeIndex = this.players.findIndex(player => player.playerId === id);
			       if(removeIndex !== -1)
					this.players.splice(removeIndex, 1);
			 });
	 }
}