using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;
using RoastPotato.Application.Tests.Operations;

namespace RoastPotato.Application.Tests.ConcerningOperations
{
    [TestFixture]
    public class When_requesting
    {
        [Test]
        public void Should_return_equals_operation_when_eq_is_provided( )
        {
            ParameterExpression param = Expression.Parameter( typeof( SampleData ), "x" );
            MemberExpression property = "Address".GetPropertyExpression<SampleData>( param );

            Operation operation = Operation.Get( "eq" ).For( param, property );

            Assert.IsNotNull( operation.Do<SampleData>( "52" ) );
        }
    }
}