import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeamDetailComponent } from './detail/team-detail.component';
import { TeamListComponent } from './list/team-list.component';
import { TeamService } from './team.service';
import { TeamRoutingModule } from './teams-routing.module';
import { MatchesModule } from '../matches/matches.module';

@NgModule({
	imports: [
		CommonModule,
		TeamRoutingModule,
		MatchesModule
	],
	 declarations: [
		TeamDetailComponent,
		TeamListComponent
	],
	providers: [ TeamService ]
})
export class TeamsModule { }