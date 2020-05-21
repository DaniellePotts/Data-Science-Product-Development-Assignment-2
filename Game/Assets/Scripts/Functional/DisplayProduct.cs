using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayProduct : MonoBehaviour
{
	private List<Product> PurchaseableProducts = new List<Product>();
	[SerializeField] private GameObject ProductCellPrefab;
	//[SerializeField] private GameObject parentPrefab;
    // Start is called before the first frame update
    void Start()
    {
		PopulateList();

		for (var i = 0; i < PurchaseableProducts.Count; i++)
		{
			AddCell(PurchaseableProducts[i]);
		}
	}

    void PopulateList()
	{
		PurchaseableProducts.Add(new Product(3, 100, "Technology", "Winter"));
		PurchaseableProducts.Add(new Product( 2, 5, "Outdoor", "Summer"));
		PurchaseableProducts.Add(new Product(3, 4, "Sports/Fitness","Winter"));
	}

    void AddCell(Product product)
	{
		var temp = Instantiate(ProductCellPrefab);
		temp.transform.parent = gameObject.transform;
		var pc = temp.GetComponent<ProductCell>();

		pc.ProductName.text = product.ProductType;
	}
}
