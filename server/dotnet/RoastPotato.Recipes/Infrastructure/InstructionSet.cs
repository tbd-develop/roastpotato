using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace RoastPotato.Recipes.Infrastructure
{
    public class Instruction
    {
        public bool IsOperation { get; set; }
        public string Content { get; set; }
        public Instruction LeftHandSide { get; set; }
        public Instruction RightHandSide { get; set; }
        public Instruction Parent { get; set; }
    }

    public class InstructionSet<TData>
    {
        private readonly string _expression;

        private const string OperationsRegex =
            @"(?<Lhs>[\w\d\s.:'!&?,]*)\s(?<Operator>and|or)\s(?:\()?(?<Rhs>[\w\d\s.:'!&?/]*)(?:\))?";

        public Instruction Root { get; private set; }

        public InstructionSet(string expression)
        {
            _expression = expression;

            Initialize( );
        }

        private void Initialize( )
        {
            Root = SeparateInstructions( _expression );
        }

        private Instruction SeparateInstructions(string instructions)
        {
            Instruction elementToBuild = null;

            var findInstructions = new Regex( OperationsRegex, RegexOptions.IgnoreCase );

            Match instruction = findInstructions.Match( instructions );

            while ( instruction.Success )
            {
                string lhs = instruction.Groups[ "Lhs" ].Value;
                string rhs = instruction.Groups[ "Rhs" ].Value;
                string op = instruction.Groups[ "Operator" ].Value;

                elementToBuild = !string.IsNullOrEmpty( op )
                                     ? new Instruction { Content = op, IsOperation = true }
                                     : new Instruction { Content = lhs };

                if ( lhs.Contains( "and" ) || lhs.Contains( "or" ) )
                    elementToBuild.LeftHandSide = SeparateInstructions( lhs );
                else if ( !string.IsNullOrEmpty( lhs ) )
                    elementToBuild.LeftHandSide = new Instruction { Content = lhs };

                if ( rhs.Contains( "and" ) || rhs.Contains( "or" ) )
                    elementToBuild.RightHandSide = SeparateInstructions( rhs );
                else if ( !string.IsNullOrEmpty( rhs ) )
                    elementToBuild.RightHandSide = new Instruction { Content = rhs };

                instruction = instruction.NextMatch( );
            }

            if ( elementToBuild == null )
                elementToBuild = new Instruction { Content = instructions };

            return elementToBuild;
        }

        public Expression<Func<TData, bool>> Build( )
        {
            return Walk( Root );
        }

        private Expression<Func<TData, bool>> Walk(Instruction instr)
        {
            Expression<Func<TData, bool>> result = null;

            if ( instr.IsOperation )
            {
                Expression<Func<TData, bool>> left = null;
                Expression<Func<TData, bool>> right = null;

                if ( instr.LeftHandSide.IsOperation )
                    left = Walk( instr.LeftHandSide );
                else
                    left = instr.LeftHandSide.Content.AsExpressionOf<TData>( );

                if ( instr.RightHandSide.IsOperation )
                    right = Walk( instr.RightHandSide );
                else
                    right = instr.RightHandSide.Content.AsExpressionOf<TData>( );

                switch ( instr.Content )
                {
                    case "and":
                        {
                            result = left.And( right );
                        }
                        break;
                    case "or":
                        {
                            result = left.Or( right );
                        }
                        break;
                }
            }
            else
            {
                result = instr.Content.AsExpressionOf<TData>( );
            }

            return result;
        }
    }
}