import { SocialMediasDto } from "./social-medias-dto";


export class ContactDto {
  id!: number;
  email!: string;
  site!: string;
  cel!: string;
  zap!: string;
  landline!: string;
  socialMedias!: SocialMediasDto[];
}
