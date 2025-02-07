namespace u02;

public abstract class Vehicle
{
    // Medlemsvariabler
    private String regNr;
    private String make;
    private String model;
    private int year;
    private bool forSale;

    // Defaultkonstrktor
    public Vehicle()
    {
    }

    // Konstruktor med fem inmatade värden
    public Vehicle(string regNr, string make, string model, int year, bool forSale)
    {
        this.RegNr = regNr;
        this.Make = make;
        this.Model = model;
        this.Year = year;
        this.ForSale = forSale;
    }

    // Properties
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
    /// Modifierar och skriver över standard-metoden ToString
    /// </summary>
    /// <returns>Sträng</returns>
    public override String ToString()
    {
        return String.Format(
            $"\nBilinformation\nReg; {this.RegNr}, {this.Make} {this.Model} [{this.YearToString()}]\n{this.ForsaleToString()}");
    }

    /// <summary>
    /// Omvandlar årtal i int till sträng
    /// </summary>
    /// <returns>Årtal i strängformat</returns>
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

    /// <summary>
    /// Omvandlar char till utvecklad sträng
    /// </summary>
    /// <returns>Sträng</returns>
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

    // Skapar metoden men ger inga instruktioner
    public abstract String ToStringList();
}