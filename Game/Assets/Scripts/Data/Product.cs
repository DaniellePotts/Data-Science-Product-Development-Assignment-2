[System.Serializable]
public class Product
{
	public int Quantity;
	public int Cost;
	public string ProductType;
    public string Season;

    public Product(int Quantity, int Cost, string productType, string season)
	{
		this.Quantity = Quantity;
		this.Cost = Cost;
		this.ProductType = productType;
		this.Season = season;
	}
}
