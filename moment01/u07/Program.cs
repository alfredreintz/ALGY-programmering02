namespace u07;

class Program
{
    static void Main(string[] args)
    {
        int rats = 100;
        int months = 0;

        while (rats < Math.Pow(10, 6))
        {
            rats *= 2;
            months++;
        }
        Console.WriteLine(months + " månader");
    }
}