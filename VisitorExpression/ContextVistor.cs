using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace VisitorExpression
{
    public class ContextVistor : ExpressionVisitor
    {
        private readonly IEnumerable<string> properties;
        public IDictionary<string, object> Mapping { get; } = new Dictionary<string, object>();
        public bool NeedsContext { get; private set; }

        public ContextVistor(IEnumerable<string> properties)
        {
            this.properties = properties;
        }

        private bool CanidateForContext(Expression memberExpression, Expression valueExpression)
        {
            if ((memberExpression.NodeType != ExpressionType.MemberAccess) ||
                (valueExpression.NodeType != ExpressionType.Constant))
            {
                return false;
            }
            var member = ((MemberExpression) memberExpression).Member.Name;
            var value = ((ConstantExpression) valueExpression).Value;
            if (!properties.Contains(member))
            {
                return false;
            }
            NeedsContext = true;
            Mapping[member] = value;
            return true;
        }

        #region Overrides of ExpressionVisitor

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.AndAlso)
            {
                var left = Visit(node.Left);
                var right = Visit(node.Right);

                if ((left == null) && (right == null))
                {
                    return Expression.Constant(true);
                }

                if (left == null)
                {
                    return right;
                }

                if (right == null)
                {
                    return left;
                }
            }
            if (node.NodeType != ExpressionType.Equal)
            {
                return Visit(node);
            }
            var leftNode = Visit(node.Left);
            var rightNode = Visit(node.Right);
            if (CanidateForContext(leftNode, rightNode) || CanidateForContext(rightNode, leftNode))
            {
                return null;
            }
            return base.VisitBinary(node);
        }

        #endregion
    }
}
