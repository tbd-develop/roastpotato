using System;
using System.Reflection;
using NUnit.Framework;

namespace RoastPotato.Application.Tests.ConcerningGettingPropertyInfo
{
    [TestFixture]
    public class When_consructing_expression_from_string
    {
        [Test]
        public void Should_create_a_valid_property_info_of_correct_type_when_provided_valid_property_name_of_T( )
        {
            PropertyInfo p = "Address".GetPropertyInfo<SampleData>( );

            Assert.AreEqual( typeof( string ), p.PropertyType );
        }

        [Test]
        public void Should_create_a_valid_property_info_of_correct_type_when_provided_with_an_associated_property_name_of_T( )
        {
            PropertyInfo p = "Location.Created".GetPropertyInfo<SampleData>( );

            Assert.AreEqual( typeof( DateTime ), p.PropertyType );
        }
    }
}