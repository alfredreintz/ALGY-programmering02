namespace u06;

class Program
{
    static void Main(string[] args)
    {
        int multi = 1;
        
        for (int i = 2; i <= 10; i += 2)
        {
            multi *= i;
        }
        
        Console.WriteLine(multi);
    }
}