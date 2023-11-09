using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        protected internal RepositoryContext RepositoryContext;
        public RepositoryBase(RepositoryContext repositoryContext) => RepositoryContext = repositoryContext;

        public IQueryable<T> ListAll(bool trackChanges = true) => !trackChanges ? RepositoryContext.Set<T>()
            .AsNoTracking() : RepositoryContext.Set<T>();

        public IQueryable<T> ListByCondition(Expression<Func<T, bool>> expression, bool trackChanges) => !trackChanges ? RepositoryContext.Set<T>()
            .Where(expression)
            .AsNoTracking() :
            RepositoryContext.Set<T>()
            .Where(expression);

        public async Task<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) => !trackChanges ? await RepositoryContext.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(expression) :
            await RepositoryContext.Set<T>()
            .FirstOrDefaultAsync(expression);

        public async Task CreateAsync(T entity)
        {
            await RepositoryContext.Set<T>().AddAsync(entity);
        }

        public async Task CreateMultipleAsync(List<T> entities)
        {
             await RepositoryContext.Set<T>().AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }
    }
}
