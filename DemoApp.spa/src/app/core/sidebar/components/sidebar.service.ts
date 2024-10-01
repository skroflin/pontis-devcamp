import { BehaviorSubject } from 'rxjs'
import { Injectable } from '@angular/core'
import { SidebarItem } from './sidebar-items/models/sidebar-item.model'

@Injectable({ providedIn: 'root' })
export class SidebarService {
  private showSidebarChildren: BehaviorSubject<SidebarItem> = new BehaviorSubject<SidebarItem>(null)
  public showSidebarChildren$ = this.showSidebarChildren.asObservable()

  showSidebarChildrenDetails(sidebarItem: SidebarItem) {
    this.showSidebarChildren.next(sidebarItem)
  }

  private toggleSidebar: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false)
  public toggleSidebar$ = this.toggleSidebar.asObservable()

  toggleSidebarState(sidebarState: boolean) {
    this.toggleSidebar.next(sidebarState)
  }
}
