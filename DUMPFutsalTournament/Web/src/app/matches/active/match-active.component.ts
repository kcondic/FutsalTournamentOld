import { Component, OnInit } from '@angular/core';
import { MatchService } from '../match.service';
import { Match } from '../../infrastructure/classes/match';
import { SocketService } from '../../common/socket.service';

@Component({
	templateUrl: './match-active.component.html'
})
export class MatchActiveComponent implements OnInit {
	match: Match;
	minute: number = 0;
	second: number = 0;
	hasLoaded: boolean = false;

	constructor(private service: MatchService, private socketService: SocketService) { }

	 ngOnInit() {
		  this.service.getActiveMatch().subscribe(activeMatch => {
			   this.match = activeMatch;
			   this.hasLoaded = true;  
			   this.initIoConnection();
		  });
	 }

	 private initIoConnection(): void {
		  this.socketService.onMatch()
			.subscribe((match: Match) => {
				  this.match = match;
			});

		  this.socketService.onMinute()
			.subscribe((minute: number) => {
				  this.minute = minute;
			});

		  this.socketService.onSecond()
			.subscribe((second: number) => {
				  this.second = second;
			});
	 }
}