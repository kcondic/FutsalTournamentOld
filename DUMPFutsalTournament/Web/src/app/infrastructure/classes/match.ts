import { Team } from './team';
import { MatchEvent } from './matchevent'
import { MatchType } from '../enums/matchtype'

export class Match {
	matchId: number;
	timeOfMatch: Date;
	homeTeam: Team;
	awayTeam: Team;
	homeGoals: number;
	awayGoals: number;
	matchType: MatchType;
	matchEvents: MatchEvent[];
	isActive: boolean;

	minute: number;
	second: number;
}