import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PlayerManageComponent } from './players/player-manage.component';
import { PlayerAddEditComponent } from './players/manage/player-add-edit.component';

const adminRoutes: Routes = [
	 {
		  path: 'players',
		  component: PlayerManageComponent,
		  children: [{ path: 'manage', component: PlayerAddEditComponent, outlet: 'popup' }],
	 },
	 { path: '', redirectTo: 'players' }
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