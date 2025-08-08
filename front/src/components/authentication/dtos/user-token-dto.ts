export class UserTokenDto {
  authenticated!: boolean;
  expiration!: Date;
  token!: string;
  id!: number;
  userName!: string;
  email!: string;
  companyId!: number;
  action!: string;
  Roles!: string[];
}
