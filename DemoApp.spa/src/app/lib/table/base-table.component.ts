import { FilterMetadata } from './../../shared/entities/models/filter-metadata';
import { Output, EventEmitter, Component, ViewChild, Input, OnChanges, SimpleChanges, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort, Sort } from '@angular/material/sort';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

import { getListProperties, getAdjustedColumnName } from '../decorators/list.decorator';
import { getFilterProperties } from '../decorators/filter.decorator';

/** DOMAIN */
import { TableMetadata } from './../../shared/entities/models/table-metadata';
import { SortMetadata } from './../../shared/entities/models/sort-metadata';

/** SERVICES */
import { BaseTableService } from './base-table.service';
import { TableRowDeleteResult } from '../../shared/entities/models/table-row-delete-result';
import { AuthenticationService } from '../../core/authentication/authentication.service';
import { Authorization } from '@shared/entities/enums/authorization.enum';
@Component({
  selector: 'app-base-table',
  templateUrl: './base-table.component.html',
})
export class BaseTableComponent implements OnChanges, OnInit {
  @Input() pagedListDto: any;
  @Input() tableMetadata: TableMetadata;
  @Input() tableHeader;
  @Input() type: any;
  @Input() orderBy: string;
  @Input() dtoSuffix: string = '';
  @Input() showAddBtn: boolean = false;
  @Input() showDeleteBtn: boolean = false;
  @Input() showFormDetails: boolean = false;
  @Input() testText: string = '';

  @Output() onLoad = new EventEmitter();
  @Output() onAdd = new EventEmitter();
  @Output() onShowDetails = new EventEmitter();
  @Output() onDelete = new EventEmitter();
  @Output() onFilter = new EventEmitter();
  @Output() onSort = new EventEmitter();
  @Output() onPaginate = new EventEmitter();
  @Output() childTestEmitter = new EventEmitter();

  dataSource: MatTableDataSource<any>;
  @ViewChild(MatSort, { static: true }) matSort: MatSort;
  @ViewChild(MatPaginator, { static: true }) matPaginator: MatPaginator;

  columns: string[];
  columnsWithFilter: string[];

  canAdd: boolean = false;
  canOpen: boolean = false;
  canDelete: boolean = false;
  constructor(private baseTableService: BaseTableService, private authenticationService: AuthenticationService) {
    this.baseTableService.showFormDetails$.subscribe((result) => {
      this.showFormDetails = result;
    });
  }

  ngOnInit() {
    const authorizations = this.authenticationService.getUserAuthorizations();
    this.canAdd = authorizations.includes(Authorization.Write);
    this.canOpen = authorizations.includes(Authorization.Open);
    this.canDelete = authorizations.includes(Authorization.Delete);

    this.tableMetadata = {
      filterMetadata: null,
      sortMetadata: {
        orderBy: this.orderBy,
        orderDirection: 'ASC',
      },
      pagingMetadata: {
        offset: 0,
        pageCount: 1,
        pageIndex: 0,
        pageSize: 5,
        totalCount: 0,
      },
    };

    console.log(this.testText);
    console.log(this.pagedListDto);

    this.onLoad.emit(this.tableMetadata);
    this.childTestEmitter.emit('Pozdrav iz child kompomente');
  }

  ngOnChanges(changes: SimpleChanges) {
    for (const propName in changes) {
      if (changes.hasOwnProperty(propName)) {
        switch (propName) {
          case 'pagedListDto': {
            if (this.pagedListDto) {
              this.setDataSource();
              this.setDataSourceAttributes();
              console.log(this.pagedListDto);

            }
          }
        }
      }
    }
  }

  setDataSource() {
    this.columns = this.getColumns();
    this.columnsWithFilter = this.getColumnsWithFilter();
    this.columns.push('actions');

    this.dataSource = new MatTableDataSource(this.pagedListDto.pagedData);
    this.tableMetadata.pagingMetadata = this.pagedListDto.pagingMetadata;
  }

  setDataSourceAttributes() {
    this.dataSource.sort = this.matSort;
  }

  getColumns(): string[] {
    const listProperties = getListProperties(this.type);
    return listProperties ? Object.getOwnPropertyNames(listProperties) : [];
  }

  getColumnName(propertyKey: string): string {
    //#TASK 1 - Uncomment below and check how many times this method is called
    // console.log(propertyKey);
    return getAdjustedColumnName(this.type, propertyKey)
  }

  getColumnsWithFilter(): string[] {
    const filterProperties = getFilterProperties(this.type);
    return filterProperties ? Object.getOwnPropertyNames(filterProperties) : [];
  }

  add() {
    this.showFormDetails = true;
    this.onAdd.emit();
  }

  showDetails(entity: any) {
    this.showFormDetails = true;
    this.onShowDetails.emit(entity);
  }

  delete(entity: any) {
    this.onDelete.emit(<TableRowDeleteResult>{
      entity: entity,
      tableMetadata: this.tableMetadata,
    });
  }

  filter(filterValue: string, columnName: string) {
    if (filterValue.length >= 3) {
      let columnNormalized = columnName.replace('filter_', '').replace(this.dtoSuffix, '');
      const metadata: FilterMetadata = {
        filterValue: filterValue,
        filterColumn: columnNormalized,
      };
      this.tableMetadata.filterMetadata = metadata;
      this.onFilter.emit(this.tableMetadata);
    } else if (filterValue.length === 0) {
      this.tableMetadata.filterMetadata = {};
      this.onFilter.emit(this.tableMetadata);
    }
  }

  sort(sortValue: Sort) {
    const columnNormalized = sortValue.active.replace(this.dtoSuffix, '');
    const sort = <SortMetadata>{
      orderBy: columnNormalized,
      orderDirection: sortValue.direction.toUpperCase(),
    };

    this.tableMetadata.sortMetadata = sort
    this.onSort.emit(this.tableMetadata);
  }

  paginate(pageEvent: PageEvent) {
    const isPrev = pageEvent.previousPageIndex > pageEvent.pageIndex;
    const isNext = pageEvent.previousPageIndex < pageEvent.pageIndex;

    if (isPrev) {
      let previousPageIndex = (pageEvent.previousPageIndex === 0 || pageEvent.pageIndex === 0) ? 0 : pageEvent.previousPageIndex - 1;
      this.tableMetadata.pagingMetadata.offset = previousPageIndex * pageEvent.pageSize;
    } else if (isNext) {
      this.tableMetadata.pagingMetadata.offset = pageEvent.pageIndex * pageEvent.pageSize;
    } else {
      this.tableMetadata.pagingMetadata.offset = 0;
    }

    this.tableMetadata.pagingMetadata.pageIndex = pageEvent.pageIndex;
    this.tableMetadata.pagingMetadata.pageSize = pageEvent.pageSize;
    this.onPaginate.emit(this.tableMetadata);
  }
}
