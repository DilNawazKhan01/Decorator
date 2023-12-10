using System;

namespace Decorator
{
    // The base component
    abstract class Beverage
    {
        public virtual string Description { get; } = "Unknown Beverage";
        public abstract double Cost();
    }

    // Concrete components (different coffee types)
    class HouseBlend : Beverage
    {
        public override string Description { get; } = "House Blend Coffee";
        public override double Cost() => 0.89;
    }

    class DarkRoast : Beverage
    {
        public override string Description { get; } = "Dark Roast Coffee";
        public override double Cost() => 0.99;
    }

    class Decaf : Beverage
    {
        public override string Description { get; } = "Decaf Coffee";
        public override double Cost() => 1.05;
    }

    class Espresso : Beverage
    {
        public override string Description { get; } = "Espresso Coffee";
        public override double Cost() => 1.99;
    }

    // Decorator base class
    abstract class CondimentDecorator : Beverage
    {
        protected readonly Beverage Beverage;

        protected CondimentDecorator(Beverage beverage)
        {
            Beverage = beverage ?? throw new ArgumentNullException(nameof(beverage));
        }
    }

    // Concrete decorators (condiments)
    class Milk : CondimentDecorator
    {
        public Milk(Beverage beverage) : base(beverage) { }

        public override string Description => Beverage.Description + ", Steamed Milk";
        public override double Cost() => Beverage.Cost() + 0.20;
    }

    class Mocha : CondimentDecorator
    {
        public Mocha(Beverage beverage) : base(beverage) { }

        public override string Description => Beverage.Description + ", Mocha";
        public override double Cost() => Beverage.Cost() + 0.20;
    }

    class Soy : CondimentDecorator
    {
        public Soy(Beverage beverage) : base(beverage) { }

        public override string Description => Beverage.Description + ", Soy";
        public override double Cost() => Beverage.Cost() + 0.15;
    }

    class WhippedCream : CondimentDecorator
    {
        public WhippedCream(Beverage beverage) : base(beverage) { }

        public override string Description => Beverage.Description + ", Whipped Cream";
        public override double Cost() => Beverage.Cost() + 0.10;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Starbuzz Coffee!");

            Console.WriteLine("Please select your coffee:");
            Console.WriteLine("1. House Blend Coffee");
            Console.WriteLine("2. Dark Roast Coffee");
            Console.WriteLine("3. Decaf Coffee");
            Console.WriteLine("4. Espresso Coffee");

            if (!int.TryParse(Console.ReadLine(), out int coffeeChoice) || coffeeChoice < 1 || coffeeChoice > 4)
            {
                Console.WriteLine("Invalid coffee choice. Exiting...");
                return;
            }

            Beverage beverage = null;

            switch (coffeeChoice)
            {
                case 1:
                    beverage = new HouseBlend();
                    break;
                case 2:
                    beverage = new DarkRoast();
                    break;
                case 3:
                    beverage = new Decaf();
                    break;
                case 4:
                    beverage = new Espresso();
                    break;
                default:
                    Console.WriteLine("Invalid coffee choice. Exiting...");
                    return;
            }

            Console.WriteLine("Please select condiments (comma-separated):");
            Console.WriteLine("1. Milk (0.20)");
            Console.WriteLine("2. Mocha (0.20)");
            Console.WriteLine("3. Soy (0.15)");
            Console.WriteLine("4. Whipped cream (0.10)");

            var condimentChoices = Console.ReadLine()?.Split(',');

            if (condimentChoices == null)
            {
                Console.WriteLine("Invalid condiment choices. Exiting...");
                return;
            }

            foreach (var choice in condimentChoices)
            {
                var trimmedChoice = choice.Trim();

                switch (trimmedChoice)
                {
                    case "1":
                        beverage = new Milk(beverage);
                        break;
                    case "2":
                        beverage = new Mocha(beverage);
                        break;
                    case "3":
                        beverage = new Soy(beverage);
                        break;
                    case "4":
                        beverage = new WhippedCream(beverage);
                        break;
                    default:
                        Console.WriteLine($"Invalid condiment choice: {trimmedChoice}");
                        break;
                }
            }

            Console.WriteLine($"Your order: {beverage.Description}");
            Console.WriteLine($"Total cost: {beverage.Cost()}");
        }
    }
}
