import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { GroupWithStandings } from '../infrastructure/classes/groupwithstandings';
import { Match } from '../infrastructure/classes/match';

@Injectable()
export class GroupsService {
	private groupsUrl = 'api/groups';
	private matchesUrl = 'api/matches';
	constructor(private http: HttpClient) { }

	getAllGroupsWithStandings(): Observable<GroupWithStandings[]> { return this.http.get<GroupWithStandings[]>(`${this.groupsUrl}/standings`); }

	getEliminationMatches(): Observable<Match[]> { return this.http.get<Match[]>(`${this.matchesUrl}/elimination`); }
}