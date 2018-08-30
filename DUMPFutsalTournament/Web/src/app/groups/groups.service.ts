import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { GroupExtended } from './classes/group_extended';
import { MatchExtended } from './classes/match_extended';

@Injectable()
export class GroupsService {
	private groupsUrl = 'api/groups';
	private matchesUrl = 'api/matches/brackets'
	constructor(private http: HttpClient) { }

	getAllGroupsWithStandings(): Observable<GroupExtended[]> { return this.http.get<GroupExtended[]>(this.groupsUrl + '/standings'); }

	getAllBracketMatches(): Observable<MatchExtended[]> { return this.http.get<MatchExtended[]>(this.matchesUrl); }
}