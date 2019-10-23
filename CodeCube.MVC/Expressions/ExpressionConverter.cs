using System;
using System.Linq;
using System.Linq.Expressions;

namespace CodeCube.Mvc.Expressions
{
    /// <summary>
    /// Class to convert type in lambda expression to another desired type.
    /// </summary>
    /// <see cref="http://stackoverflow.com/questions/4601844/expression-tree-copy-or-convert"/>
    /// <typeparam name="TTo">The type to convert to.</typeparam>
    public class ExpressionConverter<TTo>
    {
        class ConversionVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _newParameter;
            private readonly ParameterExpression _oldParameter;

            public ConversionVisitor(ParameterExpression newParameter, ParameterExpression oldParameter)
            {
                _newParameter = newParameter;
                _oldParameter = oldParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return _newParameter; // replace all old param references with new ones
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                if (node.Expression != _oldParameter) // if instance is not old parameter - do nothing
                    return base.VisitMember(node);

                var newObj = Visit(node.Expression);
                var newMember = _newParameter.Type.GetMember(node.Member.Name).First();
                return Expression.MakeMemberAccess(newObj, newMember);
            }
        }

        /// <summary>
        /// Convert an labmda expression to a lambda expression of a different type.
        /// </summary>
        /// <typeparam name="TFrom">The object to convert from.</typeparam>
        /// <typeparam name="TOut">The object to convert to.</typeparam>
        /// <param name="expression">The expression to convert.</param>
        /// <example>ExpressionConverter<MyObject>.Convert(expression)</example>
        /// <returns>The expression created with the new type.</returns>
        public static Expression<Func<TTo, TOut>> Convert<TFrom, TOut>(Expression<Func<TFrom, TOut>> expression)
        {
            var oldParameter = expression.Parameters[0];
            var newParameter = Expression.Parameter(typeof(TTo), oldParameter.Name);
            var converter = new ConversionVisitor(newParameter, oldParameter);
            var newBody = converter.Visit(expression.Body);
            return Expression.Lambda<Func<TTo, TOut>>(newBody, newParameter);
        }
    }
}
