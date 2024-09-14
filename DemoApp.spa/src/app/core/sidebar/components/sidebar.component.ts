import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

/** DOMAIN */
import { SidebarItem } from './sidebar-items/models/sidebar-item.model';

/** SERVICES */
import { SidebarService } from './sidebar.service';

@Component({
	selector: 'app-sidebar',
	templateUrl: 'sidebar.component.html',
})
export class SidebarComponent implements OnDestroy {
	public showSidebar: boolean = false;
	subsciptions: Subscription[] = [];

	constructor(private sidebarService: SidebarService) {
		this.subsciptions.push(this.sidebarService.showSidebarChildren$.subscribe((sidebarItem: SidebarItem) => {
			this.showSidebar = (sidebarItem !== null);
		}));
	}


	ngOnDestroy(): void {
		this.subsciptions.forEach((subscription) => { subscription.unsubscribe() });
	}
}
