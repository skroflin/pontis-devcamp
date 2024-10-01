import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

/** LIBRARIES */
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';

/** COMPONENTS */
import { NavbarComponent } from './components/navbar.component';
@NgModule({
  declarations: [NavbarComponent],
  imports: [
    CommonModule,
    MatMenuModule,
    MatButtonModule,
    MatIconModule,
  ],
  exports: [NavbarComponent],
})
export class NavbarModule {}
