import { PagingMetadata } from './paging-metadata';

export class PagedListDto<T>{
    pagedData: T[];
    pagingMetadata: PagingMetadata;
}