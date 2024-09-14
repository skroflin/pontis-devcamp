import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

/** DOMAIN */
import { LoginRequestDto, LoginResponseDto } from './components/login/models/login-user.model';
import { ResponseState } from '@shared/entities/enums/response-state.enum';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Role } from '@shared/entities/enums/role.enum';
import { Authorization } from '@shared/entities/enums/authorization.enum';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  constructor(private httpClient: HttpClient, private router: Router) { }
  private url: string = `${environment.baseUrl}/authentication`;

  login(loginRequestDto: LoginRequestDto): Promise<ResponseState> {
    return new Promise((resolve) => {
      localStorage.removeItem('token');
      this.httpClient
        .post<LoginResponseDto>(`${this.url}/login`, loginRequestDto)
        .subscribe((loginResponseDto: LoginResponseDto) => {
          if (loginResponseDto) {
            localStorage.setItem('token', JSON.stringify(loginResponseDto));
            resolve(ResponseState.SUCCESS);
          }
          if (!localStorage.getItem('token')) {
            resolve(ResponseState.FAILURE);
          }
        });
    });
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/authentication/login']);
  }

  isLoggedIn() {
    return localStorage.getItem('token');
  }

  getUserRole(): Role {
    var token = localStorage.getItem('token');
    if (token) {
      var loginResponseDto: LoginResponseDto = JSON.parse(token);
      return loginResponseDto.userRole;
    }
  }

  getUserAuthorizations():Authorization[] {
    var token = localStorage.getItem('token');
    if (token) {
      var loginResponseDto: LoginResponseDto = JSON.parse(token);
      return loginResponseDto.roleAuthorizations;
    }
  }

  hasUserRole(role: Role): boolean {
    var token = localStorage.getItem('token');
    if (token) {
      var loginResponseDto: LoginResponseDto = JSON.parse(token);
      if (loginResponseDto.userRole == role) {
        return true;
      }
      return false;
    }

    return false;
  }

  hasUserAuthorization(authorization: Authorization): boolean {
    var token = localStorage.getItem('token');
    if (token) {
      var loginResponseDto: LoginResponseDto = JSON.parse(token);
      if (loginResponseDto.roleAuthorizations.includes(authorization)) {
        return true;
      }
      return false;
    }


    return false;
  }
}
