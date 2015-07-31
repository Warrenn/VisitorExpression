using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorExpression
{
    public class RepositoryFactory : IRepositoryFactory
    {
        #region Implementation of IRepositoryFactory

        public IRepository Create()
        {
            return new Repository(new DataContext());
        }

        #endregion
    }
}
