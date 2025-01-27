using System;
// Namespace som innehåller klasser och metoder för att hantera listor
using System.Collections.Generic;

namespace method
{
    public class Method
    {
        // string är alias för String
        // Denna listan ligger i klassen men utanför alla metoder,
        // detta gör att variabeln "res" går att nå ifrån alla metoder.
        // Nyckelordet static innebär att variabeln tillhör klassen och
        // inte ett objekt, mer om detta när vi jobbar med OOP senare.
        static List<string> res = new List<string>();

        public static void Main(string[] args)
        {
            // Ser till att vi kan skriva ut svenska tecken
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Anropar metoden AddName
            AddName("Johan");

            // Anropar metoden Summa med två inparametrar,
            // lagrar sedan returvärdet i listan res.
            res.Add(Summa(12, 45));

            // Skriver ut innehållet i res
            Print(res);

            // Exempel 2
            // Skapar en ny lista
            List<string> list = new List<string>();

            // Lägger till 3 värden i listan
            list.Add("Pelle");
            list.Add("Sven");
            list.Add("Stina");

            // Skriver ut innehållet i listan
            // listan "list" i Main-metoden skiljer sig från listan list i metoden Print
            // Att ha möjlighet att använda olika listor gör att en metod är mer generell.
            Print(list);
        }

        /// <summary>
        /// Metoden AddName lägger till ett namn till listan
        /// </summary>
        /// <param name="s">Namnet som skall läggas till</param>
        public static void AddName(string s)
        {
            res.Add(s);
        }

        /// <summary>
        /// Metoden Summa summerar två tal och skapar en sträng med uträkningen.
        /// </summary>
        /// <param name="a">Ett heltal</param>
        /// <param name="b">Ett heltal till</param>
        /// <returns>Returnerar uträkning</returns>
        public static String Summa(int a, int b)
        {
            String s = String.Format("Summan av {0} och {1} är {2}", a, b, a + b);
            return s;
            // Går att slå ihop till
            // return String.Format("Summan av {0} och {1} är {2}", a, b, a + b);
        }

        /// <summary>
        /// Metoden print skriver ut innehållet i en lista
        /// </summary>
        /// <param name="list">lista med strängar</param>
        public static void Print(List<string> list)
        {
            // Loopa igenom listan och låta varje komponent lagras i l
            foreach (string l in list)
            {
                Console.WriteLine(l);
            }
        }
    }
}