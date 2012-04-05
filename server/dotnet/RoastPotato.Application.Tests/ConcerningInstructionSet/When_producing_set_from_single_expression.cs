using NUnit.Framework;
using RoastPotato.Recipes.Infrastructure;

namespace RoastPotato.Application.Tests.ConcerningInstructionSet
{
    [TestFixture]
    public class When_producing_set_from_single_expression
    {
        [Test]
        public void Should_contain_expression_in_root_content( )
        {
            string addressLkTest = "Address lk 'Test'";

            var set = new InstructionSet<SampleData>( addressLkTest );

            Assert.AreEqual( addressLkTest, set.Root.Content );
        }

    }
}