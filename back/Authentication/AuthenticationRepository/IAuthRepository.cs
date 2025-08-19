
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Authentication.AuthenticationRepository;

public interface IAuthRepository<T> where T : class
{

    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> GetCount(Expression<Func<T, bool>> predicate);

    Task<bool> SaveAsync();

    IQueryable<T> Get(
                                Expression<Func<T, bool>> predicate,
                                Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
                                Expression<Func<T, T>> selector,
                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
                                Expression<Func<T, bool>> termPredicate,
                                bool disableTracking = true
        );

    Task<T> GetByPredicate(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Expression<Func<T, T>> selector, Func<IQueryable<T>, IOrderedQueryable<T>> ordeBy, bool disableTracking = true);

}