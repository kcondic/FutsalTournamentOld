import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Player } from '../infrastructure/classes/player';
import { Team } from '../infrastructure/classes/team';
import { Group } from '../infrastructure/classes/group';
import { Match } from '../infrastructure/classes/match';
import { MatchEvent } from '../infrastructure/classes/matchevent';

@Injectable()
export class AdminService {
	private playersUrl = 'api/players';
	private teamsUrl = 'api/teams';
	private groupsUrl = 'api/groups';
	private matchesUrl = 'api/matches';

	constructor(private http: HttpClient) { }

	// Players
	 getAllPlayers(): Observable<Player[]> { return this.http.get<Player[]>(this.playersUrl); }

	 getPlayer(id: number): Observable<Player> { return this.http.get<Player>(`${this.playersUrl}/${id}`); }

	 addPlayer(player: Player): Observable<{}> { return this.http.post(`${this.playersUrl}/add`, player); }

	 editPlayer(player: Player): Observable<{}> { return this.http.post(`${this.playersUrl}/edit`, player); }

	 deletePlayer(id: number): Observable<{}> { return this.http.delete(`${this.playersUrl}/delete/${id}`); }

	// Teams
	 getAllTeams(): Observable<Team[]> { return this.http.get<Team[]>(this.teamsUrl); }

	 getAllGrouplessTeams(): Observable<Team[]> { return this.http.get<Team[]>(`${this.teamsUrl}/groupless`); }

	 getTeam(id: number): Observable<Team> { return this.http.get<Team>(`${this.teamsUrl}/${id}`); }

	 addTeam(team: Team): Observable<{}> { return this.http.post(`${this.teamsUrl}/add`, team); }

	 editTeam(team: Team): Observable<{}> { return this.http.post(`${this.teamsUrl}/edit`, team); }

	 deleteTeam(id: number): Observable<{}> { return this.http.delete(`${this.teamsUrl}/delete/${id}`); }

	// Groups
	 getAllGroups(): Observable<Group[]> { return this.http.get<Group[]>(this.groupsUrl); }

	 getGroup(id: number): Observable<Group> { return this.http.get<Group>(`${this.groupsUrl}/${id}`); }

	 addGroup(group: Group): Observable<{}> { return this.http.post(`${this.groupsUrl}/add`, group); }

	 editGroup(group: Group): Observable<{}> { return this.http.post(`${this.groupsUrl}/edit`, group); }

	 deleteGroup(id: number): Observable<{}> { return this.http.delete(`${this.groupsUrl}/delete/${id}`); }

	// Matches
	 getAllMatches(): Observable<Match[]> { return this.http.get<Match[]>(this.matchesUrl); }

	 getMatch(id: number): Observable<Match> { return this.http.get<Match>(`${this.matchesUrl}/${id}`); }

	 activateMatch(id: number): Observable<{}> { return this.http.post(`${this.matchesUrl}/activate`, id); }

	 deactivateMatch(): Observable<{}> { return this.http.post(`${this.matchesUrl}/deactivate`, null);  }

	 addMatch(match: Match): Observable<{}> { return this.http.post(`${this.matchesUrl}/add`, match); }

	 editMatch(match: Match): Observable<{}> { return this.http.post(`${this.matchesUrl}/edit`, match); }

	 deleteMatch(id: number): Observable<{}> { return this.http.delete(`${this.matchesUrl}/delete/${id}`); }

	// MatchEvents
	 addMatchEvent(matchEvent: MatchEvent): Observable<{}> { return this.http.post(`${this.matchesUrl}/add-event`, matchEvent); }

	 deleteMatchEvent(id: number): Observable<{}> { return this.http.delete(`${this.matchesUrl}/delete/${id}`); }
}