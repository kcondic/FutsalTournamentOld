import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AdminService } from '../admin.service';
import { Group } from '../../infrastructure/classes/group';

@Component({
	templateUrl: './group-manage.component.html'
})
export class GroupManageComponent implements OnInit {
	groups: Group[];

	constructor(private router: Router, private service: AdminService) {
		router.events.subscribe((val) => {
			if (val instanceof NavigationEnd)
				this.getGroups();
		});
	}

	ngOnInit() {
		this.getGroups();
	}

	getGroups() {
		this.service.getAllGroups()
			.subscribe(groupData => this.groups = groupData);
	}

	delete(id: number) {
		if (confirm("Jeste li sigurni da želite izbrisati tu grupu?"))
			this.service.deleteGroup(id).subscribe(() => {
				const removeIndex = this.groups.findIndex(group => group.groupId === id);
				if (removeIndex !== -1)
					this.groups.splice(removeIndex, 1);
			});
	}
}