using System.Linq;
using NUnit.Framework;
using RoastPotato.Application.Tests.Repositories;
using RoastPotato.Recipes.Infrastructure;

namespace RoastPotato.Application.Tests.ConcerningFollowingRecipeInstructions
{
    [TestFixture]
    public class When_performing_greater_than_or_less_than_operations
    {
        [Test]
        public void Should_return_only_items_greater_than( )
        {
            const string instruction = "Value gt '50'";

            var result = instruction.AsExpressionOf<SampleData>( );


            var data = new SampleRepository
                           {
                               DataSource = new[ ]
                                                {
                                                    new SampleData() {Value = 25M},
                                                    new SampleData() {Value = 51M},
                                                    new SampleData() {Value = 100M}
                                                }
                           }.DataSource;

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 2, data.Count( ) );
        }

        [Test]
        public void Should_return_items_greater_than_or_equal_to_when_specified()
        {
            const string instruction = "Value gte '25'";

            var result = instruction.AsExpressionOf<SampleData>( );


            var data = new SampleRepository
            {
                DataSource = new[ ]
                                                {
                                                    new SampleData() {Value = 10M},
                                                    new SampleData() {Value = 25M},
                                                    new SampleData() {Value = 51M},
                                                    new SampleData() {Value = 100M}
                                                }
            }.DataSource;

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 3, data.Count( ) );
        }

        [Test]
        public void Should_return_only_items_less_than( )
        {
            const string instruction = "Value lt '60'";

            var result = instruction.AsExpressionOf<SampleData>( );


            var data = new SampleRepository
            {
                DataSource = new[ ]
                                                {
                                                    new SampleData() {Value = 25M},
                                                    new SampleData() {Value = 51M},
                                                    new SampleData() {Value = 100M}
                                                }
            }.DataSource;

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 2, data.Count( ) );
        }

        [Test]
        public void Should_return_items_less_than_or_equal_to_when_specified( )
        {
            const string instruction = "Value lte '51'";

            var result = instruction.AsExpressionOf<SampleData>( );


            var data = new SampleRepository
            {
                DataSource = new[ ]
                                                {
                                                    new SampleData() {Value = 10M},
                                                    new SampleData() {Value = 25M},
                                                    new SampleData() {Value = 51M},
                                                    new SampleData() {Value = 100M}
                                                }
            }.DataSource;

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 3, data.Count( ) );
        }
    }
}