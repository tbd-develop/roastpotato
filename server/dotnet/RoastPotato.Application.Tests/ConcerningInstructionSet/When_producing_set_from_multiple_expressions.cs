using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using RoastPotato.Application.Tests.Repositories;
using RoastPotato.Recipes.Infrastructure;

namespace RoastPotato.Application.Tests.ConcerningInstructionSet
{
    [TestFixture]
    public class When_producing_set_from_multiple_expressions
    {
        private string _expressionToTest;
        private InstructionSet<SampleData> _set;
        private SampleRepository _repository;

        [TestFixtureSetUp]
        public void Context( )
        {
            _expressionToTest = "Address lk 'Test' and ( Value gt '20' or AddDate gt '1/1/2011' )";

            _set = new InstructionSet<SampleData>( _expressionToTest );

            _repository = new SampleRepository
                              {
                                  DataSource = new[ ]
                                                   {
                                                       new SampleData() {Address = "Test 1"},
                                                       new SampleData() {Address = "Test 2", Value = 21, AddDate = new DateTime(2012,1,1)},
                                                       new SampleData() {Address = "Could Not Pass", Value = 45},
                                                       new SampleData() {Address = "No Pass", Value = 33},
                                                   }
                              };
        }

        [Test]
        public void Should_contain_operator_as_first_element( )
        {
            Assert.IsTrue( _set.Root.IsOperation );
        }

        [Test]
        public void Should_contain_instruction_to_build_expression_on_left_hand_side( )
        {
            Assert.IsNotNull( _set.Root.LeftHandSide.Content.AsExpressionOf<SampleData>( ) );
        }

        [Test]
        public void Should_contain_valid_expression_on_left_hand_side( )
        {
            var expr = _set.Root.LeftHandSide.Content.AsExpressionOf<SampleData>( );

            var data = _repository.DataSource.Where( expr.Compile( ) );

            Assert.AreEqual( 2, data.Count( ) );
        }

        [Test]
        public void Should_contain_operator_on_right_hand_side( )
        {
            Assert.IsTrue( _set.Root.RightHandSide.IsOperation );
        }

        [Test]
        public void Should_provide_single_valid_expression( )
        {
            Expression<Func<SampleData, bool>> expr = _set.Build( );

            var data = _repository.DataSource.Where( expr.Compile( ) );

            Assert.AreEqual( 1, data.Count( ) );
        }


    }
}