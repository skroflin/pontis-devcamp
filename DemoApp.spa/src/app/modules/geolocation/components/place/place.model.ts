import { BaseModel } from '@shared/entities/models/base.model';
import { List } from '@lib/decorators/list.decorator';


export class Place extends BaseModel {
	placeId: number;

	@List('Place','National code')
	placeNationalCode: string

	@List('Place','Place name')
	placeName: string

	districtId: number

	regionId: number
}