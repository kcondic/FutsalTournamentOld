import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GroupsComponent } from './groups.component';

const groupRoutes: Routes = [
  { path: '', component: GroupsComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(groupRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class GroupRoutingModule { }