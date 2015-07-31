using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VisitorExpression
{
    public interface IRepository
    {
        IQueryable<T> List<T>(Expression<Func<T, bool>> filter) where T : class;
    }
}
