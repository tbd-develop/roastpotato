using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RoastPotato.Recipes.Operations
{
    [Operation( "in" )]
    public class InOperation : Operation
    {
        private const string CollectionMethod = "In";

        public override Expression<Func<TEntity, bool>> Do<TEntity>(object collection)
        {
            if ( !( collection is IEnumerable ) )
                throw new ArgumentException( "You can not perform an 'In' operation given the type " + collection.GetType( ) );

            MethodInfo genericMethod = GetType( ).GetMethod( CollectionMethod, BindingFlags.Instance | BindingFlags.NonPublic );

            MethodInfo methodToCall = genericMethod.MakeGenericMethod( new[ ] { typeof( TEntity ), Property.Type } );

            dynamic collectionToUse = CastCollectionToCorrectType( collection );

            return ( Expression<Func<TEntity, bool>> )methodToCall.Invoke( this, new[ ] { collectionToUse } );
        }     

        private Expression<Func<TEntity, bool>> In<TEntity, TList>(IEnumerable<TList> list)
        {
            Expression conversionExpression = Expression.Convert( Property, typeof( TList ) );

            Expression<Func<TEntity, TList>> propertyExpression =
                Expression.Lambda<Func<TEntity, TList>>( conversionExpression, Param );

            Expression<Func<IEnumerable<TList>, TList, bool>> methodExpression = (x, y) => x.Contains( y );

            ReadOnlyCollection<ParameterExpression> parameters = propertyExpression.Parameters;

            MethodCallExpression methodBody = methodExpression.Body as MethodCallExpression;
            MethodCallExpression methodCall = Expression.Call( methodBody.Method, Expression.Constant( list ),
                                                              conversionExpression );

            return Expression.Lambda<Func<TEntity, bool>>( methodCall, parameters );
        }
    }
}