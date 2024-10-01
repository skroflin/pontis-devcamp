import { BaseModel } from '@shared/entities/models/base.model';
import { List } from '@lib/decorators/list.decorator';


export class Employee extends BaseModel {
	employeeId: number;

	@List('Employee','Username')
	username: string

	@List('Employee','Firstname')
	firstname: string

	@List('Employee','Lastname')
	lastname: string

	@List('Employee','Id number')
	nationalIdNumber: string

	nationalIdTypeId: number

	genderId: number

	@List('Employee','Birthdate')
	birthdate: string

	@List('Employee','Address')
	address: string

	placeId: number

	countryId: number
}