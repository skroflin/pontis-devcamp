import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationLayoutComponent } from './layouts/authentication/authentication-layout.component';

/** COMPONENTS */
import { MainLayoutComponent } from './layouts/main/main-layout.component';
import { AuthenticationGuard } from './shared/guards/authentication.guard';

const AppRoutes: Routes = [
  { path: '', redirectTo: 'personnel', pathMatch: 'full' },
  {
    path: '',
    component: AuthenticationLayoutComponent,
    children: [
      {
        path: 'authentication',
        loadChildren: () =>
          import('./core/authentication/authentication.module').then(
            (module) => module.AuthenticationModule
          ),
      },
    ],
  },
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [AuthenticationGuard],
    children: [
      {
        path: 'personnel',
        loadChildren: () =>
          import('./modules/personnel/personnel.module').then(
            (module) => module.PersonnelModule
          ),
      },
    ],
  },
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [AuthenticationGuard],
    children: [
      {
        path: 'geolocation',
        loadChildren: () =>
          import('./modules/geolocation/geolocation.module').then(
            (module) => module.GeolocationModule
          ),
      },
    ],
  },
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [AuthenticationGuard],
    children: [
      {
        path: 'administration',
        loadChildren: () =>
          import('./modules/administration/administration.module').then(
            (module) => module.AdministrationModule
          ),
      },
    ],
  }
];

@NgModule({
  imports: [RouterModule.forRoot(AppRoutes, { useHash: false })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
