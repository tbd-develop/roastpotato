using System;
using System.Linq;
using NUnit.Framework;
using RoastPotato.Application.Tests.Repositories;

namespace RoastPotato.Application.Tests.ConcerningFollowingRecipeInstructions
{
    [TestFixture]
    public class When_performing_equals_operations
    {
        [Test]
        public void Should_filter_list_given_string_property_equals( )
        {
            const string instruction = "Address eq 'Test 1'";

            var result = instruction.AsExpressionOf<SampleData>( );

            var data = new SampleRepository( ).GetSampleData( );

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 1, data.Count( ) );
        }    

        [Test]
        public void Should_filter_list_given_single_integer_property_equals( )
        {
            const string instruction = "Id eq '22'";

            var result = instruction.AsExpressionOf<SampleData>( );

            var data = new SampleRepository
                           {
                               DataSource = new[ ]
                                                {
                                                    new SampleData {Id = 1},
                                                    new SampleData {Id = 22}
                                                }
                           }.DataSource;

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 1, data.Count( ) );
        }

        [Test]
        public void Should_filter_list_given_single_date_when_asked_for_only_date_property_equals( )
        {
            const string instruction = "AddDate.Date eq '1/1/2012'";

            var result = instruction.AsExpressionOf<SampleData>( );

            var data = new SampleRepository
                           {
                               DataSource = new[ ]
                                                {
                                                    new SampleData {AddDate = new DateTime(2012, 1, 3)},
                                                    new SampleData {AddDate = new DateTime(2012, 1, 1, 23, 55, 0, 0)}
                                                }
                           }.DataSource;

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 1, data.Count( ) );
        }

        [Test]
        public void Should_filter_list_given_single_date_and_time_when_property_equals( )
        {
            const string instruction = "AddDate eq '1/1/2012 23:55'";

            var result = instruction.AsExpressionOf<SampleData>( );

            var data = new SampleRepository
            {
                DataSource = new[ ]
                                                {
                                                    new SampleData {AddDate = new DateTime(2012, 1, 3)},
                                                    new SampleData {AddDate = new DateTime(2012, 1, 1, 23, 55, 0, 0)}
                                                }
            }.DataSource;

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 1, data.Count( ) );
        }

        [Test]
        public void Should_filter_list_given_single_decimal_property_equals( )
        {
            const string instruction = "Value eq '23.95'";

            var result = instruction.AsExpressionOf<SampleData>( );

            var data = new SampleRepository
                           {
                               DataSource = new[ ]
                                                {
                                                    new SampleData {Value = 100},
                                                    new SampleData {Value = 23.95M}
                                                }
                           }.DataSource;

            data = data.Where( result.Compile( ) );

            Assert.AreEqual( 1, data.Count( ) );
        }
    }
}