import { SocialMediasDto } from "./social-medias-dto";


export class ContactDto {
  id: number = 0;
  email: string = '';
  site: string = '';
  cel: string = '';
  zap: string = '';
  landline: string = '';
  socialMedias: SocialMediasDto[] = [];
}
