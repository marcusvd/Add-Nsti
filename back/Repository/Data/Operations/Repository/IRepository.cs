using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Pagination.Models;
//using Pagination;

namespace Repository.Data.Operations.Repository;

    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> GetCount(Expression<Func<T, bool>> predicate);
        IQueryable<T> Get(
                                Expression<Func<T, bool>> predicate = null,
                                Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                Expression<Func<T, T>> selector = null,
                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                Expression<Func<T, bool>> termPredicate = null,
                                bool disableTracking = true
        );
        Task<T> GetById(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Expression<Func<T, T>> selector = null, Func<IQueryable<T>, IOrderedQueryable<T>> ordeBy = null, bool disableTracking = true);
        Task<Page<T>> GetPaged(
             Params parameters,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Expression<Func<T, T>> selector = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Expression<Func<T, bool>> termPredicate = null,
            bool noTraking = true
        );
    }