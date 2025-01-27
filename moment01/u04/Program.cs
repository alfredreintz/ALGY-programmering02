namespace u04;

class Program
{
    static void Main(string[] args)
    {
        DateTime dt = DateTime.Now;
        int hour = dt.Hour;
        Console.WriteLine(hour);

        if (hour >= 8 && hour < 16)
        {
            Console.WriteLine("Skoldagen pågår");
        }
        else if (hour >= 16)
        {
            Console.WriteLine("Skoldagen har slutat");
        }
        else
        {
            Console.WriteLine("Skoldagen har inte börjat");
        }
    }
}