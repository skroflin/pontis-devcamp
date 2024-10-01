import { Role } from '@shared/entities/enums/role.enum';

export interface SidebarItemChild {
  path: string;
  title: string;
  permissions: Role[];
  type?: string;
  badge?: string;
}
