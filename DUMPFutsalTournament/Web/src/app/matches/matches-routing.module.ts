import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatchActiveComponent } from './active/match-active.component';
import { MatchDetailComponent } from './detail/match-detail.component';
import { MatchListComponent } from './list/match-list.component';

const matchesRoutes: Routes = [
  { path: 'active', component: MatchActiveComponent },
  { path: ':id', component: MatchDetailComponent, outlet: 'popup'},
  { path: '', component: MatchListComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(matchesRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class MatchRoutingModule { }