import { Authorization } from "@shared/entities/enums/authorization.enum";
import { Role } from "@shared/entities/enums/role.enum";

export class LoginRequestDto {
  username: string;
  password: string;
  applicationName: string = "NetCoreApi";
}

export class LoginResponseDto{
  username: string;
  userRole: Role;
  roleAuthorizations: Authorization[];
}
