using System;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisitorExpression.Models;

namespace VisitorExpression
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void VisitorMustVistProperty()
        {
            Expression<Func<Model1, bool>> expr = m => m.SpecialKey2 == 2 && m.SpecialKey == 4;

            var treeModifier = new ContextVistor(new[] {"SpecialKey2", "SpecialKey3"});
            var modifiedExpr = treeModifier.Visit(expr);

            Assert.IsTrue(treeModifier.NeedsContext);
            Assert.AreEqual(2, treeModifier.Mapping["SpecialKey2"]);
        }

        [TestMethod]
        public void QuickExample()
        {
            IRepositoryFactory factory = new RepositoryFactory();

            using (var repository = (IDisposable)factory.Create())
            {
                ((IRepository) repository).List<Model1>(m => m.SpecialKey2 == 2 && m.SpecialKey == 4);
            }
        }
    }
}
