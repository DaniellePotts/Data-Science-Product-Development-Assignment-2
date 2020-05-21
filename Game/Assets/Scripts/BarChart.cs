using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class BarChart : MonoBehaviour
{
	private RectTransform graphContainer;
	[SerializeField] private Sprite dotSprite;
	[SerializeField] private RectTransform labelTemplateX;
	[SerializeField] private RectTransform labelTemplateY;
	[SerializeField] private RectTransform dashTemplateX;
	[SerializeField] private RectTransform dashTemplateY;

	private List<GameObject> gameObjectList;
    [SerializeField] private Player Player;

    List<Color32> Colors = new List<Color32>()
        {
            new Color32(66, 135, 245, 255),
            new Color32(245, 66, 66, 255),
            new Color32(75, 245, 66, 255),
            new Color32(245, 242, 66, 255),
            new Color32(185, 66, 245, 255)
        };

    private void Awake()
	{
		graphContainer = transform.Find("graphContainerStocks").GetComponent<RectTransform>();

		var productTypes = new List<string> { "Technology", "Home/Furniture", "Kitchen", "Sports/Fitness", "Outdoor" };

       	var valueList = new List<int>();
		var labels = new List<string>();

		Debug.Log(Player.PlayerData.Products[0].ProductType);

        for(var i = 0; i < productTypes.Count; i++)
        {
           var product = Player.PlayerData.Products.FindAll(p => p.ProductType == productTypes[i]);
			if(product != null){
				var totalQuantity = 0;

				product.ForEach((p)=> {
					totalQuantity += p.Quantity;
				});

				valueList.Add(totalQuantity);
				labels.Add(productTypes[i]);
			}
		}

		Debug.Log(valueList[0]);
        gameObjectList = new List<GameObject>();

		ShowGraph(valueList, -1, (int _i) => labels[_i], (float _f) => "" + Mathf.RoundToInt(_f));
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

		foreach (GameObject gameObject in gameObjectList)
		{
			Destroy(gameObject);
		}

		gameObjectList.Clear();

		var graphHeight = graphContainer.sizeDelta.y;
		var graphWidth = graphContainer.sizeDelta.x;

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
			var barGameObject = CreateBar(new Vector2(xPosition, yPosition), xSize * 0.9f);
            barGameObject.transform.localPosition += new Vector3(12.61f, -21.3f);

            barGameObject.GetComponent<Image>().color = Colors[i];

            gameObjectList.Add(barGameObject);
			
			RectTransform labelX = Instantiate(labelTemplateX);
			labelX.SetParent(graphContainer);
			labelX.gameObject.SetActive(true);
			labelX.anchoredPosition = new Vector2((xPosition + 5f), -33f);

			labelX.GetComponent<Text>().text = getAxisLabelX(i);
            labelX.GetComponent<Text>().color = new Color32(0, 0, 0, 255);

            gameObjectList.Add(labelX.gameObject);

			xIndex++;
		}

		var seperatorCount = 10;

		for (var i = 0; i <= seperatorCount; i++)
		{
			RectTransform labelY = Instantiate(labelTemplateY);
			labelY.SetParent(graphContainer);
			labelY.gameObject.SetActive(true);
            var labelYOffset = -23f;
			var normalizedValue = i * 1f / seperatorCount;
			labelY.anchoredPosition = new Vector2(-35, (normalizedValue * graphHeight) + labelYOffset);
			labelY.GetComponent<Text>().text = getAxisLabelY(yMinimum + (normalizedValue * (yMaximum - yMinimum)));
            labelY.GetComponent<Text>().color = new Color32(0, 0, 0, 255);
            gameObjectList.Add(labelY.gameObject);

			
		}
	}

	private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
	{
		GameObject gameObject = new GameObject("dotConnection", typeof(Image));
		gameObject.transform.SetParent(graphContainer, false);
		gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 48);
		RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
		Vector2 dir = (dotPositionB - dotPositionA).normalized;
		float distance = Vector2.Distance(dotPositionA, dotPositionB);
		rectTransform.anchorMin = new Vector2(0, 0);
		rectTransform.anchorMax = new Vector2(0, 0);
		rectTransform.sizeDelta = new Vector2(distance, 3f);
		rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
		rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
		return gameObject;
	}

    private GameObject CreateBar(Vector2 graphPosition, float barWidth)
	{
		GameObject gameObject = new GameObject("bar", typeof(Image));
		gameObject.transform.SetParent(graphContainer, false);
		RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
		rectTransform.anchoredPosition = new Vector2(graphPosition.x, -7.5f);
		rectTransform.sizeDelta = new Vector2(barWidth, graphPosition.y);
		rectTransform.anchorMin = new Vector2(0, 0);
		rectTransform.anchorMax = new Vector2(0, 0);
		rectTransform.pivot = new Vector2(.5f, 0f);
		return gameObject;
	}
}
