import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import {Routes, RouterModule } from '@angular/router';
import { DashBoardComponent } from './dash-board/dash-board.component';
import {  MaterialModule,  ShareModule} from '../shared';

const routes: Routes = [
  { path: '', component: DashBoardComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    DashBoardComponent,
  ],
  imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      MaterialModule,
      ShareModule,
      RouterModule.forChild(routes),
  ]
})
export class DashBoardModule {
  public static routes = routes;
}
