using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RoastPotato.Application.Tests.Repositories;
using RoastPotato.Recipes;

namespace RoastPotato.Application.Tests.ConcerningRecipes
{
    [TestFixture]
    public class When_given_instructions
    {
        [Test]
        public void Should_be_marked_as_invalid_when_instructions_are_empty( )
        {
            var r = new Recipe<SampleData>( string.Empty );

            Assert.IsTrue( r.Invalid );
        }

        [Test]
        public void Should_build_a_single_instruction_when_provided_one_value_query( )
        {
            var r = new Recipe<SampleData>( "Address lk 'Test'" );

            Assert.IsNotNull( r.Prepare( ) );
        }

        [Test]
        public void Should_build_a_working_expression_given_one_value_query( )
        {
            var r = new Recipe<SampleData>( "Address lk 'Test'" );

            var data = new SampleRepository
                           {
                               DataSource = new[ ]
                                                {
                                                    new SampleData {Address = "Test 2"},
                                                    new SampleData {Address = "Test 3"},
                                                    new SampleData {Address = "Not like anything else"},
                                                }
                           }.DataSource;
            data = data.Where( r.Prepare( ) );

            Assert.AreEqual( 2, data.Count( ) );
        }

        [Test]
        public void Should_build_a_single_expression_when_given_two_query_parameters_with_an_and( )
        {
            var r = new Recipe<SampleData>( "Address lk 'Test' and Value gt '20'" );

            var data = new SampleRepository
                           {
                               DataSource = new[ ]
                                                {
                                                    new SampleData {Address = "Test 2", Value = 21},
                                                    new SampleData {Address = "Test 3", Value = 19},
                                                    new SampleData {Address = "Not like anything else", Value = 30},
                                                }
                           }.DataSource;


            data = data.Where( r.Prepare( ) );

            Assert.AreEqual( 1, data.Count( ) );
        }

        [Test]
        public void Should_build_a_single_expression_when_given_a_complex_parameter_string( )
        {
            var r = new Recipe<SampleData>( "Address lk 'Test' and (Value gt '20' or Id eq '1')" );

            var data = new SampleRepository
                           {
                               DataSource = new[ ]
                                                {
                                                    new SampleData {Address = "Test 2", Value = 21, Id = 2},
                                                    new SampleData {Address = "Test 3", Value = 19, Id = 13},
                                                    new SampleData
                                                        {Address = "Test else", Value = 18, Id = 1},
                                                    new SampleData
                                                        {Address = "Not like anything else", Value = 30, Id = 4},
                                                }
                           }.DataSource;

            data = data.Where( r.Prepare( ) );

            Assert.AreEqual( 2, data.Count( ) );
        }
    }
}
