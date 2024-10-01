import { BaseModel } from '@shared/entities/models/base.model';
import { List } from '@lib/decorators/list.decorator';
import { Filter } from '@lib/decorators/filter.decorator';

export class Region extends BaseModel {
	regionId: number;

	@List('Region','Region name')
	@Filter()
	regionName: string

	countryId: number
}