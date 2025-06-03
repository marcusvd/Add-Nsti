import { Component, OnInit } from '@angular/core';
import { DefaultCompImports } from 'components/imports/default-comp-imports';
import { map, Observable, of, Subscription } from 'rxjs';
import { OnClickInterface } from 'shared/components/list-g/list/interfaces/on-click-interface';
import { ListImports } from '../../../imports/crud-customer-equipment-employees-imports';
import { EquipmentDto } from '../../commons-components/dtos/equipment-dto';
import { ListEquipmentDto } from '../../commons-components/dtos/list-equipment-dto';
import { EquipmentImports } from '../../imports/imports-equipment';
import { ListEquipmentService } from '../../services/list-equipment.service';

@Component({
  selector: 'app-list-equipment',
  standalone: true,
  imports: [
    EquipmentImports,
    ListImports,
    DefaultCompImports
  ],
  templateUrl: './list-equipment.component.html',
  styleUrl: './list-equipment.component.scss'
})
export class ListEquipmentComponent implements OnInit {

  entities$!: Observable<ListEquipmentDto[]>;
  entities: ListEquipmentDto[] = [];
  entitiesFiltered$!: Observable<ListEquipmentDto[] | undefined>;
  entitiesFiltered: ListEquipmentDto[] = [];

  constructor(private _listEquipmentService: ListEquipmentService) { }
  ngOnInit(): void {
    this.startSupply();
  }

  labelHeadersMiddle = () => {
    return [
      { key: '', style: 'cursor: pointer;' },
      { key: 'Equipamento', style: 'cursor: pointer;color: rgb(11, 112, 155);' },
      { key: 'Modelo', style: 'cursor: pointer;color: rgb(11, 112, 155);' },
      { key: 'Fabricante', style: 'cursor: pointer;color: rgb(11, 112, 155);' }
    ]
  }
  fieldsHeadersMiddle = () => {
    return [
      { key: 'id', style: '' },
      { key: 'description', style: '' },
      { key: 'model', style: '' },
      { key: 'manufacturer', style: '' }
    ]
  }

  supplyItemsGrid = (equipmentList: ListEquipmentDto[], equipment: EquipmentDto) => {

    const items: ListEquipmentDto = new ListEquipmentDto();
    // equipment.description)
    Object.assign(items, {

      id: {
        key: equipment.id.toString(),
        display: 'icons',
        icons: ['list|', 'edit|', 'home|'],
        styleInsideCell: `color: rgb(11, 112, 155); cursor: pointer; font-size:20px; padding-right:15 px;`,
        styleCell: '',
        route: ''
      },


      description: {
        key: equipment.description,
        styleCell: 'width:100%;'
      },

      model: {
        key: equipment.Model.description,
        styleCell: 'width:100%;',
      },

      manufacturer: {
        key: equipment.Manufacturer.description,
        styleCell: 'width:100%;',
      }
    })

    equipmentList.push(items);

    return equipmentList;
  }
  onClickIcons($event: OnClickInterface) {
   
  }
  onClickButton($event: string) {
   
  }
  onClickOrderByFields($event: string, arg1: Observable<ListEquipmentDto[] | undefined>) {
    
  }
  callRouter(arg0: string) {
 
  }



  customerSubscribe!: Subscription | undefined;



  ngOnDestroy(): void {
    this.customerSubscribe?.unsubscribe();
  }

  startSupply() {

    let entities: ListEquipmentDto[] = [];

    const equipments: Observable<EquipmentDto[]> = this._listEquipmentService.getData();

    equipments.pipe(
      map(y => {
        // y)
        y.forEach(x => {
          this.entities = this.supplyItemsGrid(entities, x);
          this.entities$ = of(this.entities);
          this.entitiesFiltered$ = this.entities$;
        }

        )
      })).subscribe();




  }
}
