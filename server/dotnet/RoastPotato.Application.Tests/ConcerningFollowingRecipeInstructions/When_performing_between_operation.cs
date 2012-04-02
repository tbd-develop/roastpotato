using System;
using System.Linq;
using NUnit.Framework;
using RoastPotato.Application.Tests.Repositories;
using RoastPotato.Recipes.Infrastructure;

namespace RoastPotato.Application.Tests.ConcerningFollowingRecipeInstructions
{
    [TestFixture]
    public class When_performing_between_operation
    {
        [Test]
        public void Should_return_items_greater_than_or_equal_to_and_less_than_or_equal_to( )
        {
            const string instruction = "Id bt '5','20'";

            var result = instruction.AsExpressionOf<SampleData>( );

            var data = new SampleRepository
                           {
                               DataSource = new[ ]
                                                {
                                                    new SampleData() {Id = 3},
                                                    new SampleData() {Id = 5},
                                                    new SampleData() {Id = 10},
                                                    new SampleData() {Id = 20},
                                                    new SampleData() {Id= 24}
                                                }
                           }.DataSource;

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 3, data.Count( ) );
        }

        [Test]
        public void Should_return_items_as_expected_when_provided_decimal( )
        {
            const string instruction = "Value bt '15.5','21'";

            var result = instruction.AsExpressionOf<SampleData>( );

            var data = new SampleRepository
                           {
                               DataSource = new[ ]
                                                {
                                                    new SampleData() {Value = 3},
                                                    new SampleData() {Value = 15.499999996M},
                                                    new SampleData() {Value = 15.51M},
                                                    new SampleData() {Value = 20},
                                                    new SampleData() {Value = 24}
                                                }
                           }.DataSource;

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 2, data.Count( ) );
        }

        [Test]
        public void Should_return_items_as_expected_when_provided_date_range( )
        {
            const string instruction = "AddDate bt '1/1/2012','5/1/2012'";

            var result = instruction.AsExpressionOf<SampleData>( );

            var data = new SampleRepository
            {
                DataSource = new[ ]
                                                {
                                                    new SampleData() {AddDate = new DateTime(2011,1,1)},
                                                    new SampleData() {AddDate = new DateTime(2012,1,1)},
                                                    new SampleData() {AddDate = new DateTime(2012,4,1)},
                                                    new SampleData() {AddDate = new DateTime(2013,1,1)},
                                                }
            }.DataSource;

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 2, data.Count( ) );
        }
    }
}
