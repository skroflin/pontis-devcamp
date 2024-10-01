import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class SpinnerService {
  private spinnerStateLoadingSource: BehaviorSubject<boolean> =
    new BehaviorSubject<boolean>(false);
  public spinnerStateLoading$ = this.spinnerStateLoadingSource.asObservable();

  loading(isLoading: boolean) {
    this.spinnerStateLoadingSource.next(isLoading);
  }
}
