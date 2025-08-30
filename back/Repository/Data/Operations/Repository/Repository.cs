using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System;
using Repository.Data.Context;
using Microsoft.EntityFrameworkCore.Query;
using Pagination.Models;
using System.Collections.Generic;

namespace Repository.Data.Operations.Repository;
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ImSystemDbContext _CONTEXT;
        public Repository(ImSystemDbContext CONTEXT)
        {
            _CONTEXT = CONTEXT;
        }
        public void Add(T entity)
        {
            _CONTEXT.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _CONTEXT.Entry(entity).CurrentValues.SetValues(entity);
            _CONTEXT.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _CONTEXT.Set<T>().Remove(entity);
        }
        public async Task<int> GetCount(Expression<Func<T, bool>> predicate)
        {
            return await _CONTEXT.Set<T>().Where(predicate).AsNoTracking().CountAsync();
        }
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Expression<Func<T, T>> selector = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Expression<Func<T, bool>> termPredicate = null, bool disableTracking = true)
        {
            IQueryable<T> query = _CONTEXT.Set<T>();

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
        public virtual async Task<T> GetByPredicate(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Expression<Func<T, T>> selector = null, Func<IQueryable<T>, IOrderedQueryable<T>> ordeBy = null, bool disableTracking = true)
        {

            IQueryable<T> query = _CONTEXT.Set<T>();

            if (disableTracking)
                query = query.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (include != null)
                query = include(query);

            if (ordeBy != null)
                return await ordeBy(query).Select(selector).SingleOrDefaultAsync() ?? null;
            else
                return await query.Select(selector).SingleOrDefaultAsync() ?? null;


        }
        public async Task<Page<T>> GetPaged(Params parameters, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Expression<Func<T, T>> selector = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Expression<Func<T, bool>> termPredicate = null, bool noTraking = true)
        {
            IQueryable<T> query = _CONTEXT.Set<T>();
            if (noTraking)
                query = query.AsNoTracking();

            if (termPredicate != null)
                query = query.Where(termPredicate);

            if (predicate != null)
                query = query.Where(predicate);

            if (include != null)
                query = include(query);

            if (orderBy != null)
                query = orderBy(query).Select(selector);

            return await Page<T>.ToPagedList(query, parameters.PgNumber, parameters.PgSize, selector);

        }

    }