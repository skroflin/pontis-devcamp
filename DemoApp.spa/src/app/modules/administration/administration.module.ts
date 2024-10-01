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
import { AdministrationRoutingModule } from './administration.routing';
import { LibModule } from '@lib/lib.module';

/** COMPONENTS */
import { ApplicationComponent } from './components/application/application.component';
import { ApplicationListComponent } from './components/application/application-list/application-list.component';
import { ApplicationFormComponent } from './components/application/application-form/application-form.component';

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

    AdministrationRoutingModule,
    LibModule,
  ],
  declarations: [
    ApplicationComponent,
    ApplicationListComponent,
    ApplicationFormComponent,
  ],
  exports: [
    ApplicationComponent,
    ApplicationListComponent,
    ApplicationFormComponent,
  ],
})
export class AdministrationModule {}
