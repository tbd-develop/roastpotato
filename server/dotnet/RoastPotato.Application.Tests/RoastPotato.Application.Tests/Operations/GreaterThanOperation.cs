using System;
using System.Linq;
using System.Linq.Expressions;

namespace RoastPotato.Application.Tests.Operations
{
    [Operation( "gt", ValidOnTypes = new[ ] { typeof( int ), typeof( decimal ), typeof( double ), typeof( float ) } )]
    public class GreaterThanOperation : Operation
    {
        public override Expression<Func<TEntity, bool>> Do<TEntity>(object value)
        {
            var expressionValue = Expression.Constant( Convert.ChangeType( value, Property.Type ) );

            return Expression.Lambda<Func<TEntity, bool>>( Expression.GreaterThan( Property, expressionValue ), Param );
        }

        public override dynamic GetCorrectParameterList(string[ ] collection)
        {
            return collection.First( );
        }
    }

    [Operation( "gte", ValidOnTypes = new[ ] { typeof( int ), typeof( decimal ), typeof( double ), typeof( float ) } )]
    public class GreaterThanOrEqualOperation : Operation
    {
        public override Expression<Func<TEntity, bool>> Do<TEntity>(object value)
        {
            var expressionValue = Expression.Constant( Convert.ChangeType( value, Property.Type ) );

            return Expression.Lambda<Func<TEntity, bool>>( Expression.GreaterThanOrEqual( Property, expressionValue ), Param );
        }

        public override dynamic GetCorrectParameterList(string[ ] collection)
        {
            return collection.First( );
        }
    }
}