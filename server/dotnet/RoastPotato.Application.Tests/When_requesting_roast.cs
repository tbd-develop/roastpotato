using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using RoastPotato.Application.Tests.Repositories;
using RoastPotato.Recipes;
using RoastPotato.Recipes.Infrastructure;

namespace RoastPotato.Application.Tests
{
    [TestFixture]
    public class When_requesting_roast
    {
        [Test]
        public void Should_return_original_set_when_no_recipe_is_specified( )
        {
            var data = new SampleRepository( ).GetSampleData( );

            var roasted = data.Roast( null );

            Assert.AreEqual( 2, roasted.Count( ) );
        }

        [Test]
        public void Should_return_original_set_when_recipe_has_no_parameters_specified( )
        {
            Recipe testKitchen = new Recipe( );

            var data = new SampleRepository().GetSampleData( );

            var roasted = data.Roast( testKitchen );

            Assert.AreEqual( 2, roasted.Count( ) );
        }

        [Test]
        public void Should_return_one_record_when_one_filter_is_requested( )
        {
            Recipe testKitchen = new Recipe
                                     {
                                         Instructions = new[ ]
                                                      {
                                                          "Address eq 'Test 1'"
                                                      }
                                     };

            var data = new SampleRepository( ).GetSampleData( );

            var roasted = data.Roast( testKitchen );

            Assert.AreEqual( 1, roasted.Count( ) );
        }

        
    }
}