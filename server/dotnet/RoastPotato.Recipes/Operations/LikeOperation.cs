using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RoastPotato.Recipes.Operations
{
    [Operation( "lk" )]
    public class LikeOperation : Operation
    {      
        public override Expression<Func<TEntity, bool>> Do<TEntity>(object value)
        {
            if ( !typeof( string ).IsAssignableFrom( Property.Type ) )
                throw new ArgumentException( string.Format( "Invalid use of Like Operation with {0}", Property.Member.Name ) );

            MethodInfo methodToCall = typeof( string ).GetMethod( "Contains" );

            return
                Expression.Lambda<Func<TEntity, bool>>(
                    Expression.Call( Property, methodToCall, Expression.Constant( value, typeof( string ) ) ), Param );
        }

        public override dynamic GetCorrectParameterList(string[ ] collection)
        {
            return collection.First();
        }
    }
}