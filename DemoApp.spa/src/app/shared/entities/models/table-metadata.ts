import { FilterMetadata } from './filter-metadata';
import { PagingMetadata } from './paging-metadata';
import { SortMetadata } from './sort-metadata';

export class TableMetadata{
    filterMetadata: FilterMetadata;
    sortMetadata: SortMetadata;
    pagingMetadata: PagingMetadata;
}
