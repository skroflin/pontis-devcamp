import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

/** DOMAIN */
import { NationalIdType } from '../national-id-type.model';
import { TableMetadata } from '@shared/entities/models/table-metadata';
import { TableRowDeleteResult } from '@shared/entities/models/table-row-delete-result';
import { PagedListDto } from '@shared/entities/models/paged-list-dto';

/** COMPONENTS */
import { DialogComponent } from '@lib/dialog/dialog.component';

/** SERVICES */
import { NationalIdTypeService } from '../national-id-type.service';

@Component({
  selector: 'app-national-id-type-list',
  templateUrl: './national-id-type-list.component.html',
})
export class NationalIdTypeListComponent implements OnInit {
  entity: NationalIdType;
  showFormDetails: boolean = false;
  
  pagedListDto: PagedListDto<NationalIdType>;
  tableMetadata: TableMetadata = new TableMetadata();
  tableHeader: string = 'National id types';
  type: NationalIdType = new NationalIdType();
  orderBy: string = 'Id';
  dtoSuffix: string = 'nationalIdType';

  constructor(
    private nationalIdTypeService: NationalIdTypeService,
    private cd: ChangeDetectorRef,
    public dialog: MatDialog
  ) { }

  ngOnInit() { }

  showDetails(entity?: NationalIdType, showFormDetails?: boolean) {
    this.entity = entity;
    this.showFormDetails = showFormDetails;
  }

  add() {
    this.entity = new NationalIdType();
    this.showFormDetails = true;
  }

  delete(tableRowDeleteResult: TableRowDeleteResult) {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        header: 'Delete  national id type',
        message: 'Are you certain you wish to delete  national id type?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.nationalIdTypeService.delete(tableRowDeleteResult.entity.id).subscribe(() => this.updateTable(tableRowDeleteResult.tableMetadata));
      }
    });
    this.updateTable(tableRowDeleteResult.tableMetadata);
  }

  updateTable(tableMetadata?: TableMetadata) {
  this.tableMetadata = tableMetadata ? tableMetadata : this.tableMetadata;
    this.nationalIdTypeService
      .getPaged(tableMetadata)
      .subscribe((pagedListDto) => {
        this.pagedListDto = pagedListDto;

        this.cd.detectChanges();
      });
  }
}
