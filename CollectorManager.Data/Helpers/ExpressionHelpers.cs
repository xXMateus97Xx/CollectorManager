using System.Linq.Expressions;

namespace CollectorManager.Data.Helpers;

public static class ExpressionHelpers
{
    public static Expression<Func<T, bool>> CombineAnd<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var parameter = Expression.Parameter(typeof(T));

        var leftVisitor = new ReplaceExpressionVisitor(left.Parameters[0], parameter);
        var leftExpression = leftVisitor.Visit(left.Body);

        var rightVisitor = new ReplaceExpressionVisitor(right.Parameters[0], parameter);
        var rightExpression = rightVisitor.Visit(right.Body);

        return Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(leftExpression, rightExpression), parameter);
    }

    class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression? node)
        {
            if (node == _oldValue)
                return _newValue;

            return base.Visit(node) ?? throw new ArgumentNullException(nameof(node));
        }
    }
}
