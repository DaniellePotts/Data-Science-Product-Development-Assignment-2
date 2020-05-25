using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    [SerializeField] CustomerBehaviour CustomerBehaviour;
    public delegate void CustomerBehaviourResponse(CustomerPurchasing customers);
    public event CustomerBehaviourResponse onBehaviourResponse;

    [SerializeField] Text CurrentDate;
    [SerializeField] Player player;

    [SerializeField] private GameObject LoadingScreen;

    void OnEnable()
    {
        LoadingScreen.SetActive(true);
        InvokeRepeating("TriggerAI", 0, 10);
    }

    private void Customer_onGetCustomerBehaviour(CustomerPurchasing customers)
    {
        CustomerBehaviour.onGetCustomerBehaviour -= Customer_onGetCustomerBehaviour;
        AddSales(customers);
        onBehaviourResponse?.Invoke(customers);
        LoadingScreen.SetActive(false);
    }

    void TriggerAI()
    {
        CustomerBehaviour.CallCustomerBehaviour(DateUtils.DetermineSeason(player.PlayerData.CurrentDate));
        CustomerBehaviour.onGetCustomerBehaviour += Customer_onGetCustomerBehaviour;
    }

    void AddSales(CustomerPurchasing customers)
    {
        var sales = new Sales();

		var productTypes = new List<string> { "Technology", "Home/Furniture", "Kitchen", "Sports/Fitness", "Outdoor" };

		productTypes.ForEach((productType) =>
		{
			var products = new List<ProductPurchasingInfo>();

			for(var i=0;i<customers.Products.Length;i++)
			{
				if (customers.Products[i].ProductType.Equals(productType))
				{
					products.Add(customers.Products[i]);
				}
			}

            if(products.Count > 0)
			{
				sales.TotalSales = products.Count;
				sales.Season = DateUtils.DetermineSeason(player.PlayerData.CurrentDate);
				sales.Month = DateUtils.GetMonth(player.PlayerData.CurrentDate);
				sales.Day = DateUtils.GetDay(player.PlayerData.CurrentDate);
				sales.Year = DateUtils.GetYear(player.PlayerData.CurrentDate);
				sales.Date = DateUtils.DateToText(player.PlayerData.CurrentDate);
				sales.ProductType = productType;
				player.PlayerData.SalesInfo.Sales.Add(sales);
			}
		});

		
        player.PlayerData.Customers.Add(new Customer
        {
            Count = customers.Products.Length,
            Season = DateUtils.DetermineSeason(player.PlayerData.CurrentDate)
        });

    }
}
