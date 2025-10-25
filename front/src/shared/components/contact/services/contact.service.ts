import { Injectable } from "@angular/core";
import { FormArray, FormBuilder, FormGroup, Validators } from "@angular/forms";

import { BaseForm } from '../../../extends/forms/base-form';
import { ContactDto } from "../dtos/contact-dto";
import { SocialMediasDto } from "../dtos/social-medias-dto";
import { atLeastOneContactValidator } from "../validators/at-least-one-contact.validator";

@Injectable({ providedIn: 'root' })
export class ContactService extends BaseForm {

  constructor(
    private _fb: FormBuilder,

  ) {
    super()

  }


  formLoad(contact?: ContactDto) {

    let cel = '0'.repeat(11);
    let zap = '0'.repeat(11);
    let landline = '0'.repeat(10);

    return this.formMain = this._fb.group({
      id: [contact?.id ?? 0, [Validators.required]],
      email: [contact?.email ?? '', [Validators.required, Validators.email, Validators.maxLength(150)]],
      cel: [contact?.cel ?? cel ?? '', [Validators.minLength(11)]],
      zap: [contact?.zap ?? zap ?? '', [Validators.minLength(10), Validators.maxLength(11)]],
      landline: [contact?.landline ?? '', [Validators.minLength(10)]],
      site: [contact?.site ?? '', [Validators.maxLength(150)]],
      socialMedias: this._fb.array([])
    }, { validator: atLeastOneContactValidator() });

  }



  seedingSocialnetworks(socialMedias?: SocialMediasDto[]) {

    socialMedias?.forEach((item: SocialMediasDto) => {
      return this.socialNets.push(this.subForm = this._fb.group({
        id: [item?.id ?? '', [Validators.required]],
        name: [item?.name ?? '', [Validators.required, Validators.maxLength(150)]],
        url: [item?.url ?? '', [Validators.required, Validators.maxLength(150)]]
      }))

    })

  }

  get subFormValidation() {
    return this.subForm;
  }

  socialNetworkValidators(): FormGroup {
    return this.subForm = this._fb.group({
      name: ['', [Validators.required, Validators.maxLength(150)]],
      url: ['', [Validators.required, Validators.maxLength(150)]]
    })
  }

  addSocialNets() {
    this.socialNets.push(this.socialNetworkValidators())
  }

  get socialNets(): FormArray {
    return <FormArray>this.formMain.get('socialMedias');
  }
}
