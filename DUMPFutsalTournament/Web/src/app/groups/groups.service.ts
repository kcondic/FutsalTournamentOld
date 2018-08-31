import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { GroupWithStandings } from '../infrastructure/classes/groupwithstandings';
import { Match } from '../infrastructure/classes/match';
import { TopScorer } from '../infrastructure/classes/topscorer';

@Injectable()
export class GroupsService {
	private groupsUrl = 'http://dump-turnir.com/api/groups';
	private matchesUrl = 'http://dump-turnir.com/api/matches';
	private playersUrl = 'http://dump-turnir.com/api/players';
	constructor(private http: HttpClient) { }

	getAllGroupsWithStandings(): Observable<GroupWithStandings[]> { return this.http.get<GroupWithStandings[]>(`${this.groupsUrl}/standings`); }

	getEliminationMatches(): Observable<Match[]> { return this.http.get<Match[]>(`${this.matchesUrl}/elimination`); }

	getTopScorers(): Observable<TopScorer[]> { return this.http.get<TopScorer[]>(`${this.playersUrl}/top-scorer`) }
}