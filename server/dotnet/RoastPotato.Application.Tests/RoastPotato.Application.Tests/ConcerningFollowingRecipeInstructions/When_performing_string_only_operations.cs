using System;
using System.Linq;
using NUnit.Framework;
using RoastPotato.Application.Tests.Repositories;

namespace RoastPotato.Application.Tests.ConcerningFollowingRecipeInstructions
{
    [TestFixture]
    public class When_performing_string_only_operations
    {
        [Test]
        [ExpectedException( typeof( ArgumentException ) )]
        public void Should_not_work_on_types_other_than_string( )
        {
            const string instruction = "Value sw 'Test 1'";

            var result = instruction.AsExpressionOf<SampleData>( );

            Assert.Catch( ( ) => result.Compile( ), "Invalid operation" );
        }
    }
}