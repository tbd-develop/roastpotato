using System;

namespace RoastPotato.Recipes.Operations
{
    [AttributeUsage( AttributeTargets.Class, AllowMultiple = false )]
    public class OperationAttribute : Attribute
    {
        public string Op { get; private set; }
        public Type[ ] ValidOnTypes { get; set; }

        public OperationAttribute(string op)
        {
            Op = op;
        }
    }
}