using System.Collections.Generic;

namespace u02;

class Program
{
    public static List<Vehicle> vehiclelist = new List<Vehicle>();

    /// <summary>
    /// Startar applikationen
    /// </summary>
    /// <param name="args">standardparameter för main-metoden</param>
    static void Main(string[] args)
    {
        // Deklarerar variabler
        char menuSelection;

        addVehicleAtStart();

        // Loopar minst en gång
        do
        {
            // Loopar igeno möjliga svar från användaren
            switch (menuSelection = menu())
            {
                case '0':
                    break;
                case '1':
                    printList();
                    break;
                case '2':
                    addCar();
                    break;
                case '3':
                    addLorry();
                    break;
                case '4':
                    removeVehicle();
                    break;
                case '5':
                    emptyList();
                    break;
                default:
                    break;
            }
        } while (menuSelection != '0'); // Fortsätter så länge användaren inte avslutar programmet
    }

    /// <summary>
    /// Skapar en användarmeny
    /// </summary>
    /// <returns>Användarens menyval</returns>
    public static char menu()
    {
        // Bygger upp menyns innehåll
        String menu = "\n\n##########################" +
                      "\n##                      ##" +
                      "\n## Programmeny          ##" +
                      "\n## Antal fordon: " + vehiclelist.Count + " st   ##" +
                      "\n##                      ##" +
                      "\n##########################" +
                      "\n1. Skriv ut listan" +
                      "\n2. Lägg till bil" +
                      "\n3. Lägg till lastbil" +
                      "\n4. Ta bort fordon" +
                      "\n5. Töm hela listan" +
                      "\n0. Avsluta" +
                      "\nAnge ditt val: ";

        // Skriver ut menyn
        Console.Write(menu);

        // Returnerar resultatet
        return Console.ReadKey().KeyChar;
    }

    /// <summary>
    /// Skriver ut lista med fordonsuppgifter
    /// </summary>
    public static void printList()
    {
        int i = 1;

        Console.WriteLine("\n\nNr\tRegNr\tMake\tModell\tÅrsmodell\tTill salu?\tÖvrig info");

        // Loopar igenom listan med c motsvarande listans objekt
        foreach (Vehicle c in vehiclelist)
        {
            Console.Write(i++);
            Console.WriteLine(c.ToStringList());
        }
    }

    /// <summary>
    /// Lägger till bil
    /// </summary>
    public static void addCar()
    {
        // Frågar och tar emot användarens input
        Console.WriteLine("\n\nAnge information om bilen");

        Console.Write("Registreringsnummer: ");
        String regNr = Console.ReadLine();

        Console.Write("Märke: ");
        String make = Console.ReadLine();

        Console.Write("Modell: ");
        String model = Console.ReadLine();

        Console.Write("Årsmodell: ");
        int year = Convert.ToInt32(Console.ReadLine());

        Console.Write("Till salu (J/N): ");
        // Läser endast in första bokstaven
        char ch = Convert.ToChar(Console.ReadKey().KeyChar);

        bool forSale = false;
        // Om användaren använt en gemen istället för char men bokstaven stämmer
        if (Char.ToUpper(ch) == 'J') forSale = true;

        // Lägger till sist i listan
        vehiclelist.Add(new Car(regNr, make, model, year, forSale));
    }

    /// <summary>
    /// Lägger till lastbil
    /// </summary>
    public static void addLorry()
    {
        // Frågar och tar emot användarens input
        Console.WriteLine("\n\nAnge information om bilen");

        Console.Write("Registreringsnummer: ");
        String regNr = Console.ReadLine();

        Console.Write("Märke: ");
        String make = Console.ReadLine();

        Console.Write("Modell: ");
        String model = Console.ReadLine();

        Console.Write("Årsmodell: ");
        int year = Convert.ToInt32(Console.ReadLine());

        Console.Write("Lastkapacitet: ");
        int load = Convert.ToInt32(Console.ReadLine());
        
        Console.Write("Till salu (J/N): ");
        // Läser endast in första bokstaven
        char ch = Convert.ToChar(Console.ReadKey().KeyChar);

        bool forSale = false;
        // Om användaren använt en gemen istället för char men bokstaven stämmer
        if (Char.ToUpper(ch) == 'J') forSale = true;

        // Lägger till sist i listan
        vehiclelist.Add(new Lorry(regNr, make, model, year, forSale, load));
    }

    /// <summary>
    /// Lägger till fordon för testning av programmet
    /// </summary>
    public static void addVehicleAtStart()
    {
        vehiclelist.Add(new Car("ABC123", "Volvo", "V70", 2012, false));
        vehiclelist.Add(new Car("FDS434", "Audi", "RS6", 2024, true));
        vehiclelist.Add(new Lorry("LKH999", "Scania", "770", 2000, false, 32000));
    }

    /// <summary>
    /// Tar bort fordon
    /// </summary>
    public static void removeVehicle()
    {
        Console.WriteLine("dessa fordon finns i din lista");

        printList();

        Console.Write("\nväälj en bil att ta bort från listan [0 ångrar]: ");
        int removeIndex = Convert.ToInt16(Console.ReadLine());

        if (removeIndex != 0)
        {
            // Tar objekt vid specifikt index (1:a fordeonet för användaren motsvarar index 0 i listan)
            vehiclelist.RemoveAt(removeIndex - 1);
        }
    }

    /// <summary>
    /// Tar bort alla fordon i listan
    /// </summary>
    public static void emptyList()
    {
        vehiclelist.Clear();
    }
}