import { SpinnerService } from './spinner.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
})
export class SpinnerComponent implements OnInit {
  public loading: boolean = false;

  constructor(private spinnerService: SpinnerService) {
    this.spinnerService.spinnerStateLoading$.subscribe(
      (isLoading) => (this.loading = isLoading)
    );
  }

  ngOnInit() {}
}
