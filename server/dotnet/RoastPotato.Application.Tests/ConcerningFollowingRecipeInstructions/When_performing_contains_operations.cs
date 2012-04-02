using System;
using System.Linq;
using NUnit.Framework;
using RoastPotato.Application.Tests.Repositories;
using RoastPotato.Recipes.Infrastructure;

namespace RoastPotato.Application.Tests.ConcerningFollowingRecipeInstructions
{
    [TestFixture]
    public class When_performing_contains_operations
    {
        [Test]
        public void Should_filter_list_given_string_property_like( )
        {
            const string instruction = "Address lk 'Test'";

            var result = instruction.AsExpressionOf<SampleData>( );

            var data = new SampleRepository
                           {
                               DataSource = new[ ]
                                                {
                                                    new SampleData() {Address = "Test 1"},
                                                    new SampleData() {Address = "2 Test"},
                                                    new SampleData() {Address = "Not like"}
                                                }
                           }.DataSource;

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 2, data.Count( ) );
        }

        [Test]
        public void Should_filter_list_given_in_string_property_in( )
        {
            const string instruction = "Address in 'Test 1','Test 2'";

            var result = instruction.AsExpressionOf<SampleData>( );

            var data = new SampleRepository
                           {
                               DataSource = new[ ]
                                                {
                                                    new SampleData() {Address = "Test 1"},
                                                    new SampleData() {Address = "Test 2"},
                                                    new SampleData() {Address = "Test 4"}
                                                }
                           }.DataSource;

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 2, data.Count( ) );
        }

        [Test]
        public void Should_filter_list_given_integer_property_in( )
        {
            const string instruction = "Id in '22','1'";

            var result = instruction.AsExpressionOf<SampleData>( );

            var data = new SampleRepository
                           {
                               DataSource = new[ ]
                                                {
                                                    new SampleData() {Id = 1},
                                                    new SampleData() {Id = 22},
                                                    new SampleData() {Id = 1023}
                                                }
                           }.DataSource;

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 2, data.Count( ) );
        }

        [Test]
        public void Should_filter_list_given_date_property_in( )
        {
            const string instruction = "AddDate.Date in '1/1/2012','1/1/2011'";

            var result = instruction.AsExpressionOf<SampleData>( );

            var data = new SampleRepository
                           {
                               DataSource = new[ ]
                                                {
                                                    new SampleData() {AddDate = new DateTime(2012, 1, 1)},
                                                    new SampleData() {AddDate = new DateTime(2011, 1, 1)},
                                                    new SampleData() {AddDate = new DateTime(2010, 1, 1)}
                                                }
                           }.DataSource;

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 2, data.Count( ) );
        }

        [Test]
        public void Should_filter_list_given_date_time_property_in( )
        {
            const string instruction = "AddDate in '1/1/2012 23:45','1/1/2011 19:10'";

            var result = instruction.AsExpressionOf<SampleData>( );

            var data = new SampleRepository
            {
                DataSource = new[ ]
                                                {
                                                    new SampleData() {AddDate = new DateTime(2012, 1, 1)},
                                                    new SampleData() {AddDate = new DateTime(2012, 1, 1, 22, 45, 0, 0, 0)},
                                                    new SampleData() {AddDate = new DateTime(2012, 1, 1, 23, 45, 0, 0, 0)},
                                                    new SampleData() {AddDate = new DateTime(2011, 1, 1, 19, 10, 0, 0, 0)},
                                                    new SampleData() {AddDate = new DateTime(2011, 1, 1, 20, 10, 0, 0, 0)},
                                                    new SampleData() {AddDate = new DateTime(2011, 1, 1)},
                                                    new SampleData() {AddDate = new DateTime(2010, 1, 1)}
                                                }
            }.DataSource;

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 2, data.Count( ) );
        }       
    }
}