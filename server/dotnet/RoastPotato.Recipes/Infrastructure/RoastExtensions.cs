using System.Collections.Generic;
using System.Linq;

namespace RoastPotato.Recipes.Infrastructure
{
    public static class RoastExtensions
    {
        public static IEnumerable<T> Roast<T>(this IEnumerable<T> set, Recipe<T> recipe)
        {
            return set.Where( recipe.Prepare( ) );
        }
    }
}