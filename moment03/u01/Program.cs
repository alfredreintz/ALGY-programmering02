namespace u01;

class Program
{
    static void Main(string[] args)
    {
        Car c = new Car("ABC124", "Saab", "95", 2003, false);
        Vehicle l = new Lorry("ABC123", "Volvo", "V70", 2013, true, 10000);

        
        Console.WriteLine($"\n {l.ToString()}");
        Console.WriteLine($"\n {c.ToString()}");
    }
}