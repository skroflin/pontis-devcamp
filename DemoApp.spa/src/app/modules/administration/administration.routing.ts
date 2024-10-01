import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

/** COMPONENTS */
import { ApplicationComponent } from './components/application/application.component';

export const AdministrationRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'applications',
        component: ApplicationComponent,
      }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(AdministrationRoutes)],
  exports: [RouterModule],
})
export class AdministrationRoutingModule {}
