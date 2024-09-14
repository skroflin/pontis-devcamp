import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

/** DOMAIN */
import { Place } from '../place.model';
import { TableMetadata } from '@shared/entities/models/table-metadata';
import { TableRowDeleteResult } from '@shared/entities/models/table-row-delete-result';
import { PagedListDto } from '@shared/entities/models/paged-list-dto';

/** COMPONENTS */
import { DialogComponent } from '@lib/dialog/dialog.component';

/** SERVICES */
import { PlaceService } from '../place.service';

@Component({
  selector: 'app-place-list',
  templateUrl: './place-list.component.html',
})
export class PlaceListComponent implements OnInit {
  entity: Place;
  showFormDetails: boolean = false;
  Test: string = 'Test';

  pagedListDto: PagedListDto<Place>;
  tableMetadata: TableMetadata = new TableMetadata();
  tableHeader: string = 'Places';
  type: Place = new Place();
  orderBy: string = 'Id';
  dtoSuffix: string = 'place';

  constructor(
    private placeService: PlaceService,
    private cd: ChangeDetectorRef,
    public dialog: MatDialog
  ) {}

  ngOnInit() {}

  showDetails(entity?: Place, showFormDetails?: boolean) {
    this.entity = entity;
    this.showFormDetails = showFormDetails;
  }

  add() {
    this.entity = new Place();
    this.showFormDetails = true;
  }

  delete(tableRowDeleteResult: TableRowDeleteResult) {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        header: 'Delete  place',
        message: 'Are you certain you wish to delete  place?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.placeService
          .delete(tableRowDeleteResult.entity.id)
          .subscribe(() =>
            this.updateTable(tableRowDeleteResult.tableMetadata)
          );
      }
    });
    this.updateTable(tableRowDeleteResult.tableMetadata);
  }

  updateTable(tableMetadata?: TableMetadata) {
    this.tableMetadata = tableMetadata ? tableMetadata : this.tableMetadata;
    this.placeService.getPaged(tableMetadata).subscribe((pagedListDto) => {
      this.pagedListDto = pagedListDto;

      this.cd.detectChanges();
    });
  }

  parentTestEmitter(value: any) {
    console.log(value);
  }
}
