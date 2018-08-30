import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupRoutingModule } from './groups-routing.module';
import { GroupsComponent } from './groups.component';
import { GroupsService } from './groups.service'


@NgModule({
	imports: [
		CommonModule,
		GroupRoutingModule
	],
	declarations: [
		GroupsComponent
	],
	providers: [GroupsService]
})
export class GroupsModule { }