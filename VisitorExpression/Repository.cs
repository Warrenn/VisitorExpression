using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VisitorExpression.Models;

namespace VisitorExpression
{
    public class Repository : IRepository, IDisposable
    {
        private readonly DataContext dbContext;

        private static IDictionary<Type, IEnumerable<string>> mapping = new Dictionary<Type, IEnumerable<string>>
        {
            {typeof (Model1), new[] {"SpecialKey", "SpecialKey2"}}
        };

        public Repository(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region Implementation of IRepository

        public IQueryable<T> List<T>(Expression<Func<T, bool>> filter) where T : class
        {
            if (!mapping.ContainsKey(typeof(T)))
            {
                return dbContext.Set<T>().Where(filter);
            }
            var treeModifier = new ContextVistor(mapping[typeof(T)]);
            filter = (Expression<Func<T, bool>>)treeModifier.Visit(filter);
            if (treeModifier.NeedsContext)
            {
                dbContext.SetDatabaseContext(treeModifier.Mapping);
            }
            return dbContext.Set<T>().Where(filter);
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            dbContext.Dispose();
        }

        #endregion
    }
}
