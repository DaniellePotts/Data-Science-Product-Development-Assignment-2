using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    public PlayerData PlayerData;

    public delegate void UpdatePlayerMetrics(double Money, int StoreCount);
    public event UpdatePlayerMetrics onMetricUpdate;

    private void Start()
    {
        CreatePlayer();
    }

    private void Update()
    {
        PlayerData.Money += 10;
        PlayerData.StoreCount += 10;

        onMetricUpdate?.Invoke(PlayerData.Money, PlayerData.StoreCount);
    }


    public void CreatePlayer()
    {
        PlayerData = new PlayerData
        {
            CurrentDate = "1970-1-1",
            Money = 100,
            StoreCount = 1,
            Level = 1
        };

        var sales = new List<Sales>();

        var days = 365;
        var date = "1970-1-1";

        var productTypes = new List<string> { "Technology", "Home/Furniture", "Kitchen", "Sports/Fitness", "Outdoor" };

        var random = new System.Random();
       
        for (var i = 0; i < days; i++)
        {
            var totalSales = random.Next(0, 500);
            var type = productTypes[random.Next(0, productTypes.Count)];

            while (string.IsNullOrEmpty(type))
            {
                type = productTypes[random.Next(0, productTypes.Count)];
            }

            sales.Add(new Sales
            {
                Date = date,
                Season = DateUtils.DetermineSeason(date),
                Month = DateUtils.GetMonth(date),
                Day = DateUtils.GetDay(date),
                Year = DateUtils.GetYear(date),
                TotalSales = totalSales,
                ProductType = type
            }) ;

            date = DateUtils.AddDay(date, 10);
        }

        var seasons= new List<string>(){"Spring","Fall","Summer","Winter"};

        for(var i=0;i<productTypes.Count;i++){
            var quantity = random.Next(0, 15);
            var season = seasons[random.Next(0, seasons.Count)];

            PlayerData.Products.Add(new Product(quantity,0,productTypes[i], season));
        }

        PlayerData.SalesInfo = new SalesInfo();
        PlayerData.SalesInfo.Sales = sales;
    }

    public bool CheckIfProductExists(string productLine)
    {
        if(PlayerData.Products.Count > 0)
        {
            var product = PlayerData.Products.Find(p => p.ProductType.Equals(productLine));
            return product != null ;
        }

        return false;
    }

    public void AddProducts(string productLine, int quantity)
    {
        if (CheckIfProductExists(productLine))
        {
            for(var i = 0; i < PlayerData.Products.Count; i++)
            {
                if (PlayerData.Products[i].ProductType.Equals(productLine))
                {
                    PlayerData.Products[i].Quantity += quantity;
                }
            }
        }
        else
        {
            PlayerData.Products.Add(new Product( quantity, 100, productLine, "Winter"));
        }
    }

    public void DecreaseQuantity(string productLine, int quantity)
    {
        if (CheckIfProductExists(productLine))
        {
            for (var i = 0; i < PlayerData.Products.Count; i++)
            {
                if (PlayerData.Products[i].ProductType.Equals(productLine))
                {
                    if (quantity >= PlayerData.Products[i].Quantity)
                    {
                        PlayerData.Products.Remove(PlayerData.Products[i]);
                    }
                    else
                    {
                        PlayerData.Products[i].Quantity -= quantity;
                    }
                }
            }
        }
    }

    public void Save()
    {
        GameSaveUtils.SaveGame(this.PlayerData);
        Debug.Log("Saved!");
    }

    public void Load(UnityEngine.UI.Text fileName)
    {
        this.PlayerData = GameSaveUtils.LoadGame(fileName.text);
    }
}