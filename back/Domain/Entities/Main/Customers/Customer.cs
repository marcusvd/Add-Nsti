using Domain.Entities.Main.Inheritances;
using Domain.Entities.Main.Inheritances.Enums;
using System;
using Domain.Entities.Shared;
using System.Collections.Generic;
namespace Domain.Entities.Main.Customers;

public class Customer : RootBase
{
    public string Name { get; set; }
    public string TradeName { get; set; }
    public string CNPJ { get; set; }
    public EntityTypeEnum EntityType { get; set; }
    public string Description { get; set; }
    public Address Address { get; set; }
    public Contact Contact { get; set; }

}