
export class UpdateUserAccountEmailDto {
  constructor(Old: string, newE: string) {
    this.oldEmail = Old;
    this.newEmail = newE;
  }
  oldEmail!: string
  newEmail!: string
}
