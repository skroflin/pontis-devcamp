import { Component } from '@angular/core';

/** SERVICES */
import { AuthenticationService } from './../../authentication/authentication.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent {
  constructor(private authenticationService: AuthenticationService) {}

  logout() {
    this.authenticationService.logout();
  }
}
