import { Role } from '@shared/entities/enums/role.enum';
import { SidebarItem } from '../models/sidebar-item.model';

export const ROUTES: SidebarItem[] = [
  {
    path: '',
    title: 'Administration',
    permissions: [Role.Admin],
    type: 'link',
    badge: 'ADM',
    children: [
      {
        path: 'administration/applications',
        title: 'Applications',
        permissions: [Role.Admin],
        type: 'link',
        badge: '',
      },
    ],
  },
  {
    path: '',
    title: 'Geolocation',
    permissions: [Role.Admin, Role.Reader, Role.Writer],
    type: 'link',
    badge: 'GEO',
    children: [
      {
        path: 'geolocation/countries',
        title: 'Countries',
        permissions: [, Role.Admin, Role.Reader, Role.Writer],
        type: 'link',
        badge: '',
      },
      {
        path: 'geolocation/districts',
        title: 'Districts',
        permissions: [Role.Admin,Role.Reader, Role.Writer],
        type: 'link',
        badge: '',
      },
      {
        path: 'geolocation/places',
        title: 'Places',
        permissions: [Role.Admin, Role.Reader, Role.Writer],
        type: 'link',
        badge: '',
      },
      {
        path: 'geolocation/regions',
        title: 'Regions',
        permissions: [Role.Admin, Role.Reader, Role.Writer],
        type: 'link',
        badge: '',
      },
    ],
  },
  {
    path: '',
    title: 'Personnel',
    permissions: [Role.Admin, Role.Reader, Role.Writer],
    type: 'link',
    badge: 'PER',
    children: [
      {
        path: 'personnel/employees',
        title: 'Employees',
        permissions: [Role.Admin, Role.Reader, Role.Writer],
        type: 'link',
        badge: '',
      },
      {
        path: 'personnel/genders',
        title: 'Genders',
        permissions: [Role.Admin, Role.Reader, Role.Writer],
        type: 'link',
        badge: '',
      },
      {
        path: 'personnel/nationalidtypes',
        title: 'National id types',
        permissions: [Role.Admin, Role.Reader, Role.Writer],
        type: 'link',
        badge: '',
      }
    ],
  }
];
