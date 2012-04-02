using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RoastPotato.Application.Tests.Operations
{
    public abstract class Operation
    {
        protected ParameterExpression Param;
        protected MemberExpression Property;

        public abstract Expression<Func<TEntity, bool>> Do<TEntity>(object value);

        public static Operation Get(string op)
        {
            var operations = from t in Assembly.GetExecutingAssembly( ).GetTypes( )
                             where typeof( Operation ).IsAssignableFrom( t ) &&
                                   t.IsClass && !t.IsAbstract
                             select t;

            var operation = ( from o in operations
                              let attr =
                                  o.GetCustomAttributes( typeof( OperationAttribute ), false ).SingleOrDefault( ) as
                                  OperationAttribute
                              where attr != null && attr.Op.Equals( op, StringComparison.InvariantCultureIgnoreCase )
                              select o ).SingleOrDefault( );

            return ( Operation )Activator.CreateInstance( operation, null );
        }

        public virtual Operation For(ParameterExpression param, MemberExpression property)
        {
            Param = param;
            Property = property;

            return this;
        }

        public virtual dynamic GetCorrectParameterList(string[ ] collection)
        {
            return collection;
        }

        protected IEnumerable<T> ChangeTo<T>(IEnumerable<object> list)
        {
            return list.Cast<T>( ).ToList( );
        }
    }
}