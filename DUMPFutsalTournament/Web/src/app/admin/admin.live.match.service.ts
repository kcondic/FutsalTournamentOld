import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AdminLiveMatchService {
	private matchesUrl = 'http://dump-turnir.com/api/live-match';

	constructor(private http: HttpClient) { }

	// Players
	 updateMatchSecond(): any { return this.http.get(this.matchesUrl + '/update-second'); }

	 getCurrentActiveTime() : any { return this.http.get(this.matchesUrl + '/get-time'); }

	 setTime(minutes, seconds) : any { return this.http.get(`${this.matchesUrl}/set-time?minutes=${minutes}&seconds=${seconds}`)}
}