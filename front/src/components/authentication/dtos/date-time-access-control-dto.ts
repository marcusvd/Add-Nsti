import { RootBase } from "../../../shared/entities-dtos/root-base";

export class TimedAccessControlDto extends RootBase {

  userId!: number;
  start!: string;
  end!: string;
  workBreakStart!: Date;
  workBreakEnd!: Date;

}
