using System;
using Application.Services.Operations.Main.Inheritances;

using Domain.Entities.Main.Customers;

namespace Application.Services.Operations.Main.Customers.Dtos
{
    public class CustomerDto : MainEntitiesBaseDto
    {
        public DateTime Assured { get; set; }
        public decimal Payment { get; set; }
        public DateTime Expires { get; set; }
        public decimal Discount { get; set; }
        public AdditionalCostsDto AdditionalCosts { get; set; }
        
    }
}