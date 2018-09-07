import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { GroupWithStandings } from '../infrastructure/classes/groupwithstandings';
import { Match } from '../infrastructure/classes/match';
import { TopScorer } from '../infrastructure/classes/topscorer';

@Injectable()
export class GroupsService {
	private groupsUrl = 'api/groups';
	private matchesUrl = 'api/matches';
	private playersUrl = 'api/players';
	constructor(private http: HttpClient) { }

	getAllGroupsWithStandings(): Observable<GroupWithStandings[]> { return this.http.get<GroupWithStandings[]>(`${this.groupsUrl}/standings`); }

	getTopScorers(): Observable<TopScorer[]> { return this.http.get<TopScorer[]>(`${this.playersUrl}/top-scorer`) }
}