using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoastPotato.Recipes;

namespace RoastPotato.CommandLine
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public decimal CreditLimit { get; set; }

        public override string ToString( )
        {
            return string.Format( "Hello, I am {0} {1} and I live at {2},{4} and have a limit of {3}", FirstName,
                                 LastName,
                                 Address, CreditLimit, State );
        }
    }

    public class CustomerRepository
    {
        public IEnumerable<Customer> DataSource { get; set; }
    }

    class Program
    {
        static void Main(string[ ] args)
        {
            CustomerRepository repository = new CustomerRepository
                                                {
                                                    DataSource = new[ ]
                                                                     {
                                                                         new Customer
                                                                             {
                                                                                 FirstName = "John",
                                                                                 LastName = "Smith",
                                                                                 CreditLimit = 10000,
                                                                                 Address = "156 Langolier Drive",
                                                                                 State = "NJ"
                                                                             },
                                                                         new Customer
                                                                             {
                                                                                 FirstName = "Herbert",
                                                                                 LastName = "Jenkins",
                                                                                 CreditLimit = 5000,
                                                                                 Address = "123 Penny Lane",
                                                                                 State = "OR"
                                                                             },
                                                                         new Customer
                                                                             {
                                                                                 FirstName = "Fred",
                                                                                 LastName = "Jones",
                                                                                 CreditLimit = 1500,
                                                                                 Address = "155 Poor Street",
                                                                                 State = "NJ"
                                                                             }
                                                                     }
                                                };

            //string recipeInstructions = "CreditLimit gte '5000' and LastName lk 'Jen' or CreditLimit lt '1600'";

            //Recipe<Customer> customerRecipe = new Recipe<Customer>( recipeInstructions );

            //var results = repository.DataSource.Where( customerRecipe.Prepare( ) );

            //foreach ( var result in results )
            //{
            //    Console.WriteLine( result );
            //}

            while ( true )
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Clear( );
                Console.WriteLine( "Query" );
                Console.WriteLine( new string( '-', 30 ) );
                Console.Write( ">" );
                var recipe = Console.ReadLine( );

                if ( recipe.Equals( "quit", StringComparison.InvariantCultureIgnoreCase ) )
                    break;

                Recipe<Customer> customerRecipe = new Recipe<Customer>( recipe );

                if ( customerRecipe.Invalid )
                    break;

                var results = repository.DataSource.Where( customerRecipe.Prepare( ) );

                Console.WriteLine( "First Name\t\tLastName\t\tState\t\tAddress\t\tCredit Limit" );
                Console.WriteLine( new string( '-', 100 ) );

                foreach ( var result in results )
                {
                    Console.WriteLine( string.Format( "{0}\t\t{1}\t\t{2}", result.FirstName, result.LastName,
                                                    result.Address ) );
                }

                Console.WriteLine( "Any key to continue" );
                Console.ReadKey( );
            }
        }
    }
}
