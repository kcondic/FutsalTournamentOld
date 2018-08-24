import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { User } from './infrastructure/classes/user';

@Injectable()
export class AuthService {
	constructor(private http: HttpClient) { }

	 login(user: User): Observable<string> {
		  return this.http.post('api/login', user, { responseType: 'text' });
	}
}