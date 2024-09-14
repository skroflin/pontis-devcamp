import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

/** COMPONENTS */
import { EmployeeComponent } from './components/employee/employee.component';
import { GenderComponent } from './components/gender/gender.component';
import { NationalIdTypeComponent } from './components/national-id-type/national-id-type.component';


export const PersonnelRoutes: Routes = [
  {
    path:'',
    children: [
		{
			path: 'employees',
			component: EmployeeComponent
		},
		{
			path: 'genders',
			component: GenderComponent
		},
		{
			path: 'nationalidtypes',
			component: NationalIdTypeComponent
		},

    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(PersonnelRoutes)],
  exports: [RouterModule],
})
export class PersonnelRoutingModule { }