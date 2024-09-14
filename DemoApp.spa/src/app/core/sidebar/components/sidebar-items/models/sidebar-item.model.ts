import { Role } from '@shared/entities/enums/role.enum';
import { SidebarItemChild } from '../sidebar-item-children/models/sidebar-item-children.model';

export interface SidebarItem {
  path: string;
  title: string;
  type: string;
  permissions: Role[];
  badge: string;
  children?: SidebarItemChild[];
}
