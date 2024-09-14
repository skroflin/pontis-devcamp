import { BaseModel } from '@shared/entities/models/base.model';
import { List } from '@lib/decorators/list.decorator';


export class District extends BaseModel {
	districtId: number;

	@List('District','District name')
	districtName: string

	@List('District','District type')
	districtType: string

	regionId: number
}