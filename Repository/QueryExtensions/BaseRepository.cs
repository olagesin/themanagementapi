using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.QueryExtensions
{
    public static class BaseRepositoryExtension
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> data, int pagenumber, int pagesize) where T : class
        {
            return data
                .Skip((pagenumber - 1) * pagesize)
                .Take(pagesize);
        }
    }
}
