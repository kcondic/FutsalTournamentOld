import { Match } from './match';
import { Player } from './player';
import { MatchEventType } from '../enums/matcheventtype';

export class MatchEvent {
	constructor(match: Match, player: Player, eventType: MatchEventType, isForHomeTeam: boolean, eventMinute: number) {
		this.match = match;
		this.player = player;
		this.eventType = eventType;
		this.isForHomeTeam = isForHomeTeam;
		this.eventMinute = eventMinute;
	}

	matchEventId: number;
	eventMinute: number;
	match: Match;
	player: Player;
	eventType: MatchEventType;
	isForHomeTeam: boolean;
}