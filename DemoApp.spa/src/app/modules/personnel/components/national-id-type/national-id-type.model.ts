import { List } from '@lib/decorators/list.decorator';


export class NationalIdType {
	id:number;

	@List('NationalIdType', 'Id type name')
	nationalIdTypeName: string;
}