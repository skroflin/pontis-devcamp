import { BaseModel } from '@shared/entities/models/base.model';
import { List } from '@lib/decorators/list.decorator';


export class Gender extends BaseModel {
	genderId: number;

	@List('Gender','Gender short')
	genderShort: string

	@List('Gender','Gender name')
	genderName: string
}