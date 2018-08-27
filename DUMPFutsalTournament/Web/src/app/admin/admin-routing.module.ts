import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PlayerManageComponent } from './players/player-manage.component';
import { PlayerAddEditComponent } from './players/manage/player-add-edit.component';
import { TeamManageComponent } from './teams/team-manage.component';
import { TeamAddEditComponent } from './teams/manage/team-add-edit.component';
import { GroupManageComponent } from './groups/group-manage.component';
import { GroupAddEditComponent } from './groups/manage/group-add-edit.component';

const adminRoutes: Routes = [
	 {
			path: 'players',
			children: [
			{
				path: 'manage',
				component: PlayerAddEditComponent,
				outlet: 'popup'
			},
			{
			   path: '',
			   component: PlayerManageComponent
			}]
	 },
	 {
			path: 'teams',
			children: [
			{
				path: 'manage',
				component: TeamAddEditComponent,
				outlet: 'popup'
			},
			{
				path: '',
				component: TeamManageComponent
			}]
	 },
	 {
			path: 'groups',
			children: [
			{
				path: 'manage',
				component: GroupAddEditComponent,
				outlet: 'popup'
			},
			{
				path: '',
				component: GroupManageComponent
			}]
	 },
	 { path: '', redirectTo: 'groups' }
];

@NgModule({
	imports: [
		RouterModule.forChild(adminRoutes)
	],
	exports: [
		RouterModule
	]
})
export class AdminRoutingModule { }