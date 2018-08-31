import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Team } from '../infrastructure/classes/team';
import { Match } from '../infrastructure/classes/match';

@Injectable()
export class TeamService {
	private teamsUrl = 'api/teams';
	private teamMatchesUrl = 'api/matches/team';

	constructor(private http: HttpClient) { }

     getAllTeams(): Observable<Team[]> { return this.http.get<Team[]>(this.teamsUrl); }

	getTeam(id: number): Observable<Team> { return this.http.get<Team>(`${this.teamsUrl}/${id}`); }

	getTeamMatches(id: number): Observable<Match[]> {
		 return this.http.get<Match[]>(`${this.teamMatchesUrl}/${id}`);
	} 
}