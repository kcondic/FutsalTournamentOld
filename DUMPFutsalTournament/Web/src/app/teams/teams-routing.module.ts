import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TeamDetailComponent } from './detail/team-detail.component';
import { TeamListComponent } from './list/team-list.component';

const playersRoutes: Routes = [
	{ path: ':id', component: TeamDetailComponent, outlet: 'popup' },
	{ path: '', component: TeamListComponent }
];

@NgModule({
	imports: [
		RouterModule.forChild(playersRoutes)
	],
	exports: [
		RouterModule
	]
})
export class TeamRoutingModule { }