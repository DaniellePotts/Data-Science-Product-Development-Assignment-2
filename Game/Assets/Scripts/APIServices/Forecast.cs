using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Forecast : MonoBehaviour
{
	public delegate void GetForecast(Forecasts forecast);
	public event GetForecast onGetForecast;
    [SerializeField] private Player Player;
	private string ForecastUrl = "http://localhost:3001/models/getPrediction"; //temp url

	private List<ForecastInfo> forecasts = new List<ForecastInfo>();
	[SerializeField] private APIClient ApiClient;

	public void CallForecast()
	{
        var sales = new List<string>();
        var data = Player.PlayerData.SalesInfo.Sales.Take(100).ToList();       

        data.ForEach((sale) =>
        {
            sales.Add(JsonUtility.ToJson(sale));
        });

		ApiClient.CallApi(ForecastUrl, sales, "POST");
		ApiClient.onGetApiResponse += ApiClient_onGetApiResponse;
	}

    public void CallForecastSeason(string season)
    {
        var sales = Player.PlayerData.SalesInfo.Sales.Where(p => p.Season == season).ToList().Take(100).ToList();       
        var parsedSales = new List<string>();

        sales.ForEach((sale) =>
        {
            parsedSales.Add(JsonUtility.ToJson(sale));
        });

        ApiClient.CallApi(ForecastUrl, parsedSales, "POST");
        ApiClient.onGetApiResponse += ApiClient_onGetApiResponse;
    }

    private void ApiClient_onGetApiResponse(string response)
	{
		ApiClient.onGetApiResponse -= ApiClient_onGetApiResponse;
		var forecasts = JsonUtility.FromJson<Forecasts>("{\"forecasts\":" + response + "}");
		
		onGetForecast?.Invoke(forecasts);
	}


    public void CallForecastProductType(string productType)
    {
        var sales = Player.PlayerData.SalesInfo.Sales.Where(p => p.ProductType == productType).ToList().Take(100).ToList();       
        var parsedSales = new List<string>();

        sales.ForEach((sale) =>
        {
            parsedSales.Add(JsonUtility.ToJson(sale));
        });

        ApiClient.CallApi(ForecastUrl, parsedSales, "POST");
        ApiClient.onGetApiResponse += ApiClient_onGetApiResponse;
    }
}