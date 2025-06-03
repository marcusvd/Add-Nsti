import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { EquipmentDto } from '../commons-components/dtos/equipment-dto';

@Injectable({
  providedIn: 'root'
})
export class ListEquipmentService {

  constructor() { }

  minDate = new Date('0001-01-01T00:00:00');

  equipments: EquipmentDto[] = [
    {
      id: 1,
      description: "Betoneira 400L Diesel",
      ModelId: 1,
      Model: { id: 1, description: "B400D", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 1,
      Manufacturer: { id: 1, description: "Tramontina", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 2,
      description: "Compressor de Ar 500PSI",
      ModelId: 2,
      Model: { id: 2, description: "CA500", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 2,
      Manufacturer: { id: 2, description: "Schulz", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 3,
      description: "Martelo Demolidor 30kg",
      ModelId: 3,
      Model: { id: 3, description: "MD30", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 3,
      Manufacturer: { id: 3, description: "Bosch", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 4,
      description: "Cortadora de Piso 10HP",
      ModelId: 4,
      Model: { id: 4, description: "CP10", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 4,
      Manufacturer: { id: 4, description: "Makita", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 5,
      description: "Vibrador de Concreto 2m",
      ModelId: 5,
      Model: { id: 5, description: "VC2", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 5,
      Manufacturer: { id: 5, description: "Vonder", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 6,
      description: "Plataforma Elevatória 12m",
      ModelId: 6,
      Model: { id: 6, description: "PE12", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 6,
      Manufacturer: { id: 6, description: "JLG", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 7,
      description: "Rolo Compactador Liso 1T",
      ModelId: 7,
      Model: { id: 7, description: "RCL1", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 7,
      Manufacturer: { id: 7, description: "Wacker Neuson", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 8,
      description: "Gerador Diesel 50kVA",
      ModelId: 8,
      Model: { id: 8, description: "GD50", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 8,
      Manufacturer: { id: 8, description: "Cummins", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 9,
      description: "Serra Mármore 180mm",
      ModelId: 9,
      Model: { id: 9, description: "SM180", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 9,
      Manufacturer: { id: 9, description: "Dewalt", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 10,
      description: "Furadeira de Impacto 1/2",
      ModelId: 10,
      Model: { id: 10, description: "FI12", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 10,
      Manufacturer: { id: 10, description: "Milwaukee", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 11,
      description: "Andaime Metálico 2x1.5m",
      ModelId: 11,
      Model: { id: 11, description: "AM215", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 11,
      Manufacturer: { id: 11, description: "Alcoa", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 12,
      description: "Cortadora de Concreto 14",
      ModelId: 12,
      Model: { id: 12, description: "CC14", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 12,
      Manufacturer: { id: 12, description: "Husqvarna", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 13,
      description: "Martelete Rompedor 15kg",
      ModelId: 13,
      Model: { id: 13, description: "MR15", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 13,
      Manufacturer: { id: 13, description: "Hilti", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 14,
      description: "Betoneira 300L Elétrica",
      ModelId: 14,
      Model: { id: 14, description: "B300E", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 1,
      Manufacturer: { id: 1, description: "Tramontina", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 15,
      description: "Compactador de Solo 75kg",
      ModelId: 15,
      Model: { id: 15, description: "CS75", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 14,
      Manufacturer: { id: 14, description: "Bomag", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 16,
      description: "Serra Circular 7 1/4",
      ModelId: 16,
      Model: { id: 16, description: "SC714", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 3,
      Manufacturer: { id: 3, description: "Bosch", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 17,
      description: "Prensa Hidráulica 20T",
      ModelId: 17,
      Model: { id: 17, description: "PH20", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 15,
      Manufacturer: { id: 15, description: "Stanley", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 18,
      description: "Guincho Elétrico 500kg",
      ModelId: 18,
      Model: { id: 18, description: "GE500", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 16,
      Manufacturer: { id: 16, description: "Tecnoflex", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 19,
      description: "Maçarico Oxiacetilênico",
      ModelId: 19,
      Model: { id: 19, description: "MOX", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 17,
      Manufacturer: { id: 17, description: "Harris", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 20,
      description: "Nivelador Laser 360°",
      ModelId: 20,
      Model: { id: 20, description: "NL360", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 18,
      Manufacturer: { id: 18, description: "Leica", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 21,
      description: "Caminhão Munck 6T",
      ModelId: 21,
      Model: { id: 21, description: "CM6", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 19,
      Manufacturer: { id: 19, description: "Mercedes", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 22,
      description: "Pá Carregadeira 1.5m³",
      ModelId: 22,
      Model: { id: 22, description: "PC15", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 20,
      Manufacturer: { id: 20, description: "Caterpillar", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 23,
      description: "Retroescavadeira 4x4",
      ModelId: 23,
      Model: { id: 23, description: "RET4", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 20,
      Manufacturer: { id: 20, description: "Caterpillar", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 24,
      description: "Rolo Compactador Pé-de-Carneiro",
      ModelId: 24,
      Model: { id: 24, description: "RPC", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 14,
      Manufacturer: { id: 14, description: "Bomag", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 25,
      description: "Placa Vibratória 80kg",
      ModelId: 25,
      Model: { id: 25, description: "PV80", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 7,
      Manufacturer: { id: 7, description: "Wacker Neuson", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 26,
      description: "Furadeira de Coluna 16mm",
      ModelId: 26,
      Model: { id: 26, description: "FC16", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 21,
      Manufacturer: { id: 21, description: "Delta", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 27,
      description: "Serra Tico-Tico 600W",
      ModelId: 27,
      Model: { id: 27, description: "ST600", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 4,
      Manufacturer: { id: 4, description: "Makita", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 28,
      description: "Misturador de Argamassa 120L",
      ModelId: 28,
      Model: { id: 28, description: "MA120", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 5,
      Manufacturer: { id: 5, description: "Vonder", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 29,
      description: "Carrinho de Mão Aço 80L",
      ModelId: 29,
      Model: { id: 29, description: "CM80", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 22,
      Manufacturer: { id: 22, description: "Fortg", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    },
    {
      id: 30,
      description: "Cortadora de Azulejo Elétrica",
      ModelId: 30,
      Model: { id: 30, description: "CAE", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      ManufacturerId: 23,
      Manufacturer: { id: 23, description: "Ryobi", companyId: 1, userId: 1, deleted: this.minDate, registered: new Date() },
      companyId: 1, userId: 1, deleted: this.minDate, registered: new Date()
    }
  ];

  getData(): Observable<EquipmentDto[]> {
    return of(this.equipments)
  }


}
