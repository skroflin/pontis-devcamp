import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { Injectable, Injector, NgZone } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from '@environments/environment';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
  constructor(
    public snackBar: MatSnackBar,
    private injector: Injector,
    private zone: NgZone
  ) { }

  baseUrl: string = `${environment.baseUrl}`;
  whitelist: string[] = [/**`${this.baseUrl}/user/user-login` */]

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = '';

        if (error.error instanceof ErrorEvent) {
          // client-side error
          errorMessage = `Error: ${error.error.message}`;
        } else {
          // server-side error
          console.log(error);
          errorMessage = `Error Code: ${error.status}\n Message: ${error.error != null ? error.error.message : error.message
            }`;
        }

        console.log(this.whitelist);

        if (!this.whitelist.find(x => x === request.url)) {
          this.snackBar = this.injector.get(MatSnackBar);
          this.zone.run(() => {
            this.snackBar.open(errorMessage, 'OK', {
              duration: 5000,
            });
          });
        }
        return throwError(() => new Error(errorMessage));
      })
    );
  }
}
