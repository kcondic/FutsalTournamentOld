import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Match } from '../infrastructure/classes/match';

@Injectable()
export class MatchService {
	private matchesUrl = 'http://dump-turnir.com/api/matches';
	constructor(private http: HttpClient) { }

	getAllMatches(): Observable<Match[]> { return this.http.get<Match[]>(this.matchesUrl); }

	getActiveMatch(): Observable<Match> { return this.http.get<Match>(`${this.matchesUrl}/active`); }

	getMatch(id: number): Observable<Match> { return this.http.get<Match>(`${this.matchesUrl}/${id}`); }
}