using System.Collections.Generic;

namespace u09;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hur många tärningsslag vill du göra:");
        int amount = Convert.ToInt32(Console.ReadLine());
        Random r = new Random();

        List<int> result = new List<int>();

        for (int i = 1; i <= amount; i++)
        {
            result.Add(r.Next(1, 7));
        }
        
        int count1 = result.Count(x => x == 1);
        int count2 = result.Count(x => x == 2);
        int count3 = result.Count(x => x == 3);
        int count4 = result.Count(x => x == 4);
        int count5 = result.Count(x => x == 5);
        int count6 = result.Count(x => x == 6);
        
        Console.WriteLine($"Su fick:\n1: {count1} gånger\n2: {count2} gånger\n3: {count3} gånger\n4: {count4} gånger\n5: {count5} gånger\n6: {count6} gånger");
    }
}