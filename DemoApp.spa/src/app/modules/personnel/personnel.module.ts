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
import { PersonnelRoutingModule } from './personnel.routing';
import { LibModule } from '@lib/lib.module';

/** COMPONENTS */
import { EmployeeComponent } from './components/employee/employee.component';
import { EmployeeListComponent } from './components/employee/employee-list/employee-list.component';
import { EmployeeFormComponent } from './components/employee/employee-form/employee-form.component';
import { GenderComponent } from './components/gender/gender.component';
import { GenderListComponent } from './components/gender/gender-list/gender-list.component';
import { GenderFormComponent } from './components/gender/gender-form/gender-form.component';
import { NationalIdTypeComponent } from './components/national-id-type/national-id-type.component';
import { NationalIdTypeListComponent } from './components/national-id-type/national-id-type-list/national-id-type-list.component';
import { NationalIdTypeFormComponent } from './components/national-id-type/national-id-type-form/national-id-type-form.component';


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

    PersonnelRoutingModule,
    LibModule,
  ],
  declarations:
    [
      EmployeeComponent,
      EmployeeListComponent,
      EmployeeFormComponent,
      GenderComponent,
      GenderListComponent,
      GenderFormComponent,
      NationalIdTypeComponent,
      NationalIdTypeListComponent,
      NationalIdTypeFormComponent
    ],
  exports:
    [
      EmployeeComponent,
      EmployeeListComponent,
      EmployeeFormComponent,
      GenderComponent,
      GenderListComponent,
      GenderFormComponent,
      NationalIdTypeComponent,
      NationalIdTypeListComponent,
      NationalIdTypeFormComponent
    ],
})
export class PersonnelModule { }