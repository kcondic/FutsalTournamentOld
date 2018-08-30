import { MatchExtended } from './match_extended';
import { MatchType } from '../enums/matchtype';

export class MatchStage {
	constructor(title: string, matchType: MatchType, matches: MatchExtended[]){
		this.title = title;
		this.matchType = matchType;
		this.matches = matches;
	}

	matchType: MatchType;
	title: string;
	matches: MatchExtended[]
}