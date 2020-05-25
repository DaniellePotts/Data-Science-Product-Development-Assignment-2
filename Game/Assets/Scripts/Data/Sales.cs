
[System.Serializable]
public class Sales
{
    public int Month;
    public int Year;
    public int Day;
    public string Date;
    public string Season;
    public int TotalSales;
    public string ProductType;

    public override string ToString()
    {
        return $"Month: {Month} Day {Day} Year {Year}\n Season: {Season} Total Sales - {TotalSales}";
    }
}
