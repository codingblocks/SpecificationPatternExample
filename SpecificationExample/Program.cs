using System;
using System.Collections.Generic;

using SpecificationExample.Specification;
using SpecificationExample.Core;
using SpecificationExample.Domains.Order;

namespace SpecificationExample
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Specification Example");
            Console.WriteLine("===============================");

			var Allen = new Core.User("Allen", "Underwood", new List<User.Role>()
			{
				User.Role.Invoicing
			});
			
            var Michael = new Core.User("Michael", "Outlaw", new List<User.Role>() { 
                User.Role.Accounting,
                User.Role.CustomerServiceRepresentative
            });

            var Joe = new Core.User("Joe", "Zack", new List<User.Role>() { 
                User.Role.CustomerServiceManager
            });


            // Setting up some dummy orders for demonstration purposes
            var order1 = new OrderAggregate(100, 0);
            var order2 = new OrderAggregate(50, 10);
            var order3 = new OrderAggregate(40, 0);

            var allenCanDiscountSpec = Factory<IOrderAggregate>.GetUserCanDiscountSpecification(Allen);
            var michaelCanDiscountSpec = Factory<IOrderAggregate>.GetUserCanDiscountSpecification(Michael);
            var joeCanDiscountSpec = Factory<IOrderAggregate>.GetUserCanDiscountSpecification(Joe);

            // Should all be false - Allen only has the "Invoicing" Role...none of the customer service roles
			Console.WriteLine("Allen can Modify Order1: " + allenCanDiscountSpec.IsSatisfiedBy(order1));
			Console.WriteLine("Allen can Modify Order2: " + allenCanDiscountSpec.IsSatisfiedBy(order2));
			Console.WriteLine("Allen can Modify Order3: " + allenCanDiscountSpec.IsSatisfiedBy(order3));
            Console.WriteLine("");

            // Should all be true - Michael is a Customer Service Representative
            Console.WriteLine("Michael can Modify Order1: " + michaelCanDiscountSpec.IsSatisfiedBy(order1));
            Console.WriteLine("Michael can Modify Order2: " + michaelCanDiscountSpec.IsSatisfiedBy(order2));
            Console.WriteLine("Michael can Modify Order3: " + michaelCanDiscountSpec.IsSatisfiedBy(order3));
            Console.WriteLine();

			// Should all be true - Joe is a Customer Service Manager
			Console.WriteLine("Joe can Modify Order1: " + joeCanDiscountSpec.IsSatisfiedBy(order1));
			Console.WriteLine("Joe can Modify Order2: " + joeCanDiscountSpec.IsSatisfiedBy(order2));
			Console.WriteLine("Joe can Modify Order3: " + joeCanDiscountSpec.IsSatisfiedBy(order3));
			Console.WriteLine();


			var michaelCanDiscountOrder1By10Spec = Factory<IOrderAggregate>.GetUserCanDiscountByAmountSpecification(Michael, 10);
			var michaelCanDiscountOrder1By25Spec = Factory<IOrderAggregate>.GetUserCanDiscountByAmountSpecification(Michael, 25);

			Console.WriteLine("Michael can Modify Order1 by 10: " + michaelCanDiscountOrder1By10Spec.IsSatisfiedBy(order1));
			Console.WriteLine("Michael can Modify Order1 by 25: " + michaelCanDiscountOrder1By25Spec.IsSatisfiedBy(order1));
			Console.WriteLine();

			var joeCanDiscountOrder1By10Spec = Factory<IOrderAggregate>.GetUserCanDiscountByAmountSpecification(Joe, 10);
			var joeCanDiscountOrder1By25Spec = Factory<IOrderAggregate>.GetUserCanDiscountByAmountSpecification(Joe, 25);
			var joeCanDiscountOrder1By35Spec = Factory<IOrderAggregate>.GetUserCanDiscountByAmountSpecification(Joe, 35);
			var joeCanDiscountOrder1By40Spec = Factory<IOrderAggregate>.GetUserCanDiscountByAmountSpecification(Joe, 40);
			var joeCanDiscountOrder2By7_50Spec = Factory<IOrderAggregate>.GetUserCanDiscountByAmountSpecification(Joe, 7.50);
			var joeCanDiscountOrder2By7_51Spec = Factory<IOrderAggregate>.GetUserCanDiscountByAmountSpecification(Joe, 7.51);

			Console.WriteLine("Joe can Modify Order1 by 10: " + joeCanDiscountOrder1By10Spec.IsSatisfiedBy(order1));
			Console.WriteLine("Joe can Modify Order1 by 25: " + joeCanDiscountOrder1By25Spec.IsSatisfiedBy(order1));
			Console.WriteLine("Joe can Modify Order1 by 35: " + joeCanDiscountOrder1By35Spec.IsSatisfiedBy(order1));
			Console.WriteLine("Joe can Modify Order1 by 40: " + joeCanDiscountOrder1By40Spec.IsSatisfiedBy(order1));
			Console.WriteLine("Joe can Modify Order2 by 7.50: " + joeCanDiscountOrder2By7_50Spec.IsSatisfiedBy(order2));
			Console.WriteLine("Joe can Modify Order2 by 7.51: " + joeCanDiscountOrder2By7_51Spec.IsSatisfiedBy(order2));
			Console.WriteLine();


		}
    }
}
