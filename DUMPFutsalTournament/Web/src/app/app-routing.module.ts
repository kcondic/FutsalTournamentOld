import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageNotFoundComponent } from './not-found.component';

const appRoutes: Routes = [
	{
		path: 'matches',
		loadChildren: './matches/matches.module#MatchesModule'
	},
	{
		path: 'groups',
		loadChildren: './groups/groups.module#GroupsModule'
	},
	{
		path: 'teams',
		loadChildren: './teams/teams.module#TeamsModule'
	},
	{
		path: 'players',
		loadChildren: './players/players.module#PlayersModule'
	},
	{
		path: 'admin',
		loadChildren: './admin/admin.module#AdminModule'
	},
	{ path: '', redirectTo: '/matches/active', pathMatch: 'full' },
	{ path: '**', component: PageNotFoundComponent }
];

@NgModule({
	imports: [
		RouterModule.forRoot(appRoutes)
	],
	exports: [
		RouterModule
	],
	providers: [
	]
})
export class AppRoutingModule { }