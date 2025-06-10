using System.Text.Json;
using Microsoft.AspNetCore.Http;
namespace Pagination.Models
{
    public static class Pagination
    {

        public static void AddPagination(this HttpResponse response,
                                          int CurrentPg,
                                          int TotalPgs,
                                          int PgSize,
                                          int TotalCount,
                                          bool HasPrevious,
                                          bool HasNext
                                          )

        {
            var pagination = new PaginationHeader(CurrentPg,
                                                   TotalPgs,
                                                   PgSize,
                                                   TotalCount,
                                                   HasPrevious,
                                                   HasNext
                                                   );

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            response.Headers.Add("Pagination", JsonSerializer.Serialize(pagination, options));

            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }




    }
}