export class User {
	constructor(name: string, pass: string)
	{
		this.username = name;
		this.password = pass;
	}

	userId: number;
	username: string;
	password: string;
}