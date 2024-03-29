using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using RoastPotato.Recipes.Operations;

namespace RoastPotato.Recipes.Infrastructure
{
    public static class RecipeExtensions
    {
        private const string ExpressionRegEx =
            @"(?<Field>[\w._]*)\s(?<Operation>\w{0,3})\s(?:'(?<Value>[A-Za-z0-9\s/:.]+)'[,]?)+$";

        public static Expression<Func<TEntity, bool>> AsExpressionOf<TEntity>(this string expression)
        {
            expression = expression.Trim( );

            var matches = Regex.Match( expression, ExpressionRegEx );

            if ( matches.Success )
            {
                string field = matches.Groups[ "Field" ].Value;
                string op = matches.Groups[ "Operation" ].Value;
                string[ ] values = matches.Groups[ "Value" ].Captures.Cast<Capture>( ).Select( c => c.Value ).ToArray( );

                ParameterExpression param = Expression.Parameter( typeof( TEntity ) );

                var property = field.GetPropertyExpression<TEntity>( param );

                Operation operation = Operation.Get( op ).For( param, property );

                return operation.Do<TEntity>( operation.GetCorrectParameterList( values ) );
            }

            return null;
        }

        public static MemberExpression GetPropertyExpression<TEntity>(this string field, ParameterExpression param)
        {
            string[ ] properties = field.Split( '.' );
            var propertyName = properties.Last( );
            MemberExpression result;

            if ( properties.Count( ) > 1 )
            {
                result = Expression.Property( param, properties.First( ) );

                if ( properties.Count( ) > 2 )
                {
                    result = properties.Skip( 1 ).Reverse( ).Aggregate( result, Expression.Property );
                }

                result = Expression.Property( result, propertyName );
            }
            else
                result = Expression.Property( param, typeof( TEntity ).GetProperty( propertyName ) );

            return result;
        }

        public static PropertyInfo GetPropertyInfo<TEntity>(this string field)
        {
            string[ ] properties = field.Split( '.' );
            PropertyInfo result;

            if ( properties.Count( ) > 1 )
            {
                var propertyName = properties.Last( );

                PropertyInfo association = typeof( TEntity ).GetProperty( properties.First( ) );

                if ( properties.Count( ) > 2 )
                {
                    association = properties.Skip( 1 ).Reverse( ).Aggregate( association,
                                                                             (current, assoc) =>
                                                                             current.PropertyType.GetProperty( assoc ) );
                }

                result = association.PropertyType.GetProperty( propertyName );
            }
            else
            {
                result = typeof( TEntity ).GetProperty( field );
            }

            return result;
        }

        /*
         * Credit: http://stackoverflow.com/users/390278/jeff-mercado
         * http://stackoverflow.com/questions/5744764/linq-to-sql-throwing-a-stackoverflowexception/5751931#5751931
         * */

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var param = expression1.Parameters.Single( );
            var left = expression1.Body;
            var right = ParameterRebinder.Rebind( expression2.Body, expression2.Parameters.Single( ), param );
            var body = Expression.AndAlso( left, right );
            return Expression.Lambda<Func<T, bool>>( body, param );
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var param = expression1.Parameters.Single( );
            var left = expression1.Body;
            var right = ParameterRebinder.Rebind( expression2.Body, expression2.Parameters.Single( ), param );
            var body = Expression.OrElse( left, right );
            return Expression.Lambda<Func<T, bool>>( body, param );
        }
    }
}