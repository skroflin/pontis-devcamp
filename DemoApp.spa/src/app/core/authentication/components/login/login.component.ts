import { Component, Inject, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

/** DOMAIN */
import { InputErrorStateMatcher } from '@shared/utilities/custom-input-error-state-matcher';
import { LoginRequestDto } from './models/login-user.model';
import { ResponseState } from '@shared/entities/enums/response-state.enum';

/** SERVICES */
import { AuthenticationService } from '../../authentication.service';

@Component({
  selector: 'app-login-component',
  templateUrl: 'login.component.html',
})
export class LoginComponent implements OnInit {
  formGroup: FormGroup;
  matcher: InputErrorStateMatcher;
  loginFailureError: string;

  constructor(
    private authenticationService: AuthenticationService,
    private fb: FormBuilder,
    private router: Router,
  ) { }

  ngOnInit() {
      this.setFormGroup();
  }

  setFormGroup() {
    this.formGroup = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', Validators.required],
    });

    // Reset errors in form group
    Object.keys(this.formGroup.controls).forEach((control) => {
      this.formGroup.controls[control].markAsUntouched();
    });

    this.matcher = new InputErrorStateMatcher();
    this.loginFailureError = '';
  }

  login() {
    let userLogin = new LoginRequestDto();
    userLogin.username = this.formGroup.get('username').value;
    userLogin.password = this.formGroup.get('password').value;

    if (this.formGroup.valid) {
      this.authenticationService
        .login(userLogin)
        .then((response) => {
          console.log(response);
          if (response === ResponseState.SUCCESS) {
            this.router.navigate(['/']);
          } else {
            this.loginFailureError = 'Username or password is incorrect';
          }
        });
    }
  }
}
