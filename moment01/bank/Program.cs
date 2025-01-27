using System.Collections.Generic;

namespace bank;

class Program
{
    // Skapar en global lista med listor i där listorna i innehåller strängar
    public static List<List<string>> transactions = new List<List<string>>();

    static void Main(string[] args)
    {
        // Om filen inte finns så skapas den och lägger till transaktion på 1000 kr
        if (!File.Exists("transactions.txt"))
        {
            string fileName = "transactions.txt";
            // Öppnar fil i skrivläge där filen inte skrivs över
            StreamWriter writer = new StreamWriter(fileName, true);
            writer.Close();

            transaction(1000, "Banken bjuder");
        }

        char menuChoice;
        string menuOutput = "\n------------------------------" +
                            "\nHej och välkommen till banken!";

        Console.Write(menuOutput);

        // Kör loopen minst en gång
        do
        {
            // Uppdaterar transaktionslistan
            updateTransactions();

            double balance = Math.Abs(Math.Round(getBalance(), 2));

            menuOutput =
                "\n------------------------------" +
                $"\nMitt Saldo: {balance:#0.00} kr" +
                "\n------------------------------" +
                "\nVälj ett av följande alternativ" +
                "\n1. Visa transaktioner" +
                "\n2. Sätt in pengar" +
                "\n3. Ta ut pengar" +
                "\n4. Rensa konto" +
                "\n0. Stäng av banken" +
                "\n------------------------------" +
                "\nSvar: ";

            Console.Write(menuOutput);

            // Kollar efter error
            try
            {
                menuChoice = Convert.ToChar(Console.ReadLine());
            }
            catch (Exception)
            {
                menuChoice = 'X';
            }

            // Loopar igenom möjliga svar
            switch (menuChoice)
            {
                case '1':
                    displayTransactions();
                    break;
                case '2':
                case '3':
                    if (menuChoice == '3' && balance <= 0)
                    {
                        Console.WriteLine("\nDu har inga pengar att ta ut");
                        break;
                    }

                    double amount = 0;
                    bool err = false;
                    string typeOfTransaction = "deposit";

                    // Loopar så läänge användaren inte anger rätt information
                    do
                    {
                        Console.Write("\nSumma: ");

                        try
                        {
                            // Absolutvärdet blir alltid positivt oavsett vad användaren anger
                            amount = Math.Round(Convert.ToDouble(Console.ReadLine()), 2);

                            if (amount <= 0)
                            {
                                Console.WriteLine("\nSumma ej tillåten");
                                err = true;
                                continue;
                            }

                            err = false;
                        }
                        // Om användaren inte anger en double fångas errorn
                        catch (Exception)
                        {
                            Console.WriteLine("\nDu måste ange ett nummber");
                            err = true;
                            continue;
                        }

                        if (menuChoice == '3')
                        {
                            if (amount > balance)
                            {
                                Console.WriteLine("Du kan inte ta ut mer perngar än du har");
                                err = true;
                            }
                            else
                            {
                                // Om användaren vill ta ut pengar
                                amount = -amount;
                            }
                        }
                    } while (err == true);

                    Console.Write("Information om insättning: ");
                    string information = Console.ReadLine();

                    transaction(amount, information);

                    break;
                case '4':
                    clearTransactions();
                    transaction(1000, "Banken bjuder");
                    Console.WriteLine("\nKontot har rensats");
                    break;
                case '0':
                    Console.WriteLine("\nApplikationen avslutas");
                    break;
                // Om användaren inte anger något av alternativens
                default:
                    Console.WriteLine("\nAlternativet är inte tillgängligt");
                    break;
            }
        } while (menuChoice != '0');
    }

    /// <summary>
    /// visar transaktioner
    /// </summary>
    static void displayTransactions()
    {
        Console.WriteLine("\nTransaktioner:" +
                          "\n------------------------------");

        // Loopar igenom lista och striver ut transaktionshistorik
        for (int i = 0; i < transactions.Count; i++)
        {
            Console.WriteLine(
                $"{i + 1} | Datum: {transactions[i][0]}, Summa: {Convert.ToDouble(transactions[i][1]):#0.00} kr, Information: {transactions[i][2]}");
        }
    }

    /// <summary>
    /// uppdaterar transaktionslistan
    /// </summary>
    static void updateTransactions()
    {
        string fileName = "transactions.txt";
        // Öppnar filen i läsarläge
        StreamReader reader = new StreamReader(fileName);

        string row;
        int transactionsIteration = 0;


        transactions.Clear();

        // Så länge det finns information i filraden
        while ((row = reader.ReadLine()) != null)
        {
            // Skapar array med informationen från raden i filen
            string[] data = row.Split('|');

            // Lägger till lista med transaktionshistorik i listan
            transactions.Add(new List<string> { data[0], data[1], data[2] });

            transactionsIteration++;
        }

        reader.Close();
    }

    /// <summary>
    /// genomför en transaktion
    /// </summary>
    /// <param name="amount">summa</param>
    /// <param name="transactionInformation">typ av transaktion</param>
    static void transaction(double amount, string transactionInformation)
    {
        string fileName = "transactions.txt";
        // Öppnar fil i skrivläge där filen inte skrivs över
        StreamWriter writer = new StreamWriter(fileName, true);

        // Hämtar tid och datum i rätt formattering
        DateTime now = DateTime.Now;
        string formatedDate = now.ToString("yyyy-MM-dd HH:mm:ss");

        // Skriver till fil
        writer.WriteLine($"{formatedDate}|{Math.Round(amount, 2)}|{transactionInformation}");

        writer.Close();
    }

    /// <summary>
    /// hämtar saldo
    /// </summary>
    /// <returns>saldot</returns>
    static double getBalance()
    {
        double balance = 0;

        // Loopar igenom lista och lägger räknar fram saldot
        for (int i = 0; i < transactions.Count; i++)
        {
            balance += Convert.ToDouble(transactions[i][1]);
        }

        return balance;
    }

    /// <summary>
    /// rensar textfil med transaktioner
    /// </summary>
    static void clearTransactions()
    {
        string fileName = "transactions.txt";
        // Öppnar fil i skrivläge där filen skrivs över
        StreamWriter writer = new StreamWriter(fileName);
        writer.Close();
    }
}