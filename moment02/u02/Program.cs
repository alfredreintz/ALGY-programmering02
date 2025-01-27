using System.Collections.Generic;

namespace u01;

class Program
{
    /// <summary>
    /// Startar applikationen
    /// </summary>
    /// <param name="args">standardparameter för main-metoden</param>
    static void Main(string[] args)
    {
        // Deklarerar variabler
        List<Car> carList = new List<Car>();
        char menuSelection;

        carList = addCarAtStart();

        // Loopar minst en gång
        do
        {
            // Loopar igeno möjliga svar från användaren
            switch (menuSelection = menu(carList))
            {
                case '0':
                    break;
                case '1':
                    printList(carList);
                    break;
                case '2':
                    carList = addCar(carList);
                    break;
                case '3':
                    removeCar(carList);
                    break;
                case '4':
                    emptyList(carList);
                    break;
                default:
                    break;
            }
        } while (menuSelection != '0');         // Fortsätter så länge användaren inte avslutar programmet
    }

    /// <summary>
    /// Skapar en användarmeny
    /// </summary>
    /// <returns>Användarens menyval</returns>
    public static char menu(List<Car> carList)
    {
        // Bygger upp menyns innehåll
        String menu = "\n\n##########################" +
                      "\n##                      ##" +
                      "\n## Programmeny          ##" +
                      "\n## Antal bilar: " + carList.Count + " st    ##" +
                      "\n##                      ##" +
                      "\n##########################" +
                      "\n1. Skriv ut listan" +
                      "\n2. Lägg till bil" +
                      "\n3. Ta bort bil" +
                      "\n4. Töm hela listan" +
                      "\n0. Avsluta" +
                      "\nAnge ditt val: ";

        // Skriver ut menyn
        Console.Write(menu);

        // Returnerar resultatet
        return Console.ReadKey().KeyChar;
    }
    
    /// <summary>
    /// Skriver ut lista med bilinformation
    /// </summary>
    /// <param name="carList">lista med bilars information</param>
    public static void printList(List<Car> carList)
    {
        int i = 1;
        
        Console.WriteLine("\n\nNr\tRegNr\tMake\tModell\tÅrsmodell\tTill salu?");
        
        // Loopar igenom listan med c motsvarande listans objekt
        foreach (Car c in carList)
        {
            Console.Write(i++);
            Console.WriteLine(c.ToStringList());
        }
    }

    /// <summary>
    /// Lägger till bil
    /// </summary>
    /// <param name="carList">Lista med bilar</param>
    /// <returns>Lista med bilar</returns>
    public static List<Car> addCar(List<Car> carList)
    {
        // Frågar och tar emot användarens input
        Console.WriteLine("\n\nAnge information om bilen");
        
        Console.Write("Registreringsnummer: ");
        String regNr = Console.ReadLine();
        
        Console.Write("bilmärke: ");
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
        if (Char.ToUpper(ch) == 'J')
        {
            forSale = true;
        }
        
        // Lägger till sist i listan
        carList.Add(new Car(regNr, make, model, year, forSale));

        return carList;
    }
    
    /// <summary>
    /// Lägger till bilar för testning av programmet
    /// </summary>
    /// <returns>Listan med bilar</returns>
    public static List<Car> addCarAtStart()
    {
        // Skapar en ny lista och lägger till bilar
        List<Car> carList = new List<Car>();
        carList.Add(new Car("ABC123", "Volvo", "V70", 2012, false));
        carList.Add(new Car("FDS434", "Audi", "RS6", 2024, true));
        carList.Add(new Car("LKH999", "Ford", "Fiesta", 2000, false));
        
        return carList;
    }

    /// <summary>
    /// Tar bort bil
    /// </summary>
    /// <param name="carList">Lista med bilar</param>
    /// <returns>Listan med bilar</returns>
    public static List<Car> removeCar(List<Car> carList)
    {
        Console.WriteLine("dessa bilar finns i din lista");
        
        printList(carList);
        
        Console.Write("\nväälj en bil att ta bort från listan [0 ångrar]: ");
        int removeIndex = Convert.ToInt16(Console.ReadLine());

        if (removeIndex != 0)
        {
            // Tar objekt vid specifikt index (1:a bilen för användaren motsvarar index 0 i listan)
            carList.RemoveAt(removeIndex - 1);
        }

        return carList;
    }

    /// <summary>
    /// Tar bort alla bilar i listan
    /// </summary>
    /// <param name="carList">Lista med bilar</param>
    /// <returns>Listan med bilar</returns>
    public static List<Car> emptyList(List<Car> carList)
    {
        carList.Clear();
        return carList;
    }
}