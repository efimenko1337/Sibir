using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sibir.DAL.Repositories.Shared
{
    internal static partial class EfExtensions
    {
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var visitor = new ReplaceParameterVisitor();
            visitor.Add(expr1.Parameters[0], parameter);
            visitor.Add(expr2.Parameters[0], parameter);

            var combined = visitor.Visit(Expression.AndAlso(expr1.Body, expr2.Body));

            return Expression.Lambda<Func<T, bool>>(combined, parameter);
        }

        private class ReplaceParameterVisitor : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> _map = [];

            public void Add(ParameterExpression from, ParameterExpression to)
            {
                _map[from] = to;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (_map.TryGetValue(node, out var replacement))
                {
                    node = replacement;
                }
                return base.VisitParameter(node);
            }
        }
    }
}
