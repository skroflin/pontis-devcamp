import { BaseModel } from '@shared/entities/models/base.model';
import { List } from '@lib/decorators/list.decorator';


export class Application extends BaseModel {
	applicationId: number;

	@List('Application','Application name')
	applicationName: string
}