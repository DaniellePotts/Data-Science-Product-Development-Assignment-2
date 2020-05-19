using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class GameActivity : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI totalCustomersLabel;
    [SerializeField] private TextMeshProUGUI currentSeason;
    [SerializeField] private TextMeshProUGUI bestSellingProduct;
    
    [SerializeField] private GameLogic GameLogic;
    [SerializeField] private Player Player;

    private string CurrentSeason;

    private void OnEnable()
    {
        GameLogic.onBehaviourResponse += GameLogic_onBehaviourResponse;

        CurrentSeason = DateUtils.DetermineSeason(Player.PlayerData.CurrentDate);
    }

    private void OnDisable()
    {
        GameLogic.onBehaviourResponse -= GameLogic_onBehaviourResponse;
    }

    private void GameLogic_onBehaviourResponse(CustomerPurchasing customers)
    { 
        setLabels();
    }

    void setLabels()
    {
        currentSeason.text = DateUtils.DetermineSeason(Player.PlayerData.CurrentDate);

        totalCustomersLabel.text = CalculateCustomers().ToString();


        bestSellingProduct.text = BestSellingProduct();
    }

    int CalculateCustomers()
    {
        var totalCustomers = 0;

        Player.PlayerData.Customers.ForEach((customer) =>
        {
            totalCustomers += customer.Count;
        });

        return totalCustomers;
    }

    string BestSellingProduct()
    {
        var productTypes = new List<string> { "Technology", "Home/Furniture", "Kitchen", "Sports/Fitness", "Outdoor" };
        var bestSelling = string.Empty;
        var max = 0;
        if (Player != null && Player.PlayerData != null && Player.PlayerData.SalesInfo != null && Player.PlayerData.SalesInfo.Sales != null)
        {
            if (Player.PlayerData.SalesInfo.Sales.Count > 0)
            {
                productTypes.ForEach((productType) =>
                {
                    var productCount = Player.PlayerData.SalesInfo.Sales.FindAll(p => p.ProductType == productType).Count();

                    if(max < productCount)
                    {
                        max = productCount;
                        bestSelling = productType;
                    }

                });
            }
        }


        return bestSelling;
    }
}
