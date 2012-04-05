using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RoastPotato.Recipes.Infrastructure;

namespace RoastPotato.Recipes
{
    public class Recipe<TEntity>
    {
        public bool Invalid { get; private set; }

        private InstructionSet<TEntity> Instructions { get; set; }

        public Recipe(string instructions)
        {
            Instructions = new InstructionSet<TEntity>( instructions );

            Invalid = string.IsNullOrEmpty( instructions );
        }

        public Func<TEntity, bool> Prepare( )
        {
            return Instructions.Build( ).Compile( );
        }
    }
}