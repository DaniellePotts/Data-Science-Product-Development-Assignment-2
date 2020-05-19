using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class CustomerBarChart : MonoBehaviour
{
    private RectTransform barChartContainer;
    [SerializeField] private Sprite dotSprite;
    [SerializeField] private RectTransform labelTemplateX;
    [SerializeField] private RectTransform labelTemplateY;
    [SerializeField] private RectTransform dashTemplateX;
    [SerializeField] private RectTransform dashTemplateY;

    private List<GameObject> gameObjectList = new List<GameObject>();
    [SerializeField] private GameLogic GameLogic;
	[SerializeField] private Player Player;
	List<string> productTypes = new List<string> { "Technology", "Home/Furniture", "Kitchen", "Sports/Fitness", "Outdoor" };

	List<Color32> Colors = new List<Color32>()
        {
            new Color32(66, 135, 245, 255),
            new Color32(245, 66, 66, 255),
            new Color32(75, 245, 66, 255),
            new Color32(245, 242, 66, 255),
            new Color32(185, 66, 245, 255)
        };

    public void DisplayProducts()
    {

		List<int> valueList = new List<int>();
		List<string> labels = new List<string>();

        if(Player != null && Player.PlayerData != null && Player.PlayerData.SalesInfo != null && Player.PlayerData.SalesInfo.Sales.Count > 0)
        {
            barChartContainer = transform.Find("barChartContainer_Sales").GetComponent<RectTransform>();

            foreach (var productType in productTypes)
            {
                var productCount = Player.PlayerData.SalesInfo.Sales.FindAll(x => x.ProductType.Equals(productType));
                if(productCount != null && productCount.Count > 0)
                {
                    valueList.Add(productCount.Count());
                    labels.Add(productType);

                }
            }

            foreach (GameObject gameObject in gameObjectList)
            {
                Destroy(gameObject);
            }

            gameObjectList.Clear();
            ShowGraph(valueList, -1, (int _i) => labels[_i], (float _f) => Mathf.RoundToInt(_f).ToString());
        }
		
	}

    private void ShowGraph(List<int> valueList, int maxVisibleValueAmount = -1, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null)
    {
        if (getAxisLabelX == null)
        {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
        }

        if (getAxisLabelY == null)
        {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }

        if (maxVisibleValueAmount <= 0)
        {
            maxVisibleValueAmount = valueList.Count;
        }

        var graphHeight = barChartContainer.sizeDelta.y;
        var graphWidth = barChartContainer.sizeDelta.x;

        float yMaximum = valueList[0];
        float yMinimum = valueList[0];

        var xSize = graphWidth / (maxVisibleValueAmount + 1);

        for (int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++)
        {

            var value = valueList[i];
            if (value > yMaximum)
            {
                yMaximum = value;
            }
            if (value < yMinimum)
            {
                yMinimum = value;
            }
        }

        var yDifference = yMaximum - yMinimum;

        if (yDifference <= 0)
        {
            yDifference = 5f;
        }

        yMaximum = yMaximum + (yDifference * 0.2f);
        yMinimum = yMinimum - (yDifference * 0.2f);

        var xIndex = 0;

        yMinimum = 0;

        for (int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++)
        {
            var xPosition = (xSize + xIndex * xSize) + -46f;
            var yPosition = ((valueList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight;
            var barGameObject = CreateBar(new Vector2(xPosition * 2, yPosition), xSize * 0.9f);
            barGameObject.transform.localPosition += new Vector3(12.61f, -21.3f);

            barGameObject.GetComponent<Image>().color = Colors[i];

            gameObjectList.Add(barGameObject);

            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(barChartContainer);
            labelX.gameObject.SetActive(true);
            labelX.localScale = new Vector3(1, 1, 1);
            labelX.anchoredPosition = new Vector2((barGameObject.transform.localPosition.x + 94), -13f);
            labelX.GetComponent<TextMeshProUGUI>().text = getAxisLabelX(i);
            labelX.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);


            gameObjectList.Add(labelX.gameObject);

            xIndex++;
        }

        var seperatorCount = 10;

        for (var i = 0; i <= seperatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(barChartContainer);
            labelY.gameObject.SetActive(true);
            var labelYOffset = -23f;
            var normalizedValue = i * 1f / seperatorCount;
            labelY.anchoredPosition = new Vector2(-35, (((normalizedValue * graphHeight) + labelYOffset) * 2) + 20f);
            labelY.GetComponent<TextMeshProUGUI>().text = getAxisLabelY(yMinimum + (normalizedValue * (yMaximum - yMinimum)));
            labelY.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
            labelY.anchoredPosition += new Vector2(2.9f, 21.3f);
            gameObjectList.Add(labelY.gameObject);
        }
    }

    private GameObject CreateBar(Vector2 graphPosition, float barWidth)
    {
        GameObject gameObject = new GameObject("bar", typeof(Image));
        gameObject.transform.SetParent(barChartContainer, false);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(graphPosition.x, -7.5f);
        rectTransform.anchoredPosition += new Vector2(29.9f + 14.8f, 21.3f);
        rectTransform.sizeDelta = new Vector2(barWidth * 2, graphPosition.y * 2);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.pivot = new Vector2(.5f, 0f);
        return gameObject;
    }
}
