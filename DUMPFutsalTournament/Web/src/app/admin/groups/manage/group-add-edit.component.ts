import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AdminService } from '../../admin.service';
import { Group } from '../../../infrastructure/classes/group';
import { Team } from '../../../infrastructure/classes/team';
import { ClosePopupService } from '../../../common/close-popup.service';

@Component({
	templateUrl: './group-add-edit.component.html'
})
export class GroupAddEditComponent implements OnInit {
	groupId: number;
	isEdit: boolean = false;
	group: Group;
	teams: Team[];
	hasLoaded: boolean = false;

	constructor(private route: ActivatedRoute, private service: AdminService,
		private closePopup: ClosePopupService) { }

	ngOnInit() {
		this.route.queryParams.subscribe(params => this.groupId = params['id']);
		if (this.groupId) {
			this.service.getGroup(this.groupId)
				 .subscribe(groupData => {
					this.group = groupData;
					this.hasLoaded = true;
				});
			this.isEdit = true;
		}
		else {
			this.group = new Group();
			this.group.teams = [];
			this.group.size = 4;
			this.hasLoaded = true;
		}
		this.service.getAllGrouplessTeams().subscribe(teamData => this.teams = teamData);
	}

	close() {
		this.closePopup.close(this.route.parent);
	}

	removeTeam(id: number) {
		const removeIndex = this.group.teams.findIndex(team => team.teamId === id);
		this.teams.push(this.group.teams[removeIndex]);
		this.group.teams.splice(removeIndex, 1);
	}

	addTeam(id: number) {
		 if (this.group.teams.length < this.group.size)
		 {
			  const removeIndex = this.teams.findIndex(team => team.teamId === id);
			  this.group.teams.push(this.teams[removeIndex]);
			  this.teams.splice(removeIndex, 1);
		 }
		else
			alert('Grupa već ima maksimalan broj timova!');
	}

	save() {
		if (this.isEdit)
			this.service.editGroup(this.group).subscribe(() => this.close());
		else
			this.service.addGroup(this.group).subscribe(() => this.close());
	}
}