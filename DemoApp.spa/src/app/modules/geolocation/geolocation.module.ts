import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';

import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';

/*CUSTOM MODULES*/
import { GeolocationRoutingModule } from './geolocation.routing';
import { LibModule } from '@lib/lib.module';

/** COMPONENTS */
import {CountryComponent} from './components/country/country.component';
import {CountryListComponent} from './components/country/country-list/country-list.component';
import {CountryFormComponent} from './components/country/country-form/country-form.component';
import {DistrictComponent} from './components/district/district.component';
import {DistrictListComponent} from './components/district/district-list/district-list.component';
import {DistrictFormComponent} from './components/district/district-form/district-form.component';
import {PlaceComponent} from './components/place/place.component';
import {PlaceListComponent} from './components/place/place-list/place-list.component';
import {PlaceFormComponent} from './components/place/place-form/place-form.component';
import {RegionComponent} from './components/region/region.component';
import {RegionListComponent} from './components/region/region-list/region-list.component';
import {RegionFormComponent} from './components/region/region-form/region-form.component';


@NgModule({
  imports: [
    CommonModule,
    RouterModule,

    FormsModule,
    ReactiveFormsModule,

    MatInputModule,
    MatSelectModule,
    MatOptionModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatCheckboxModule,
    MatIconModule,
    MatButtonModule,
    MatToolbarModule,

   GeolocationRoutingModule,
    LibModule,
  ],
  declarations: 
  [ 
  CountryComponent, 
 CountryListComponent, 
 CountryFormComponent, 
 DistrictComponent, 
 DistrictListComponent, 
 DistrictFormComponent, 
 PlaceComponent, 
 PlaceListComponent, 
 PlaceFormComponent, 
 RegionComponent, 
 RegionListComponent, 
 RegionFormComponent 
  ],
  exports: 
  [ 
  CountryComponent, 
 CountryListComponent, 
 CountryFormComponent, 
 DistrictComponent, 
 DistrictListComponent, 
 DistrictFormComponent, 
 PlaceComponent, 
 PlaceListComponent, 
 PlaceFormComponent, 
 RegionComponent, 
 RegionListComponent, 
 RegionFormComponent 
  ],
})
export class GeolocationModule { }