[System.Serializable]
public class Product
{
	public string Name;
	public int Quantity;
	public int Cost;
	public string ProductType;
    public string Season;

    public Product(string Name, int Quantity, int Cost, string productType)
	{
		this.Name = Name;
		this.Quantity = Quantity;
		this.Cost = Cost;
		this.ProductType = productType;
	}
}
