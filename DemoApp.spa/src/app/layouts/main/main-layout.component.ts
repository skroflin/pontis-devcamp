import { Component, OnInit, ViewChild } from '@angular/core';

/** COMPONENTS */
import { SidebarComponent } from '../../core/sidebar/components/sidebar.component';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
})
export class MainLayoutComponent implements OnInit {
  @ViewChild(SidebarComponent, { static: true }) sidebar: SidebarComponent;

  constructor() {}

  ngOnInit() {}
}
