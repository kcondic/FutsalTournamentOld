import { Team } from './team'
import { MatchEvent } from './matchevent'

export class Player {
	playerId: number;
	firstName: string;
	lastName: string;
	dateOfBirth: Date;
	team: Team;
	matchEvents: MatchEvent[];
}