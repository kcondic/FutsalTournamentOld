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
		 this.service.activateMatch(id).subscribe(() => {
			this.matches[this.matches.findIndex(match => match.isActive === true)].isActive = false;
			this.matches[this.matches.findIndex(match => match.matchId === id)].isActive = true;
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