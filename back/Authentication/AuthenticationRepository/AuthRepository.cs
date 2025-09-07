using System.Linq.Expressions;
using Authentication.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;


namespace Authentication.AuthenticationRepository;

public class AuthRepository<T> : IAuthRepository<T> where T : class
{
    private readonly IdImDbContext _dbContext;

    public AuthRepository(IdImDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(T entity)
    {
        _dbContext.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public IQueryable<T> Get(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Expression<Func<T, T>> selector = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Expression<Func<T, bool>> termPredicate = null, bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();
        if (disableTracking)
            query = query.AsNoTracking();

        if (termPredicate != null)
            query = query.Where(termPredicate);

        if (predicate != null)
            query = query.Where(predicate);

        if (include != null)
            query = include(query);

        if (orderBy != null)
            return query = orderBy(query).Select(selector);

        return query;
    }

    public async Task<T> GetByPredicate(System.Linq.Expressions.Expression<Func<T, bool>> predicate, Func<IQueryable<T>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>> include, System.Linq.Expressions.Expression<Func<T, T>> selector, Func<IQueryable<T>, IOrderedQueryable<T>> ordeBy, bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (disableTracking)
            query = query.AsNoTracking();

        if (predicate != null)
            query = query.Where(predicate);

        if (include != null)
            query = include(query).AsSplitQuery();


        return ordeBy != null ? await ordeBy(query).Select(selector).SingleOrDefaultAsync() : await query.Select(selector).SingleOrDefaultAsync();

    }

    public async Task<int> GetCount(System.Linq.Expressions.Expression<Func<T, bool>> predicate) => await _dbContext.Set<T>().Where<T>(x => x == predicate).AsNoTracking().CountAsync();


    public void Update(T entity)
    {
        _dbContext.Entry(entity).CurrentValues.SetValues(entity);
        _dbContext.Set<T>().Update(entity);
    }

    // public async Task<bool> SaveAsync() =>  await _dbContext.SaveChangesAsync() > 0 ? true : false;
}