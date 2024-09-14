import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

/** DOMAIN */
import { Country } from '../country.model';
import { TableMetadata } from '@shared/entities/models/table-metadata';
import { TableRowDeleteResult } from '@shared/entities/models/table-row-delete-result';
import { PagedListDto } from '@shared/entities/models/paged-list-dto';

/** COMPONENTS */
import { DialogComponent } from '@lib/dialog/dialog.component';

/** SERVICES */
import { CountryService } from '../country.service';

@Component({
  selector: 'app-country-list',
  templateUrl: './country-list.component.html',
})
export class CountryListComponent implements OnInit {
  entity: Country;
  showFormDetails: boolean = false;
  
  pagedListDto: PagedListDto<Country>;
  tableMetadata: TableMetadata = new TableMetadata();
  tableHeader: string = 'Countries';
  type: Country = new Country();
  orderBy: string = 'Id';
  dtoSuffix: string = 'country';

  constructor(
    private countryService: CountryService,
    private cd: ChangeDetectorRef,
    public dialog: MatDialog
  ) { }

  ngOnInit() { }

  showDetails(entity?: Country, showFormDetails?: boolean) {
    this.entity = entity;
    this.showFormDetails = showFormDetails;
  }

  add() {
    this.entity = new Country();
    this.showFormDetails = true;
  }

  delete(tableRowDeleteResult: TableRowDeleteResult) {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        header: 'Delete  country',
        message: 'Are you certain you wish to delete  country?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.countryService.delete(tableRowDeleteResult.entity.id).subscribe(() => this.updateTable(tableRowDeleteResult.tableMetadata));
      }
    });
    this.updateTable(tableRowDeleteResult.tableMetadata);
  }

  updateTable(tableMetadata?: TableMetadata) {
  this.tableMetadata = tableMetadata ? tableMetadata : this.tableMetadata;
    this.countryService
      .getPaged(tableMetadata)
      .subscribe((pagedListDto) => {
        this.pagedListDto = pagedListDto;

        this.cd.detectChanges();
      });
  }
}
