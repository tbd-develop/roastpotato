using System;
using System.Linq;
using System.Linq.Expressions;

namespace RoastPotato.Recipes.Operations
{
    [Operation( "eq" )]
    public class EqualOperation : Operation
    {
        public override Expression<Func<TEntity, bool>> Do<TEntity>(object value)
        {
            var expressionValue = Expression.Constant( Convert.ChangeType( value, Property.Type ) );

            return Expression.Lambda<Func<TEntity, bool>>( Expression.Equal( Property, expressionValue ), Param );
        }

        public override dynamic GetCorrectParameterList(string[ ] collection)
        {
            return collection.First( );
        }
    }
}