import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { User } from '../infrastructure/classes/user';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class AuthService {
	constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

	 login(user: User): Observable<string> {
		  return this.http.post('http://dump-turnir.com/api/login', user, { responseType: 'text' });
	 }

	 isAuthenticated(): boolean {
		const token = localStorage.getItem('token');
		return token !== null && !this.jwtHelper.isTokenExpired(token);
	 }
}