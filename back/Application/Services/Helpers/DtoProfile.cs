// using AutoMapper;
// using Domain.Entities.Shared;
// 
// using Domain.Entities.Authentication;
// using Application.Services.Operations.Outsourced.Dtos;
// using Domain.Entities.ServicesBench;
// using Application.Services.Operations.BenchBudgetService.Dtos;
// using Domain.Entities.Main;
// using Domain.Entities.Companies;
// using Domain.Entities.Customers;
// using Application.Services.Operations.Main.Customers.Dtos;
// using Domain.Entities.Main.Inheritances;
// using Application.Services.Operations.Main.Partners.Dtos;
// using Application.Services.Operations.Main.Companies.Dtos;
// using Application.Services.Operations.Authentication.Dtos;
// using Application.Services.Shared.Dtos;
// using Application.Services.Operations.BenchBudgetService.Dtos.Enums;
// using Domain.Entities.ServicesBench.Enums;
// using Application.Services.Operations.Finances.Dtos.Enums;
// using Domain.Entities.Finances.Enums;
// using Application.Services.Operations.Outsourced.Dtos.Enums;
// using Domain.Entities.Outsourced.Enums;
// using Domain.Entities.Main.Partners;
// using Application.Services.Operations.Main.Inheritances;
// using Application.Services.Operations.Main.Inheritances.Enums;
// using Domain.Entities.Main.Inheritances.Enums;
// using Domain.Entities.Finances.MonthlyExpenses;
// using Domain.Entities.Finances.CategorySubcategoryExpenses;
// using Application.Services.Operations.Finances.Dtos.CategorySubcategoryExpenses;
// using Domain.Entities.Finances.YearlyExpenses;
// using Domain.Entities.Finances.VariablesDebitsExpenses;
// using Application.Services.Operations.Finances.Dtos.MonthlyExpenses;
// using Application.Services.Operations.Finances.Dtos.YearlyExpenses;
// using Application.Services.Operations.Finances.Dtos.VariableDebitExpenses;
// using Domain.Entities.Finances.CreditCardExpenses;
// using Application.Services.Operations.Finances.Dtos.CreditCardExpenses;
// using Domain.Entities.Finances.PixExpenses;
// using Application.Services.Operations.Finances.Dtos.PixExpenses;
// using Domain.Entities.StockProduct.ProductKind;
// using Application.Services.Operations.StockProduct.ProductKind;

// namespace Application.Services.Helpers
// {
//     public class DtoProfile : Profile
//     {
//         public DtoProfile()
//         {
//             #region Main
//             CreateMap<MainEntitiesBaseDto,MainEntitiesBase>().ReverseMap();
//             CreateMap<Partner, PartnerDto>().ReverseMap();
//             CreateMap<PaymentData, PaymentDataDto>().ReverseMap();
//             CreateMap<PartnerPaymentPix, PartnerPaymentPixDto>().ReverseMap();
//             CreateMap<PartnerPaymentBankAccount, PartnerPaymentBankAccountDto>().ReverseMap();

//             CreateMap<Customer, CustomerDto>().ReverseMap();
//             CreateMap<AdditionalCosts, AdditionalCostsDto>().ReverseMap();
//             CreateMap<PhysicallyMovingCosts, PhysicallyMovingCostsDto>().ReverseMap();
//             #endregion

//             CreateMap<EntityTypeEnum, EntityTypeEnumDto>().ReverseMap();

//             #region Authentication
//             CreateMap<MyUser, MyUserDto>().ReverseMap();
//             #endregion
//             #region BudgetServicesBench
//             CreateMap<BudgetService, BudgetServiceDto>().ReverseMap();
//             CreateMap<BudgetServiceDto, BudgetService>().ReverseMap();
//             CreateMap<Service, ServiceDto>().ReverseMap();
//             CreateMap<Repair, RepairDto>().ReverseMap();

//             CreateMap<CollectDeliverCosts, CollectDeliverCostsDto>().ReverseMap();
//             CreateMap<CostFromEnum, CostFromEnumDto>().ReverseMap();
//             CreateMap<StatusServiceEnum, StatusServiceEnumDto>().ReverseMap();
//             CreateMap<ExecutionModeEnum, ExecutionModeEnumDto>().ReverseMap();
//             CreateMap<TableProvidedServicePrice, TableProvidedServicePriceDto>().ReverseMap();
//             #endregion
            
