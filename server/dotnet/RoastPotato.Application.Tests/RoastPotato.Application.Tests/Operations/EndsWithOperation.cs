using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RoastPotato.Application.Tests.Operations
{
    [Operation( "ew" )]
    public class EndsWithOperation : Operation
    {
        public override Expression<Func<TEntity, bool>> Do<TEntity>(object value)
        {
            if ( !typeof( string ).IsAssignableFrom( Property.Type ) )
                throw new ArgumentException( string.Format( "Invalid use of EndsWith Operation with {0}", Property.Member.Name ) );

            MethodInfo methodToCall = typeof( string ).GetMethod( "EndsWith", new[ ] { typeof( string ) } );

            return
                Expression.Lambda<Func<TEntity, bool>>(
                    Expression.Call( Property, methodToCall, Expression.Constant( value, typeof( string ) ) ), Param );
        }

        public override dynamic GetCorrectParameterList(string[ ] collection)
        {
            return collection.First( );
        }
    }
}