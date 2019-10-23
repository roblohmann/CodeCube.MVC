using System;
using System.Linq;
using System.Linq.Expressions;

namespace CodeCube.Mvc.Expressions
{
    /// <summary>
    /// Predicatebuilder class to construct predicates, used to construct expression trees.
    /// </summary>
    /// <remarks>See http://www.albahari.com/expressions for information and examples.</remarks>
    public static class PredicateBuilder
    {
        /// <summary>
        /// Shortcut to creating an Expression that initially evaluates to true.
        /// </summary>
        /// <typeparam name="T">The type to evaluate to true.</typeparam>
        /// <returns>true</returns>
        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        /// <summary>
        /// Shortcut to creating an Expression that initially evaluates to false.
        /// </summary>
        /// <typeparam name="T">The type to evaluate to false.</typeparam>
        /// <returns>false.</returns>
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        /// <summary>
        /// Construct and OR axpression to check wether the specified object of type T matches on of the both provided expressions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        /// <summary>
        /// Construct and AND axpression to check wether the specified object of type T matches both expressions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}
