namespace Mathematics;

class Program
{
    static void Main(string[] args)
    {
        double areaA, areaB, areaRelation, randomP, randomA;
        Random rand = new Random();

        Console.WriteLine("Areaförhållande för uppgift a) och b)");
        Console.WriteLine($"{"i",3} | {"Random p",9} | {"Area A_A",9} | {"Area A_B",9} | {"A_A/A_B",9}");
        Console.WriteLine(new string('-', 55));

        for (int i = 1; i < 100; i++)
        {
            randomP = rand.Next(1, 1000000) / 1000.0;
            areaB = 1.0 / (randomP + 1.0);
            areaA = 1.0 - areaB;
            areaRelation = areaA / areaB;

            Console.WriteLine($"{i,3} | {randomP,9:F2} | {areaA,9:F2} | {areaB,9:F2} | {areaRelation,9:F2}");
        }

        Console.WriteLine();
        
        Console.WriteLine("Areaförhållande för uppgift c)");
        Console.WriteLine($"{"i",3} | {"Random p",9} | {"Random a",9} | {"Area A_A",13} | {"Area A_B",13} | {"A_A/A_B",9}");
        Console.WriteLine(new string('-', 76));

        for (int i = 1; i < 100; i++)
        {
            randomP = rand.Next(1, 10);   // mindre värden för stabilitet
            randomA = rand.Next(1, 10);
            areaB = Math.Pow(randomA, randomP + 1) / (randomP + 1.0);
            areaA = randomA * Math.Pow(randomA, randomP) - areaB;
            areaRelation = areaA / areaB;

            Console.WriteLine($"{i,3} | {randomP,9:F2} | {randomA,9:F2} | {areaA,13:F2} | {areaB,13:F2} | {areaRelation,9:F2}");
        }
    }
}