namespace u01;

class Program
{
    static void Main(string[] args)
    {
        Car c = new Car("ABC123", "Volvo", "V70", 2013, false);
        Lorry l = new Lorry("ABC124", "Scania", "420", 2000, true, 15000);

        
        Console.WriteLine($"\n {c.ToStringList()}");
        Console.WriteLine($"\n {l.ToStringList()}");
    }
}