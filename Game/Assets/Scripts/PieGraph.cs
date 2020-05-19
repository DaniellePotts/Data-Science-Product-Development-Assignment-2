using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PieGraph : MonoBehaviour
{

    public float[] values;
    public Color[] wedgeColors;
    public Image wedgePrebab;

    [SerializeField] private Player Player;
    [SerializeField] private GameObject keyBox;
    [SerializeField] private GameObject keyListObject;

    // Start is called before the first frame update
    void OnEnable()
    {
        var values = CompareSales();
        MakeGraph(values);
    }

    void MakeGraph(Dictionary<string, float> realValues) //these are the future values for when we import real data
    {
        var total = 0f;
        var zRotation = 0f;

        foreach (KeyValuePair<string, float> value in realValues)


        {
            total += value.Value;
  
        }

        var index = 0;


        foreach (KeyValuePair<string, float> value in realValues)
        {
            Image wedgeImage = Instantiate(wedgePrebab) as Image;
            wedgeImage.transform.SetParent(transform, false);
            
            wedgeImage.color = wedgeColors[index];
            wedgeImage.fillAmount = value.Value / total;

            wedgeImage.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, zRotation));
            zRotation -= wedgeImage.fillAmount * 360f;
            GameObject keyLabel = Instantiate(keyListObject) as GameObject;

            keyLabel.transform.SetParent(keyBox.transform, false);
            keyLabel.GetComponentInChildren<TMP_Text>().text = value.Key;
            keyLabel.GetComponentInChildren<Image>().color = wedgeColors[index];
            keyLabel.SetActive(true);
            index++;  
        }
    }

    Dictionary<string, float> CompareSales()
    {
        var productTypes = new List<string> { "Technology", "Home/Furniture", "Kitchen", "Sports/Fitness", "Outdoor" };
        var counts = new Dictionary<string, float>();
        var percentages = new Dictionary<string, float>();

        var total = 0;

        if (Player != null && Player.PlayerData != null)
        {
            if (Player.PlayerData.SalesInfo != null)
            {
                if (Player.PlayerData.SalesInfo.Sales.Count > 0)
                {
                    Debug.Log("here");

                    productTypes.ForEach((productType) =>
                    {
                        var count = Player.PlayerData.SalesInfo.Sales.FindAll(p => p.ProductType.Equals(productType)).Count();

                        total += count;
                        counts.Add(productType, count);
                    });
                }

            }
        }


        foreach(KeyValuePair<string, float> count in counts)
        {
           var precentage = (count.Value / 100);

            percentages.Add(count.Key, precentage);
        }

        Debug.Log("Percentages: " + percentages.Count);
        return percentages;
    }
}
