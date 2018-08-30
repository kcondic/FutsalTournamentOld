import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AdminService } from '../admin.service';
import { Match } from '../../infrastructure/classes/match';

@Component({
	templateUrl: './match-manage.component.html'
})
export class MatchManageComponent implements OnInit {
	matches: Match[];

	constructor(private router: Router, private service: AdminService) {
		router.events.subscribe((val) => {
			if (val instanceof NavigationEnd)
				this.getMatches();
		});
	}

	ngOnInit() {
		this.getMatches();
	}

	getMatches() {
		this.service.getAllMatches()
			.subscribe(matchData => this.matches = matchData);
	}

	 activate(id: number) {
		 if(confirm('Jeste li sigurni da želite aktivirati tu utakmicu? Utakmica može biti aktivirana samo ' +
					'jednom, a aktivacijom će se deaktivirati utakmica koja je već aktivna.'))
			  this.service.activateMatch(id).subscribe(() => {
				const activeMatchIndex = this.matches.findIndex(match => match.isActive);
				const matchToActivateIndex = this.matches.findIndex(match => match.matchId === id);
			  if(activeMatchIndex !== -1)
				this.matches[activeMatchIndex].isActive = false;
			     this.matches[matchToActivateIndex].isActive = true;
			 });
	}

	delete(id: number) {
		if (confirm("Jeste li sigurni da želite izbrisati tu utakmicu?"))
			this.service.deleteMatch(id).subscribe(() => {
				const removeIndex = this.matches.findIndex(match => match.matchId === id);
				if (removeIndex !== -1)
					this.matches.splice(removeIndex, 1);
			});
	}
}