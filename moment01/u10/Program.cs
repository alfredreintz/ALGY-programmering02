namespace u10;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Radien på cirkel (m): ");
        double radius = Convert.ToDouble(Console.ReadLine());
        
        Console.WriteLine($"Omkrets: {circumference(radius):#0.00}m");
        Console.WriteLine($"Area: {area(radius):#0.00}m^2");
    }

    /// <summary>
    /// räknar ut omkretsen på en cirkel
    /// </summary>
    /// <param name="radius">radien på cirkeln</param>
    /// <returns>omkrets</returns>
    static double circumference(double radius)
    {
        return radius * 2 * Math.PI;
    }    
    
    /// <summary>
    /// räknar ut arean på en cirkel
    /// </summary>
    /// <param name="radius">radien på cirkeln</param>
    /// <returns>area</returns>
    static double area(double radius)
    {
        return Math.Pow(radius, 2) * Math.PI;
    }
}