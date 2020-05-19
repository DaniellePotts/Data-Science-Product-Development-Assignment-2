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

        var product = Player.PlayerData.Products.Find(p => p.ProductType.Equals(SelectedProductType.text));

        if (product != null)
        {
            ProductQuantity.text = product.Quantity.ToString();
        }
    }
    
}
