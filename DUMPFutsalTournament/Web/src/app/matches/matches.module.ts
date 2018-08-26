import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatchActiveComponent } from './active/match-active.component';
import { MatchDetailComponent } from './detail/match-detail.component';
import { MatchListComponent } from './list/match-list.component';
import { MatchService } from './match.service';
import { MatchRoutingModule } from './matches-routing.module';

@NgModule({
  imports: [
    CommonModule,
    MatchRoutingModule
  ],
  declarations: [
	MatchListComponent,
     MatchDetailComponent,
     MatchActiveComponent
  ],
  providers: [ MatchService ],
  exports: [ MatchDetailComponent ]
})
export class MatchesModule { }