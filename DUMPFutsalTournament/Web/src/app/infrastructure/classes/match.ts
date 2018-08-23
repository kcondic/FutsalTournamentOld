import { Team } from './team';
import { MatchEvent } from './matchevent'
import { MatchType } from '../enums/matchtype'

export class Match {
	MatchId: number;
	TimeOfMatch: Date;
	HomeTeam: Team;
	AwayTeam: Team;
	HomeGoals: number;
	AwayGoals: number;
	MatchType: MatchType;
	MatchEvents: MatchEvent;
}