import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

/** DOMAIN */
import { Employee } from '../employee.model';
import { TableMetadata } from '@shared/entities/models/table-metadata';
import { TableRowDeleteResult } from '@shared/entities/models/table-row-delete-result';
import { PagedListDto } from '@shared/entities/models/paged-list-dto';

/** COMPONENTS */
import { DialogComponent } from '@lib/dialog/dialog.component';

/** SERVICES */
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
})
export class EmployeeListComponent implements OnInit {
  entity: Employee;
  showFormDetails: boolean = false;
  
  pagedListDto: PagedListDto<Employee>;
  tableMetadata: TableMetadata = new TableMetadata();
  tableHeader: string = 'Employees';
  type: Employee = new Employee();
  orderBy: string = 'Id';
  dtoSuffix: string = 'employee';

  constructor(
    private employeeService: EmployeeService,
    private cd: ChangeDetectorRef,
    public dialog: MatDialog
  ) { }

  ngOnInit() { }

  showDetails(entity?: Employee, showFormDetails?: boolean) {
    this.entity = entity;
    this.showFormDetails = showFormDetails;
  }

  add() {
    this.entity = new Employee();
    this.showFormDetails = true;
  }

  delete(tableRowDeleteResult: TableRowDeleteResult) {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        header: 'Delete  employee',
        message: 'Are you certain you wish to delete  employee?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.employeeService.delete(tableRowDeleteResult.entity.id).subscribe(() => this.updateTable(tableRowDeleteResult.tableMetadata));
      }
    });
    this.updateTable(tableRowDeleteResult.tableMetadata);
  }

  updateTable(tableMetadata?: TableMetadata) {
  this.tableMetadata = tableMetadata ? tableMetadata : this.tableMetadata;
    this.employeeService
      .getPaged(tableMetadata)
      .subscribe((pagedListDto) => {
        this.pagedListDto = pagedListDto;

        this.cd.detectChanges();
      });
  }
}
