import { Routes } from '@angular/router';
import { NocontentComponent } from './shared'
import { HomeComponent } from './administration'
import { IsAuthenticated } from './shared';

export const ROUTES: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [IsAuthenticated],
    children: [
      {
        path: '',
        redirectTo: 'dashboard', pathMatch: 'full'
      },
      {
        path: 'dashboard',
        loadChildren: './dashBoard/dashBoard.module#DashBoardModule'
      }
    ]
  },
  {
    path: '**',
    component: NocontentComponent
  },

];
