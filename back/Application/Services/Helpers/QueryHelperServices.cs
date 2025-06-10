using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Main.Customers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Application.Services.Helpers
{
    public static class QueryHelperServices
    {
     
        // Função auxiliar para aplicar as inclusões dinâmicas
        public static IQueryable<Customer> ApplyIncludes(IQueryable<Customer> query, string[] includeProperties)
        {
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return query;
        }

        // Função auxiliar para obter a propriedade de ordenação dinâmica
        public static Expression<Func<Customer, object>> GetProperty(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(Customer), "x");
            var property = Expression.Property(parameter, propertyName);
            return Expression.Lambda<Func<Customer, object>>(Expression.Convert(property, typeof(object)), parameter);
        }
    }
}