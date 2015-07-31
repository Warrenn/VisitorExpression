using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorExpression.Models;

namespace VisitorExpression
{
    public class DataContext : DbContext
    {
        public DbSet<Model1> Models { get; set; }

        public void SetDatabaseContext(IDictionary<string, object> contextValues)
        {
            //yeah I don't know what would need to happen here
        }

        #region Overrides of DbContext

        protected override void Dispose(bool disposing)
        {
            //do something special here with the dispose
            base.Dispose(disposing);
        }

        #endregion
    }
}
