using System;
using System.Linq;
using System.Linq.Expressions;

namespace RoastPotato.Recipes.Operations
{
    [Operation( "lt" )]
    public class LessThanOperation : Operation
    {
        public override Expression<Func<TEntity, bool>> Do<TEntity>(object value)
        {
            var expressionValue = Expression.Constant( Convert.ChangeType( value, Property.Type ) );

            return Expression.Lambda<Func<TEntity, bool>>( Expression.LessThan( Property, expressionValue ), Param );
        }

        public override dynamic GetCorrectParameterList(string[ ] collection)
        {
            return collection.First( );
        }
    }

    [Operation( "lte" )]
    public class LessThanOrEqualOperation : Operation
    {
        public override Expression<Func<TEntity, bool>> Do<TEntity>(object value)
        {
            var expressionValue = Expression.Constant( Convert.ChangeType( value, Property.Type ) );

            return Expression.Lambda<Func<TEntity, bool>>( Expression.LessThanOrEqual( Property, expressionValue ), Param );
        }

        public override dynamic GetCorrectParameterList(string[ ] collection)
        {
            return collection.First( );
        }
    }
}