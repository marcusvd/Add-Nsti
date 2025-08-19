export class UserTokenDto {
  id!: number;
  businessId!: number;
  authenticated!: boolean;
  expiration!: Date;
  token!: string;
  userName!: string;
  email!: string;
  action!: string;
  Roles!: string[];
}
