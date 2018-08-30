import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from '../admin.service';
import { Match } from '../../infrastructure/classes/match';
import { Player } from '../../infrastructure/classes/player';
import { MatchEvent } from '../../infrastructure/classes/matchevent';
import { MatchType } from '../../infrastructure/enums/matchtype';
import { MatchEventType } from '../../infrastructure/enums/matcheventtype';
import { MatchTypeTranslationService } from '../../common/match-type-translation.service';
import { SocketService } from '../../common/socket.service';
import { HostListener } from '@angular/core';
import { interval } from 'rxjs';

@Component({
	templateUrl: './active-match-manage.component.html'
})
export class ActiveMatchManageComponent implements OnInit {
	activeMatch: Match;
	minutes: number = 0;
	seconds: number = 0;
	stopWatchStopped: boolean = true;
	source = interval(1000);
	matchEventTypes = MatchEventType;
	matchEventTypeKeys: string[];
	isAddEvent: boolean = false;
	newEventType: MatchEventType;

	 constructor(private router: Router, private service: AdminService,
		 private matchTypeTranslation: MatchTypeTranslationService, private socketService: SocketService) { }

	ngOnInit() {
		this.service.getActiveMatch()
			.subscribe(matchData => this.activeMatch = matchData);
		this.source.subscribe(() => {
			if (!this.stopWatchStopped)
				this.addSecond();
		});
		this.matchEventTypeKeys = Object.keys(this.matchEventTypes).filter(f => !isNaN(Number(f)));
	 }


	@HostListener('document:keypress', ['$event'])
	handleKeyboardEvent(event: KeyboardEvent) {
		 if (event.key === ' ')
			  this.stopWatchStopped = !this.stopWatchStopped;
	}

	addSecond() {
		 if (this.seconds >= 59) {
			 this.socketService.sendSecond(this.seconds);
			 this.seconds = 0;
			 this.socketService.sendMinute(this.minutes);
			 this.minutes++;
		 }
		 else
		 {
			 this.socketService.sendSecond(this.seconds);
			 this.seconds++;
		 }
	}

	deactivate() {
		if (confirm("Jeste li sigurni da želite deaktivirati utakmicu? Više je nećete moći aktivirati."))
			this.service.deactivateMatch().subscribe(() => {
				this.router.navigateByUrl('/admin/matches');
			});
	}

	showAddMatchEventPopup(type: MatchEventType) {
		this.stopWatchStopped = true;
		this.isAddEvent = true;
		this.newEventType = type;
	}

	addMatchEvent(player: Player, isForHomeTeam: boolean) {
		const newMatchEvent = new MatchEvent(Object.assign({}, this.activeMatch), player,
							this.newEventType, isForHomeTeam, this.minutes + 1);
		newMatchEvent.match.homeTeam = null;
		newMatchEvent.match.awayTeam = null;
		newMatchEvent.match.matchEvents = null;
		 this.service.addMatchEvent(newMatchEvent).subscribe(() => {
			this.activeMatch.matchEvents.push(newMatchEvent);
			if (+newMatchEvent.eventType === MatchEventType.Goal || +newMatchEvent.eventType === MatchEventType.PenaltyGoal) {
				if (isForHomeTeam)
					this.activeMatch.homeGoals++;
				else
					  this.activeMatch.awayGoals++;
				this.service.updateMatchGoals(this.activeMatch).subscribe();
			}
			else if (+newMatchEvent.eventType === MatchEventType.OwnGoal) {
				if (isForHomeTeam)
					this.activeMatch.awayGoals++;
				else
					this.activeMatch.homeGoals++;
				this.service.updateMatchGoals(this.activeMatch).subscribe();
			}
		 });
	     this.socketService.sendMatch(this.activeMatch);
		this.stopWatchStopped = false;
		this.isAddEvent = false;
	}

	 removeMatchEvent(id: number) {
		this.stopWatchStopped = true;
		if(confirm('Želite li zaista izbrisati taj događaj?'))
			this.service.deleteMatchEvent(id).subscribe(() => {
				const removeIndex = this.activeMatch.matchEvents.findIndex(matchEvent => matchEvent.matchEventId === id);
				const eventType = this.activeMatch.matchEvents[removeIndex].eventType;
				const isForHomeTeam = this.activeMatch.matchEvents[removeIndex].isForHomeTeam;

				if (removeIndex !== -1)
					  this.activeMatch.matchEvents.splice(removeIndex, 1);
				if (+eventType === MatchEventType.Goal || +eventType === MatchEventType.PenaltyGoal) {
					if (isForHomeTeam)
						this.activeMatch.homeGoals--;
					else
						this.activeMatch.awayGoals--;
					this.service.updateMatchGoals(this.activeMatch).subscribe();
				}
				else if (+eventType === MatchEventType.OwnGoal) {
					if (isForHomeTeam)
						this.activeMatch.awayGoals--;
					else
						this.activeMatch.homeGoals--;
					this.service.updateMatchGoals(this.activeMatch).subscribe();
				}
				this.stopWatchStopped = false;
			});
		 else
			this.stopWatchStopped = false;
		 this.socketService.sendMatch(this.activeMatch);
	}

	cancelEventAdd() {
		this.isAddEvent = false;
		this.stopWatchStopped = false;
	}

	getMatchEventTypeClass(type: MatchEventType) {
		 return MatchEventType[type];
	}

	getMatchTypeTranslation(matchTypeEnumValue: MatchType): string {
		return this.matchTypeTranslation.getMatchTypeTranslation(matchTypeEnumValue);
	}
}