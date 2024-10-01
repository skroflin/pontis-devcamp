/** ANGULAR MODULES */
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

/** CUSTOM MODULES */
import { SidebarModule } from './core/sidebar/sidebar.module';
import { LibModule } from './lib/lib.module';
import { NavbarModule } from './core/navbar/navbar.module';

/** COMPONENTS */
import { AppComponent } from './app.component';
import { AuthenticationLayoutComponent } from './layouts/authentication/authentication-layout.component';
import { MainLayoutComponent } from './layouts/main/main-layout.component';

import { SpinnerComponent } from '@shared/components/spinner/spinner.component';

/** GUARDS */
import { AuthenticationGuard } from '@shared/guards/authentication.guard';

/** ROUTES */
import { AppRoutingModule } from './app-routing.module';

/** SERVICES */
import { SpinnerInterceptor } from '@shared/services/spinner-interceptor';
import { HttpErrorInterceptor } from './core/interceptors/http-error.interceptor.service';

@NgModule({
  declarations: [
    /** MAIN */
    AppComponent,
    AuthenticationLayoutComponent,
    MainLayoutComponent,

    /** SHARED */
    SpinnerComponent,
  ],
  imports: [
    AppRoutingModule,
    SidebarModule,
    LibModule,
    NavbarModule,

    CommonModule,
    BrowserAnimationsModule,
    HttpClientModule,
  ],
  providers: [
    AuthenticationGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: SpinnerInterceptor,
      multi: true,
    },
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
