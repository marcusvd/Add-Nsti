// using System;
// using System.Globalization;
// using System.Linq;
// using System.Threading.Tasks;
// using Domain.Entities.System.Customers;
// using Microsoft.EntityFrameworkCore;
// using Pagination.Models;
// using UnitOfWork.Persistence.Operations;

// namespace Application.Services.Operations.Customers.Search
// {
//     public class CustomerSearchService : ICustomerSearchService
//     {
//         private readonly IUnitOfWork _GENERIC_REPO;
//         public CustomerSearchService(IUnitOfWork GENERIC_REPO)
//         {
//             _GENERIC_REPO = GENERIC_REPO;
//         }
//         private Page<Customer> fromDb = null;

//         public async Task<Page<Customer>> FilterList(Params parameters, FilterTerms filterTerms)
//         {

//             if (filterTerms.assured != "Selecione" && filterTerms.entity == "Selecione")
//             {
//                 DateTime assured;

//                 var assuredToDateTime = DateTime.TryParseExact(filterTerms.assured, "yyyy-MM-ddTHH:mm:ss.ffffff", CultureInfo.InvariantCulture, DateTimeStyles.None, out assured);
                

//                 if (assuredToDateTime)
//                 {
//                     fromDb = fromDb = await _GENERIC_REPO.Customers.GetPaged(
//                                                                                  parameters,
//                                                                                  predicate => predicate.CompanyId == parameters.predicate && predicate.Deleted == DateTime.MinValue,
//                                                                                  toInclude => toInclude.Include(x => x.Contact),
//                                                                                  selector => selector,
//                                                                                  orderBy => orderBy.OrderBy(x => x.Name)
//                                                                                 //   term => term.Assured == assured
//                                                                                  );
//                 }
//                 return fromDb;
//             }
//             if (filterTerms.assured == "Selecione" && filterTerms.entity != "Selecione")
//             {

//                 var entityTypeEnum = (EntityTypeEnum)int.Parse(filterTerms.entity);
//                 fromDb = fromDb = await _GENERIC_REPO.Customers.GetPaged(
//                                                                   parameters,
//                                                                   predicate => predicate.CompanyId == parameters.predicate && predicate.Deleted == DateTime.MinValue,
//                                                                   toInclude => toInclude.Include(x => x.Contact),
//                                                                   selector => selector,
//                                                                   orderBy => orderBy.OrderBy(x => x.Name),
//                                                                   term => term.EntityType == entityTypeEnum
//                                                                              );
//                 return fromDb;
//             }
//             if (filterTerms.assured != "Selecione" && filterTerms.entity != "Selecione")
//             {
//                 var assured = bool.Parse(filterTerms.assured);

//                 var entityTypeEnum = (EntityTypeEnum)int.Parse(filterTerms.entity);
//                 fromDb = fromDb = await _GENERIC_REPO.Customers.GetPaged(
//                                                                               parameters,
//                                                                               predicate => predicate.CompanyId == parameters.predicate && predicate.Deleted == DateTime.MinValue,
//                                                                               toInclude => toInclude.Include(x => x.Contact),
//                                                                               selector => selector,
//                                                                               orderBy => orderBy.OrderBy(x => x.Name)
//                                                                               //term => term.Assured == assured && term.EntityType == entityTypeEnum
//                                                                               );
//                 return fromDb;
//             }

//             return null;
//         }
        

//     }
// }