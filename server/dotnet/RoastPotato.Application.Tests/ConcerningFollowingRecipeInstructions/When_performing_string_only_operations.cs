using System;
using System.Linq;
using NUnit.Framework;
using RoastPotato.Application.Tests.Repositories;
using RoastPotato.Recipes.Infrastructure;

namespace RoastPotato.Application.Tests.ConcerningFollowingRecipeInstructions
{
    [TestFixture]
    public class When_performing_string_only_operations
    {
        [Test]
        [ExpectedException( typeof( ArgumentException ) )]
        public void Should_not_work_on_types_other_than_string( )
        {
            const string instruction = "Value sw 'T'";

            var result = instruction.AsExpressionOf<SampleData>( );

            Assert.Catch( ( ) => result.Compile( ), "Invalid operation" );
        }

        [Test]
        public void Should_return_valid_expression_when_starts_with_operation_provided_on_string_property( )
        {
            const string instruction = "Address sw 'T'";

            var result = instruction.AsExpressionOf<SampleData>( );

            Assert.IsNotNull( result );
        }

        [Test]
        public void Should_return_valid_expression_when_ends_with_operation_provided_on_string_property( )
        {
            const string instruction = "Address ew 'T'";

            var result = instruction.AsExpressionOf<SampleData>( );

            Assert.IsNotNull( result );
        }
    }
}