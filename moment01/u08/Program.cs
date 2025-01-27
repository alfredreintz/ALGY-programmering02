namespace u05;

class Program
{
    static void Main(string[] args)
    {
        bool correct = false;
        
        do
        {
            Console.WriteLine("Välj en char");
            char c = Console.ReadKey().KeyChar;
            
            switch (c)
            {
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                    Console.WriteLine("\nDu valde rätt!!");
                    correct = true;
                    break;
                default:
                    Console.WriteLine("\nError: Du valde nog fel karaktär");
                    break;
            }
        } while (correct == false);
    }
}