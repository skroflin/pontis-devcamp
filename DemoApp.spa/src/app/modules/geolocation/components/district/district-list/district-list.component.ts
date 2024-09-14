import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

/** DOMAIN */
import { District } from '../district.model';
import { TableMetadata } from '@shared/entities/models/table-metadata';
import { TableRowDeleteResult } from '@shared/entities/models/table-row-delete-result';
import { PagedListDto } from '@shared/entities/models/paged-list-dto';

/** COMPONENTS */
import { DialogComponent } from '@lib/dialog/dialog.component';

/** SERVICES */
import { DistrictService } from '../district.service';

@Component({
  selector: 'app-district-list',
  templateUrl: './district-list.component.html',
})
export class DistrictListComponent implements OnInit {
  entity: District;
  showFormDetails: boolean = false;
  
  pagedListDto: PagedListDto<District>;
  tableMetadata: TableMetadata = new TableMetadata();
  tableHeader: string = 'Districts';
  type: District = new District();
  orderBy: string = 'Id';
  dtoSuffix: string = 'district';

  constructor(
    private districtService: DistrictService,
    private cd: ChangeDetectorRef,
    public dialog: MatDialog
  ) { }

  ngOnInit() { }

  showDetails(entity?: District, showFormDetails?: boolean) {
    this.entity = entity;
    this.showFormDetails = showFormDetails;
  }

  add() {
    this.entity = new District();
    this.showFormDetails = true;
  }

  delete(tableRowDeleteResult: TableRowDeleteResult) {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        header: 'Delete  district',
        message: 'Are you certain you wish to delete  district?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.districtService.delete(tableRowDeleteResult.entity.id).subscribe(() => this.updateTable(tableRowDeleteResult.tableMetadata));
      }
    });
    this.updateTable(tableRowDeleteResult.tableMetadata);
  }

  updateTable(tableMetadata?: TableMetadata) {
  this.tableMetadata = tableMetadata ? tableMetadata : this.tableMetadata;
    this.districtService
      .getPaged(tableMetadata)
      .subscribe((pagedListDto) => {
        this.pagedListDto = pagedListDto;

        this.cd.detectChanges();
      });
  }
}
