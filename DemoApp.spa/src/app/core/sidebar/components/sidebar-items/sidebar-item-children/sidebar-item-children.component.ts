import { Subscription } from 'rxjs';
import { Component, OnInit, Input, OnDestroy } from '@angular/core';

/** DOMAIN */
import { SidebarItem } from '../models/sidebar-item.model';
import { SidebarItemChild } from './models/sidebar-item-children.model';

/** SERVICES */
import { SidebarService } from './../../sidebar.service';

@Component({
	selector: 'app-sidebar-item-children',
	templateUrl: 'sidebar-item-children.component.html',
})
export class SidebarItemChildrenComponent implements OnInit, OnDestroy {
	sidebarItem: SidebarItem;
	sidebarItemChildren: SidebarItemChild[];
	subsciptions: Subscription[] = [];

	constructor(private sidebarService: SidebarService) {
		this.subsciptions.push(this.sidebarService.showSidebarChildren$.subscribe((sidebarItem: SidebarItem) => {
			if (sidebarItem) {
				this.sidebarItem = sidebarItem;
				this.sidebarItemChildren = sidebarItem.children;
			}
		}));
	}


	ngOnDestroy(): void {
		this.subsciptions.forEach((subscription) => { subscription.unsubscribe() });
	}

	ngOnInit() {
	}
}
