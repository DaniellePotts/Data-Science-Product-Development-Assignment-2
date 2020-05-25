using UnityEngine;
using UnityEngine.UI;

public class PurchasePopup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Player Player;
    [SerializeField] private Text SelectedProductType;
    [SerializeField] private Text ProductName;
    [SerializeField] private Text ProductQuantity;

    public void SetLabels()
    {
        ProductName.text = SelectedProductType.text;

        var products = Player.PlayerData.Products.FindAll(p => p.ProductType.Equals(SelectedProductType.text));

        if (products != null)
        {
            var quantity = 0;

            products.ForEach((product)=>{
                quantity +=product.Quantity;
            });

            ProductQuantity.text = quantity.ToString();
        }
    }
    
}
