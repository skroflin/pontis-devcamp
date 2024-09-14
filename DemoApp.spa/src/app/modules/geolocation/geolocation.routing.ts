import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

/** COMPONENTS */
import { CountryComponent } from './components/country/country.component';
import { DistrictComponent } from './components/district/district.component';
import { PlaceComponent } from './components/place/place.component';
import { RegionComponent } from './components/region/region.component';


export const GeolocationRoutes: Routes = [
  {
    path:'',
    children: [
		{
			path: 'countries',
			component: CountryComponent
		},
		{
			path: 'districts',
			component: DistrictComponent
		},
		{
			path: 'places',
			component: PlaceComponent
		},
		{
			path: 'regions',
			component: RegionComponent
		},

    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(GeolocationRoutes)],
  exports: [RouterModule],
})
export class GeolocationRoutingModule { }