namespace u01;

public class Lorry : Vehicle
{
    private int load;
    public Lorry(String regNr, String make, String model, int year, bool forSale, int load) : base(regNr, make, model, year,
        forSale)
    {
        this.load = load;
    }
}