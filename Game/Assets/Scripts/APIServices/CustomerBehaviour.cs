using System.Collections.Generic;
using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    public delegate void GetCustomerBehaviour(CustomerPurchasing products);
    public event GetCustomerBehaviour onGetCustomerBehaviour;
    
    [SerializeField] private APIClient ApiClient;

    public void CallCustomerBehaviour(string season)
    {
        var ShopperSimulationUrl = "http://localhost:3001/models/shoppers/" + season;
		ApiClient.CallApi(ShopperSimulationUrl, null, "GET");
        ApiClient.onGetApiResponse += ApiClient_onGetApiResponse;
    }

    private void ApiClient_onGetApiResponse(string response)
    {
        ApiClient.onGetApiResponse -= ApiClient_onGetApiResponse;
        var customers = JsonUtility.FromJson<CustomerPurchasing>("{\"Products\":" + response + "}");
        onGetCustomerBehaviour?.Invoke(customers);
    }
}

public class CustomerPurchasing
{
    public ProductPurchasingInfo[] Products;
}

[System.Serializable]
public class ProductPurchasingInfo
{
    public string Season;
    public string ProductType;

    public ProductPurchasingInfo(string Season, string ProductType)
    {
        this.Season = Season;
        this.ProductType = ProductType;
    }

    public override string ToString()
    {
        return "Product: " + ProductType + " in season: " + Season;
    }
}

