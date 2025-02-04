namespace u01;

public class Lorry : Vehicle
{
    private int load;
    public Lorry(String regNr, String make, String model, int year, bool forSale, int load) : base(regNr, make, model, year,
        forSale)
    {
        this.load = load;
    }

    public new String ToString()
    {
        String s = base.ToString();
        s += String.Format($"\nMaxlast: {this.load}kg");

        return s;
    }
    
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

        return String.Format($"\t{this.RegNr}\t{this.Make}\t{this.Model}\t{this.YearToString()}\t{fs}\t{this.load}");
    }
}