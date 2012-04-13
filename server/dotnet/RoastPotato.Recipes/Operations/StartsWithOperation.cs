using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace RoastPotato.Recipes.Operations
{
    [Operation( "sw" )]
    public class StartsWithOperation : Operation
    {
        public override Expression<Func<TEntity, bool>> Do<TEntity>(object value)
        {
            if ( !typeof( string ).IsAssignableFrom( Property.Type ) )
                throw new ArgumentException( string.Format( "Invalid use of StartsWith Operation with {0}", Property.Member.Name ) );

            MethodInfo methodToCall = typeof( string ).GetMethod( "StartsWith", new[ ] { typeof( string ) } );

            return
                Expression.Lambda<Func<TEntity, bool>>(
                    Expression.Call( Property, methodToCall, Expression.Constant( value ) ), Param );
        }

        public override dynamic GetCorrectParameterList(string[ ] collection)
        {
            return collection.First( );
        }
    }
}