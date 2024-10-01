import { BehaviorSubject } from 'rxjs'
import { Injectable } from '@angular/core'

@Injectable({ providedIn: 'root' })
export class BaseTableService {
  private showFormDetailsSource: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false)
  public showFormDetails$ = this.showFormDetailsSource.asObservable()

  showFormDetails(showFormDetails: boolean) {
    this.showFormDetailsSource.next(showFormDetails)
  }
}
