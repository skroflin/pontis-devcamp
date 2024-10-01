import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';

import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from "@angular/material/icon";

import { MatSnackBarModule } from '@angular/material/snack-bar';

/** COMPONENTS */
import { BaseTableComponent } from './table/base-table.component';
import { DialogComponent } from './dialog/dialog.component';

@NgModule({
  imports: [
    CommonModule,

    MatTableModule,
    MatSortModule,
    MatPaginatorModule,

    MatDialogModule,
    MatIconModule, 

    MatSnackBarModule
  ],
  declarations: [BaseTableComponent, DialogComponent],
  exports: [BaseTableComponent, DialogComponent]
})
export class LibModule { }
