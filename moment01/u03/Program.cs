namespace u03;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Antalet pennor: ");
        int pens = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Pris per penna: ");
        double penPrice = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Äpplen(g): ");
        double appleWheight = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Pris per kilo för äpplen: ");
        double applePrice = Convert.ToDouble(Console.ReadLine());
        double totalPrice = pens * penPrice + applePrice * appleWheight * 0.001;

        Console.WriteLine(
            $"Jag handlade {pens} pennor för {penPrice * pens:#.00} kr där en penna kostade {penPrice:#.00} kr och äpplen som kostar {applePrice * appleWheight * 0.001:#.00} kr som väger {appleWheight:#.00} g. Äpplen kostar {applePrice:#.00} kr/kg och den totala kostnaden blev {totalPrice:#.00} kr");
        Console.WriteLine(
            "Jag handlade {0} pennor för {1:#.00} kr där en penna kostade {2:#.00} kr och äpplen som kostar {3:#.00} kr som väger {4:#.00} g. Äpplen kostar {5:#.00} kr/kg och den totala kostnaden blev {6:#.00} kr",
            pens, penPrice * pens, penPrice, applePrice * appleWheight * 0.001, appleWheight, applePrice, totalPrice);
    }
}