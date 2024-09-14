import { Authorization } from "@shared/entities/enums/authorization.enum";
import { Role } from "@shared/entities/enums/role.enum";

export class UserLoginData{
    username: string;
    userRole: Role;
    userAuthorizations: Authorization[];
  }