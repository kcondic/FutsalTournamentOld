import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AdminLiveMatchService {
	private matchesUrl = 'api/live-match';

	constructor(private http: HttpClient) { }

	// Players
	 updateMatchSecond(): any { return this.http.get(this.matchesUrl + '/update-second'); }

	 getCurrentActiveTime() : any { return this.http.get(this.matchesUrl + '/get-time'); }
}