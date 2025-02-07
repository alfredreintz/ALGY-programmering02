namespace u02;

public class Car : Vehicle
{
    // Konstruktor ärvt från förälderklassen
    public Car(String regNr, String make, String model, int year, bool forSale) : base(regNr, make, model, year,
        forSale)
    {
    }

    /// <summary>
    /// Skriver över metod som modifieras
    /// </summary>
    /// <returns>Modifierad sträng</returns>
    public override String ToStringList()
    {
        String s = String.Format($"\t{this.RegNr}\t{this.Make}\t{this.Model}\t[{this.YearToString()}]");

        if (this.ForSale)
        {
            s += "\t\tJa";
        }
        else
        {
            s += "\t\tNEJ";
        }

        return s;
    }
}