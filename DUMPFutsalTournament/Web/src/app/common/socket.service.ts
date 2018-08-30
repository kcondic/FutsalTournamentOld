import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Match } from '../infrastructure/classes/match';
import { Socket } from 'ng-socket-io';

@Injectable()
export class SocketService {
	constructor(private socket: Socket) { }

	 sendMatch(match: Match) {
		 console.log("poslana utakmica");
		this.socket.emit('match', match);
	}

	 sendMinute(minute: number) {
		 console.log("poslana minuta");
		this.socket.emit('minute', minute);
	}

	 sendSecond(second: number) {
		 console.log("poslana sekunda");
		 this.socket.emit('second', second);
	}

	onMatch(): Observable<Match> {
		 return new Observable<Match>(observer => {
			 console.log("primljena utakmica");
			this.socket.on('match', (data: Match) => observer.next(data));
		});
	}

	onMinute(): Observable<number> {
		 return new Observable<number>(observer => {
			 console.log("primljena minuta");
			this.socket.on('minute', (data: number) => observer.next(data));
		});
	}

	onSecond(): Observable<number> {
		 return new Observable<number>(observer => {
			 console.log("primljena sekunda");
			this.socket.on('second', (data: number) => observer.next(data));
		});
	}

	close() {
		this.socket.disconnect();
	}
}