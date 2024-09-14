import { Component, OnInit } from '@angular/core';

/** DOMAIN */
import { SidebarItem } from './models/sidebar-item.model';

/** SERVICES */
import { SidebarService } from './../sidebar.service';
import { AuthenticationService } from '../../../authentication/authentication.service';

/** DATA */
import { ROUTES } from './data/routes';
import { SidebarItemChild } from './sidebar-item-children/models/sidebar-item-children.model';

@Component({
	selector: 'app-sidebar-items',
	templateUrl: 'sidebar-items.component.html',
})
export class SidebarItemsComponent implements OnInit {
	currentSidebarItem: SidebarItem = null;
	sidebarItems: SidebarItem[];

	constructor(private sidebarService: SidebarService, private authenticationService: AuthenticationService) { }

	ngOnInit() {
		const routes: SidebarItem[] = ROUTES;
		const userRole = this.authenticationService.getUserRole();
		let allowedRoutes: SidebarItem[] = [];
		for (let i = 0; i < routes.length; i++) {
			if (routes[i].permissions.includes(userRole)){
				let route = routes[i];
				let routeChildren = route.children;
				let allowedChildrenRoutes: SidebarItemChild[] = [];

				for(let j = 0; j < routeChildren.length; j++){
					if(routeChildren[j].permissions.includes(userRole)){
						allowedChildrenRoutes.push(routeChildren[j]);
					}
					route.children = allowedChildrenRoutes;
				}
				allowedRoutes.push(route);
			}
		}
		this.sidebarItems = allowedRoutes;
	}

	showSidebarItemChildren(sidebarItem: SidebarItem) {
		if (this.currentSidebarItem === sidebarItem) {
			this.currentSidebarItem = null;
		} else {
			this.currentSidebarItem = sidebarItem;
		}

		this.sidebarService.showSidebarChildrenDetails(this.currentSidebarItem);
	}
}
