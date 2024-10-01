import { BaseModel } from '@shared/entities/models/base.model';
import { List } from '@lib/decorators/list.decorator';
import { Filter } from '@lib/decorators/filter.decorator';


export class Country extends BaseModel {
	countryId: number;

	@List('Country','Country code')
	countryCode: string

	@List('Country','Country name')
	@Filter()
	countryName: string
}