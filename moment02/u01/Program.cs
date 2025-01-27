namespace u01;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Ange information om bilen");

        Console.Write("Registreringsnummer: ");
        String regNr = Console.ReadLine();

        Console.Write("Bilmärke: ");
        String make = Console.ReadLine();

        Console.Write("Modell: ");
        String model = Console.ReadLine();

        Console.Write("Year: ");
        int year = Convert.ToInt16(Console.ReadLine());

        Console.Write("Till salu (J/N): ");
        char ch = Convert.ToChar(Console.ReadLine());

        bool forSale = false;

        if (char.ToUpper(ch) == 'J')
        {
            forSale = true;
        }

        Car c = new Car(regNr, make, model, year, forSale);
        
        Console.WriteLine("\n" + c.ToString());
    }
}