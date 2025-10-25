
import { CompanyAuth } from '../../company/dtos/company-auth';
import { CompanyProfile } from '../../company/dtos/company-profile';

export class Update_Auth_ProfileDto {
  companyAuthId!: number;
  companyProfileId!: number;
  companyAuth!: CompanyAuth;
  companyProfile!: CompanyProfile;
}
