namespace u02;

// Klass med arv från fordonsklass
public class Lorry : Vehicle
{
    // Medlemsvariabel
    private int load;

    // Konstruktor med sec inmatade värdern varav fem första från konstruktor i förälderklassen
    public Lorry(String regNr, String make, String model, int year, bool forSale, int load) : base(regNr, make, model,
        year,
        forSale)
    {
        this.Load = load;
    }

    // Property
    public int Load
    {
        get { return load; }
        set { load = value; }
    }

    /// <summary>
    /// Skriver över och modifierar standardmetod
    /// </summary>
    /// <returns>Modifierad sträng</returns>
    public override String ToString()
    {
        String s = base.ToString();
        s += String.Format($"\nMaxlast: {this.load}kg");

        return s;
    }

    /// <summary>
    /// Skriver över metoden och modifierar den
    /// </summary>
    /// <returns>Modifierad sträng</returns>
    public override String ToStringList()
    {
        String fs = "";

        if (this.ForSale)
        {
            fs += "\t\tJa";
        }
        else
        {
            fs += "\t\tNEJ";
        }

        return String.Format(
            $"\t{this.RegNr}\t{this.Make}\t{this.Model}\t[{this.YearToString()}] {fs}\t\tMaclast: {this.Load}kg");
    }
}