import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';

/** ROUTES */
import { AuthenticationRoutingModule } from './authentication-routing.module';

/** COMPONENTS */
import { LoginComponent } from './components/login/login.component';

/** SERVICES */
import { AuthenticationService } from './authentication.service';

@NgModule({
  imports: [
    CommonModule,
    AuthenticationRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule
  ],
  declarations: [LoginComponent],
})

export class AuthenticationModule { }
