import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTooltipModule } from '@angular/material/tooltip';

/** COMPONENTS */
import { SidebarItemChildrenComponent } from './components/sidebar-items/sidebar-item-children/sidebar-item-children.component';
import { SidebarItemsComponent } from './components/sidebar-items/sidebar-items.component';
import { SidebarComponent } from './components/sidebar.component';

@NgModule({
  imports: [CommonModule, RouterModule, MatIconModule, MatSidenavModule, MatTooltipModule, MatExpansionModule],
  declarations: [SidebarItemChildrenComponent,SidebarItemsComponent, SidebarComponent],
  exports: [SidebarItemsComponent, SidebarComponent],
})
export class SidebarModule { }
