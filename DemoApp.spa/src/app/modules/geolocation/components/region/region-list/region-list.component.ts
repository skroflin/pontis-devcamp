import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

/** DOMAIN */
import { Region } from '../region.model';
import { TableMetadata } from '@shared/entities/models/table-metadata';
import { TableRowDeleteResult } from '@shared/entities/models/table-row-delete-result';
import { PagedListDto } from '@shared/entities/models/paged-list-dto';

/** COMPONENTS */
import { DialogComponent } from '@lib/dialog/dialog.component';

/** SERVICES */
import { RegionService } from '../region.service';

@Component({
  selector: 'app-region-list',
  templateUrl: './region-list.component.html',
})
export class RegionListComponent implements OnInit {
  entity: Region;
  showFormDetails: boolean = false;
  
  pagedListDto: PagedListDto<Region>;
  tableMetadata: TableMetadata = new TableMetadata();
  tableHeader: string = 'Regions';
  type: Region = new Region();
  dtoSuffix: string = 'region';
  orderBy: string = 'Name';

  constructor(
    private regionService: RegionService,
    private cd: ChangeDetectorRef,
    public dialog: MatDialog
  ) { }

  ngOnInit() { }

  showDetails(entity?: Region, showFormDetails?: boolean) {
    this.entity = entity;
    this.showFormDetails = showFormDetails;
  }

  add() {
    this.entity = new Region();
    this.showFormDetails = true;
  }

  delete(tableRowDeleteResult: TableRowDeleteResult) {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        header: 'Delete  region',
        message: 'Are you certain you wish to delete  region?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.regionService.delete(tableRowDeleteResult.entity.id).subscribe(() => this.updateTable(tableRowDeleteResult.tableMetadata));
      }
    });
    this.updateTable(tableRowDeleteResult.tableMetadata);
  }

  updateTable(tableMetadata?: TableMetadata) {
  this.tableMetadata = tableMetadata ? tableMetadata : this.tableMetadata;
    this.regionService
      .getPaged(tableMetadata)
      .subscribe((pagedListDto) => {
        this.pagedListDto = pagedListDto;

        this.cd.detectChanges();
      });
  }
}
