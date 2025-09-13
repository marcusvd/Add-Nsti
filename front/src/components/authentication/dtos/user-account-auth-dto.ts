export class UserAccountAuthDto {
  id!:number;
  userName!: string;
  displayUserName!:string;
  email!:string;
  emailConfirmed!:boolean;
  lastLogin!:Date;
  willExpires!:Date;
  registered!:Date;
}
