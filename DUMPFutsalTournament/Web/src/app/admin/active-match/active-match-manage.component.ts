import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from '../admin.service';
import { Match } from '../../infrastructure/classes/match';
import { Player } from '../../infrastructure/classes/player';
import { MatchEvent } from '../../infrastructure/classes/matchevent';
import { MatchType } from '../../infrastructure/enums/matchtype';
import { MatchEventType } from '../../infrastructure/enums/matcheventtype';
import { MatchTypeTranslationService } from '../../common/match-type-translation.service';
import { AdminLiveMatchService} from '../admin.live.match.service';
import { HostListener } from '@angular/core';
import { interval, Subscription } from 'rxjs';

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
	hasLoaded: boolean = false;
	timeSubscription: Subscription;

	constructor(private router: Router, private service: AdminService,
		 private matchTypeTranslation: MatchTypeTranslationService, private adminLiveMatchService: AdminLiveMatchService) { }

	ngOnInit() {
		this.service.getActiveMatch()
			 .subscribe(matchData => {
				  if (matchData !== null)
				  {
					  this.activeMatch = matchData;
					  this.activeMatch.homeTeam.players = this.activeMatch.homeTeam.players.sort((player1, player2) => player1.lastName < player2.lastName ? -1 : 1);
					  this.activeMatch.awayTeam.players = this.activeMatch.awayTeam.players.sort((player1, player2) => player1.lastName < player2.lastName ? -1 : 1);
				  }
				this.hasLoaded = true;
			});
		this.timeSubscription = this.source.subscribe(() => {
			if (!this.stopWatchStopped)
				this.addSecond();
		});
		this.adminLiveMatchService.getCurrentActiveTime()
			 .subscribe(time => {
				this.minutes = time;
			});

		this.matchEventTypeKeys = Object.keys(this.matchEventTypes).filter(f => !isNaN(Number(f)));
	 }

	 ngOnDestroy() {
		 this.timeSubscription.unsubscribe();
	 }


	@HostListener('document:keypress', ['$event'])
	handleKeyboardEvent(event: KeyboardEvent) {
		 if (event.key === ' ') {
		     event.preventDefault();
			this.toggleStopWatch();
		 }
	}

	addSecond() {
		if(this.minutes < 30) {
			this.seconds++;
			if (this.seconds >= 60) {
				this.seconds = 0;
				this.minutes++;
				this.adminLiveMatchService
				    .updateMatchMinute()
				    .subscribe();
			}
		} else {
			this.stopWatchStopped = true;
		}
	}

	setTime() {
		this.stopWatchStopped = true;
		this.adminLiveMatchService.setTime(this.minutes).subscribe();
	}

	toggleStopWatch() {
		this.stopWatchStopped = !this.stopWatchStopped;
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
		if (isForHomeTeam && +this.newEventType === MatchEventType.OwnGoal)
			 isForHomeTeam = false;
		else if (!isForHomeTeam && +this.newEventType === MatchEventType.OwnGoal)
			   isForHomeTeam = true;
		const newMatchEvent = new MatchEvent(Object.assign({}, this.activeMatch), player,
							this.newEventType, isForHomeTeam, this.minutes + 1);
		newMatchEvent.match.homeTeam = null;
		newMatchEvent.match.awayTeam = null;
		newMatchEvent.match.matchEvents = null;
		 this.service.addMatchEvent(newMatchEvent).subscribe(() => {
			this.activeMatch.matchEvents.push(newMatchEvent);
			  if (+newMatchEvent.eventType === MatchEventType.Goal ||
				 +newMatchEvent.eventType === MatchEventType.PenaltyGoal ||
				 +newMatchEvent.eventType === MatchEventType.OwnGoal)
			  {
				if (isForHomeTeam)
					this.activeMatch.homeGoals++;
				else
					  this.activeMatch.awayGoals++;
				this.service.updateMatchGoals(this.activeMatch).subscribe();
			  }  
		 });
		this.stopWatchStopped = false;
		this.isAddEvent = false;
	}

	 removeMatchEvent(id: number) {
		this.stopWatchStopped = true;
		if(confirm('Želite li zaista izbrisati taj događaj?'))
			this.service.deleteMatchEvent(id).subscribe(() => {
				let removeIndex = this.activeMatch.matchEvents.findIndex(matchEvent => matchEvent.matchEventId === id);
				let eventType = this.activeMatch.matchEvents[removeIndex].eventType;
				let isForHomeTeam = this.activeMatch.matchEvents[removeIndex].isForHomeTeam;

				if (removeIndex !== -1)
					  this.activeMatch.matchEvents.splice(removeIndex, 1);
				 if (+eventType === MatchEventType.Goal ||
					+eventType === MatchEventType.PenaltyGoal ||
					+eventType === MatchEventType.OwnGoal) {
					if (isForHomeTeam)
						this.activeMatch.homeGoals--;
					else
						this.activeMatch.awayGoals--;
					this.service.updateMatchGoals(this.activeMatch).subscribe();
				}
				this.stopWatchStopped = false;
			});
		 else
			this.stopWatchStopped = false;
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