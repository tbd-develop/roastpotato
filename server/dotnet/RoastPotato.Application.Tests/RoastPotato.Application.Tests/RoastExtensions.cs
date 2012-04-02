using System.Collections.Generic;

namespace RoastPotato.Application.Tests
{
    public static class RoastExtensions
    {
        public static IEnumerable<T> Roast<T>(this IEnumerable<T> set, Recipe recipe)
        {
            foreach ( var instruction in recipe.Instructions )
            {
                    
            }

            return set;
        }
    }
}