import { RootBase } from "shared/entities-dtos/root-base";
import { ManufacturerDto } from "./manufacturer-dto";
import { ModelDto } from "./model-dto";

export class EquipmentDto extends RootBase{
  description!:string;
  ModelId!:number;
  Model!:ModelDto;
  ManufacturerId!:number;
  Manufacturer!:ManufacturerDto;
}
