namespace u01;

public class Car
{
    // Medlemsvariabler
    private String regNr;
    private String make;
    private String model;
    private int year;
    private bool forSale;

    public Car()
    {
    }

    // Konstruktor med fem inmatade värden
    public Car(string regNr, string make, string model, int year, bool forSale)
    {
        this.RegNr = regNr;
        this.Make = make;
        this.Model = model;
        this.Year = year;
        this.ForSale = forSale;
    }

    public String RegNr
    {
        get { return regNr; }
        set { regNr = value; }
    }

    public String Make
    {
        get { return make; }
        set { make = value; }
    }

    public String Model
    {
        get { return model; }
        set { model = value; }
    }

    public int Year
    {
        get { return year; }
        set
        {
            if (value < 1900)
            {
                year = -1;
            }
            else
            {
                year = value;
            }
        }
    }

    public bool ForSale
    {
        get { return forSale; }
        set { forSale = value; }
    }

    /// <summary>
    /// Metod som förbereder utskrift av bilinformation
    /// </summary>
    /// <returns>Den formaterade strängen</returns>
    public override String ToString()
    {
        return String.Format(
            $"\nBilinformation\nReg; {this.RegNr} {this.Make} {this.Model} [{this.YearToString()}]\n{this.ForsaleToString()}");
    }

    public String YearToString()
    {
        if (this.year == -1)
        {
            return "Felaktikt årtal";
        }
        else
        {
            return Convert.ToString(year);
        }
    }

    public string ForsaleToString()
    {
        if (this.forSale)
        {
            return "Bilen är till salu";
        }
        else
        {
            return "Bilen är inte till salu";
        }
    }

    public String ToStringList()
    {
        String s = String.Format($"\t{this.RegNr}\t{this.Make}\t{this.Model}\t[{this.YearToString()}]");

        if (this.ForSale)
        {
            s += "\t\tJA";
        }
        else
        {
            s += "\t\tNEJ";
        }

        return s;
    }
}