//             #region Financial
//             // CreateMap<Card, CardDto>().ReverseMap();
//             // CreateMap<CreditCardLimitOperation, CreditCardLimitOperationDto>().ReverseMap();
//             // CreateMap<Pix, PixDto>().ReverseMap();
//             // CreateMap<BankAccount, BankAccountDto>().ReverseMap();
            
//             CreateMap<CategoryExpense, CategoryExpenseDto>().ReverseMap();
//             CreateMap<SubcategoryExpense, SubcategoryExpenseDto>().ReverseMap();

//             CreateMap<MonthlyFixedExpense, MonthlyFixedExpenseDto>().ReverseMap();
//             CreateMap<MonthlyFixedExpense, MonthlyFixedExpensePaymentDto>().ReverseMap();
          
//             CreateMap<YearlyFixedExpense, YearlyFixedExpenseDto>().ReverseMap();
//             CreateMap<YearlyFixedExpense, YearlyFixedExpensePaymentDto>().ReverseMap();
//             // CreateMap<YearlyFixedExpenseTracking, YearlyFixedExpenseTrackingDto>().ReverseMap();
            
//             CreateMap<VariableExpense, VariableExpenseDto>().ReverseMap();
//             // .ForMember(dest => dest.Card, act => act.Ignore())
//             // .ForMember(dest => dest.User, act => act.Ignore())
//             // .ForMember(dest => dest.Company, act => act.Ignore())
//             // .ForMember(dest => dest.BankAccount, act => act.Ignore())
//             // .ForMember(dest => dest.CategoryExpense, act => act.Ignore())
//             // .ForMember(dest => dest.SubcategoryExpense, act => act.Ignore());


            
//             CreateMap<CreditCardExpense, CreditCardExpenseDto>().ReverseMap();
//             CreateMap<CreditCardExpenseInvoice, CreditCardExpenseInvoiceDto>().ReverseMap();
//             CreateMap<PixExpense, PixExpenseDto>().ReverseMap();
//             //Enums

//             CreateMap<TypeAccountEnum, TypeAccountEnumDto>().ReverseMap();
//             CreateMap<TypeCardEnum, TypeCardEnumDto>().ReverseMap();
//             #endregion
//             #region Shared
//             CreateMap<Address, AddressDto>().ReverseMap();
//             CreateMap<Contact, ContactDto>().ReverseMap();
//             CreateMap<SocialNetwork, SocialNetworkDto>().ReverseMap();
//             #endregion
//             #region OutSourced
//             CreateMap<CollectDeliver, CollectDeliverDto>().ReverseMap();
           
//             CreateMap<CollectDeliver, CollectDeliverUpdateDto>().ReverseMap()
//             .ForMember(dest => dest.Deliver, act => act.Ignore())
//             .ForMember(coll => coll.Collect, act => act.Ignore())
//             .ForMember(other => other.Other, act => act.Ignore());


//             CreateMap<BillingFrom, BillingFromDto>().ReverseMap();
//             CreateMap<Destiny, DestinyDto>().ReverseMap();
//             CreateMap<ElectronicRepair, ElectronicRepairDto>().ReverseMap();
//             CreateMap<StatusServiceEletronicReparEnum, StatusServiceEletronicRepairEnumDto>().ReverseMap();
//             #endregion
//             #region Company
//             CreateMap<Company, CompanyDto>().ReverseMap();
//             #endregion

//              #region Product
//             //  CreateMap<ProductType, ProductTypeDto>().ReverseMap();
//              CreateMap<Manufacturer, ManufacturerDto>().ReverseMap();
//              CreateMap<Segment, SegmentDto>().ReverseMap();
//              CreateMap<Model, ModelDto>().ReverseMap();
//             // CreateMap<Item, ItemDto>().ReverseMap();
//             // CreateMap<Product, ProductDto>().ReverseMap();
//             // CreateMap<Quantity, QuantityDto>().ReverseMap();
//             // CreateMap<Tracking, TrackingDto>().ReverseMap();
//             // CreateMap<Manufacturer, ManufacturerDto>().ReverseMap();
//             // CreateMap<Segment, SegmentDto>().ReverseMap();
//             // CreateMap<Model, ModelDto>().ReverseMap();
//              #endregion

//         }
//     }
// }


