using UnityEngine;
using UnityEngine.UI;

public class PurchaseProduct : MonoBehaviour
{
	[SerializeField] private Text selectedProductText;
	[SerializeField] private Player Player;
    [SerializeField] private InputField QuantityField;
    // Start is called before the first frame update

    public void AddProduct()
	{
        var quantity = System.Convert.ToInt32(QuantityField.text);
        Player.AddProducts(selectedProductText.text, quantity);
    }
}
