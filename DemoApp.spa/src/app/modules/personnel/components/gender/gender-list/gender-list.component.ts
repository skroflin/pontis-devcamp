import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

/** DOMAIN */
import { Gender } from '../gender.model';
import { TableMetadata } from '@shared/entities/models/table-metadata';
import { TableRowDeleteResult } from '@shared/entities/models/table-row-delete-result';
import { PagedListDto } from '@shared/entities/models/paged-list-dto';

/** COMPONENTS */
import { DialogComponent } from '@lib/dialog/dialog.component';

/** SERVICES */
import { GenderService } from '../gender.service';

@Component({
  selector: 'app-gender-list',
  templateUrl: './gender-list.component.html',
})
export class GenderListComponent implements OnInit {
  entity: Gender;
  showFormDetails: boolean = false;
  
  pagedListDto: PagedListDto<Gender>;
  tableMetadata: TableMetadata = new TableMetadata();
  tableHeader: string = 'Genders';
  type: Gender = new Gender();
  orderBy: string = 'Id';
  dtoSuffix: string = 'gender';

  constructor(
    private genderService: GenderService,
    private cd: ChangeDetectorRef,
    public dialog: MatDialog
  ) { }

  ngOnInit() { }

  showDetails(entity?: Gender, showFormDetails?: boolean) {
    this.entity = entity;
    this.showFormDetails = showFormDetails;
  }

  add() {
    this.entity = new Gender();
    this.showFormDetails = true;
  }

  delete(tableRowDeleteResult: TableRowDeleteResult) {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        header: 'Delete  gender',
        message: 'Are you certain you wish to delete  gender?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.genderService.delete(tableRowDeleteResult.entity.id).subscribe(() => this.updateTable(tableRowDeleteResult.tableMetadata));
      }
    });
    this.updateTable(tableRowDeleteResult.tableMetadata);
  }

  updateTable(tableMetadata?: TableMetadata) {
  this.tableMetadata = tableMetadata ? tableMetadata : this.tableMetadata;
    this.genderService
      .getPaged(tableMetadata)
      .subscribe((pagedListDto) => {
        this.pagedListDto = pagedListDto;

        this.cd.detectChanges();
      });
  }
}
