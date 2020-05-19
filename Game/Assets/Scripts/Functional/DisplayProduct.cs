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

    // Update is called once per frame
    void Update()
    {
        
    }

    void PopulateList()
	{
		PurchaseableProducts.Add(new Product("Hat", 3, 100, "Tech"));
		PurchaseableProducts.Add(new Product("Handwash", 2, 5, "Home"));
		PurchaseableProducts.Add(new Product("Book", 3, 4, "Home"));
	}

    void AddCell(Product product)
	{
		var temp = Instantiate(ProductCellPrefab);
		temp.transform.parent = gameObject.transform;
		var pc = temp.GetComponent<ProductCell>();

		pc.ProductName.text = product.Name;
	}
}
