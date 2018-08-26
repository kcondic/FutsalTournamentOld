import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Player } from '../infrastructure/classes/player';
import { Match } from '../infrastructure/classes/match';
import { Group } from '../infrastructure/classes/group';
import { Team } from '../infrastructure/classes/team';
import { User } from '../infrastructure/classes/user';


@Injectable()
export class AdminService {
	private playersUrl = 'api/players';
	private matchesUrl = 'api/matches';
	private groupsUrl = 'api/groups';
	private teamsUrl = 'api/teams';
	private addUserUrl = 'api/add-user';

	constructor(private http: HttpClient) { }

	 getAllPlayers(): Observable<Player[]> { return this.http.get<Player[]>(this.playersUrl); }

	 getPlayer(id: number): Observable<Player> { return this.http.get<Player>(`${this.playersUrl}/${id}`); }

	 addPlayer(player: Player): Observable<{}> { return this.http.post(`${this.playersUrl}/add`, player); }

	 editPlayer(player: Player): Observable<{}> { return this.http.post(`${this.playersUrl}/edit`, player); }

	 deletePlayer(id: number): Observable<{}> { return this.http.delete(`${this.playersUrl}/delete/${id}`); }

	 getAllTeams(): Observable<Team[]> { return this.http.get<Team[]>(this.teamsUrl); }
}