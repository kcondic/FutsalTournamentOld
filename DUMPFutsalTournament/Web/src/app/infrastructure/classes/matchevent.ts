import { Match } from './match'
import { Player } from './player'
import { MatchEventType } from '../enums/matcheventtype'

export class MatchEvent {
	matchEventId: number;
	eventMinute: number;
	match: Match;
	player: Player;
	eventType: MatchEventType;
}