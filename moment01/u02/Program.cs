namespace u01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int pens = 3;
            int penPrice = 4;

            int apples = 1;
            int applePrice = 19;
            int appleWheight = 200;

            double totalPrice = pens * penPrice + applePrice * apples * appleWheight * 0.001;

            Console.WriteLine(
                "Jag handlade {0} pennor för {1} kr och {2} äpplen för {3:#.00} kr. Det kostade totalt {4:#.00} kr", pens,
                penPrice * pens, apples, applePrice * apples * appleWheight * 0.001, totalPrice);
            Console.WriteLine(
                $"Jag handlade {pens} pennor för {penPrice * pens} kr och {apples} äpplen för {applePrice * apples * appleWheight * 0.001:#.00} kr. Det kostade totalt {totalPrice:#.00} kr");
        }
    }
}