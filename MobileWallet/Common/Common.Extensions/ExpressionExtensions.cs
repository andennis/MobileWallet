using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Common.Extensions
{
    public static class ExpressionExtensions
    {
        public static string GetMethodOrPropertyName<T, TReturn>(this Expression<Func<T, TReturn>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member != null)
            {
                var pi = member.Member as PropertyInfo;
                if (pi == null)
                    throw new ArgumentException("Expression is not a property", "expression");

                return member.Member.Name;
            }

            var method = expression.Body as MethodCallExpression;
            if (method != null)
                return method.Method.Name;

            throw new ArgumentException("Expression is not a property or a method", "expression");
        }

        public static string GetMethodOrPropertyName<T>(this Expression<Action<T>> expression)
        {
            var method = expression.Body as MethodCallExpression;
            if (method == null)
                throw new ArgumentException("Expression is not a method", "expression");

            return method.Method.Name;
        }

    }
}
