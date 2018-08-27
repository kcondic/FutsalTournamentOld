import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PlayerManageComponent } from './players/player-manage.component';
import { PlayerAddEditComponent } from './players/manage/player-add-edit.component';
import { TeamManageComponent } from './teams/team-manage.component';
import { TeamAddEditComponent } from './teams/manage/team-add-edit.component';
import { GroupManageComponent } from './groups/group-manage.component';
import { GroupAddEditComponent } from './groups/manage/group-add-edit.component';
import { AdminService } from './admin.service';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminNavigationComponent } from './navigation/admin-navigation.component';

@NgModule({
	imports: [
		CommonModule,
		FormsModule,
		AdminRoutingModule
	],
	 declarations: [
		AdminNavigationComponent,
		PlayerManageComponent,
		PlayerAddEditComponent,
		TeamManageComponent,
		TeamAddEditComponent,
		GroupManageComponent,
		GroupAddEditComponent
	],
	providers: [ AdminService ]
})
export class AdminModule { }