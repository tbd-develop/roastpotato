using System;
using System.Linq;
using System.Linq.Expressions;

namespace RoastPotato.Recipes.Operations
{
    [Operation( "bt" )]
    public class BetweenOperation : Operation
    {
        public override Expression<Func<TEntity, bool>> Do<TEntity>(object collection)
        {
            dynamic collectionToUse = CastCollectionToCorrectType( collection );

            var greaterThanValue = Expression.Constant( Convert.ChangeType( collectionToUse[ 0 ], Property.Type ) );
            var lessThanValue = Expression.Constant( Convert.ChangeType( collectionToUse[ 1 ], Property.Type ) );

            return
                Expression.Lambda<Func<TEntity, bool>>(
                    Expression.And( Expression.GreaterThanOrEqual( Property, greaterThanValue ),
                                  Expression.LessThanOrEqual( Property, lessThanValue ) ), Param );
        }

        public override dynamic GetCorrectParameterList(string[ ] collection)
        {
            return collection.Take( 2 ).ToArray( );
        }
    }
}