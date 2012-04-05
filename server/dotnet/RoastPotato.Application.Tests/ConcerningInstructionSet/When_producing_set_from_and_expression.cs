using System;
using System.Linq.Expressions;
using NUnit.Framework;
using RoastPotato.Application.Tests.Repositories;
using RoastPotato.Recipes.Infrastructure;
using System.Linq;

namespace RoastPotato.Application.Tests.ConcerningInstructionSet
{
    [TestFixture]
    public class When_producing_set_from_and_expression
    {
        private string _expressionToTest;
        private InstructionSet<SampleData> _set;
        private SampleRepository _repository;

        [TestFixtureSetUp]
        public void Context( )
        {
            _expressionToTest = "Address lk 'Test' and Value gt '20'";

            _set = new InstructionSet<SampleData>( _expressionToTest );

            _repository = new SampleRepository
                              {
                                  DataSource = new[ ]
                                                   {
                                                       new SampleData() {Address = "Test 1"},
                                                       new SampleData() {Address = "Test 2", Value = 21},
                                                       new SampleData() {Address = "Could Not Pass", Value = 45},
                                                       new SampleData() {Address = "No Pass", Value = 33},
                                                   }
                              };
        }

        [Test]
        public void Should_contain_operator_as_first_element( )
        {
            Assert.AreEqual( "and", _set.Root.Content );
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
        public void Should_contain_instruction_to_build_expression_on_right_hand_side( )
        {
            Assert.IsNotNull( _set.Root.RightHandSide.Content.AsExpressionOf<SampleData>( ) );
        }

        [Test]
        public void Should_contain_valid_expression_on_right_hand_side( )
        {
            var expr = _set.Root.RightHandSide.Content.AsExpressionOf<SampleData>( );

            var data = _repository.DataSource.Where( expr.Compile( ) );

            Assert.AreEqual( 3, data.Count( ) );
        }

        [Test]
        public void Should_provide_single_valid_expression( )
        {
            Expression<Func<SampleData, bool>> expr = null;

            switch ( _set.Root.Content )
            {
                case "and":
                    {
                        expr =
                            _set.Root.LeftHandSide.Content.AsExpressionOf<SampleData>( ).And(
                                _set.Root.RightHandSide.Content.AsExpressionOf<SampleData>( ) );
                    }
                    break;
            }

            var data = _repository.DataSource.Where( expr.Compile( ) );

            Assert.AreEqual( 1, data.Count( ) );
        }
    }
}