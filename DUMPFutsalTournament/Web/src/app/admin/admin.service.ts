import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Player } from '../infrastructure/classes/player';
import { Match } from '../infrastructure/classes/match';
import { Group } from '../infrastructure/classes/group';
import { Team } from '../infrastructure/classes/team';

@Injectable()
export class AdminService {
	private playersUrl = 'api/players';
	private matchesUrl = 'api/matches';
	private groupsUrl = 'api/groups';
	private teamsUrl = 'api/teams';

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
}