import { Match } from './match'
import { Player } from './player'
import { Group } from './group'

export class Team {
	teamId: number;
	name: string;
	group: Group;
	homeMatches: Match[];
	awayMatches: Match[];
	players: Player[];
}