using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CodeMonkey.Utils;
using TMPro;

public class WindowGraph : MonoBehaviour
{
    private RectTransform graphContainer;
    [SerializeField] private Sprite dotSprite;
	[SerializeField]private RectTransform labelTemplateX;
	[SerializeField]private RectTransform labelTemplateY;
	[SerializeField] private RectTransform dashTemplateX;
	[SerializeField] private RectTransform dashTemplateY;
    [SerializeField] private GameObject ZeroLineMarker;
	[SerializeField] private GameObject loadingPopup;
    [SerializeField] private GameObject toolTip;
    [SerializeField] private Text toolTipText;
    [SerializeField] private GameObject noProductsPopup;
	private List<GameObject> gameObjectList;

    [SerializeField] private TextMeshProUGUI titleLabel;

	[SerializeField] private Forecast forecast;

    public void DisplayProductType(string productType)
    {
        DestroyGameObjects();
        titleLabel.text = "Product Forecast for Product Type - " + productType;
        forecast.CallForecastProductType(productType);
        forecast.onGetForecast += Forecast_onGetForecast;
        noProductsPopup.SetActive(false);
        ZeroLineMarker.SetActive(false);
        loadingPopup.SetActive(true);    
    }

    public void DisplayBySeason(string season)
    {
        DestroyGameObjects();
        titleLabel.text = "Product Forecast for Season - " + season; 
        forecast.CallForecastSeason(season);
        forecast.onGetForecast += Forecast_onGetForecast;
        noProductsPopup.SetActive(false);
        ZeroLineMarker.SetActive(false);
        loadingPopup.SetActive(true);
    }

    private void OnEnable()
	{
        DestroyGameObjects();
        titleLabel.text = "Product Forecast";
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
		forecast.CallForecast();
		forecast.onGetForecast += Forecast_onGetForecast;

        noProductsPopup.SetActive(false);
		loadingPopup.SetActive(true);
	}

	private void Forecast_onGetForecast(Forecasts response)
	{
        DisplayForecast(response);
        forecast.onGetForecast -= Forecast_onGetForecast;
		loadingPopup.SetActive(false);
	}

    void DisplayForecast(Forecasts forecasts)
    {
        List<float> valueList = new List<float>();
        gameObjectList = new List<GameObject>();

        for (var i = 0; i < forecasts.forecasts.Length; i++)
        {
            valueList.Add(forecasts.forecasts[i].pred_value);
        }

        if(valueList.Count > 0){
            ShowGraph(valueList, -1, (float _i) => "Month " + (_i), (float _f) => "$" + _f);
        }else{
            noProductsPopup.SetActive(true);
        }
    }

	private GameObject CreateDot(Vector2 anchoredPosition, int value)
	{
		var gameObject = new GameObject("dot", typeof(Image));
		gameObject.transform.SetParent(graphContainer, false);
        var rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.GetComponent<Image>().sprite = dotSprite;
        rectTransform.anchoredPosition = anchoredPosition;
		gameObject.transform.localScale = new Vector2(5, 5);
		rectTransform.sizeDelta = new Vector2(1, 1);
		rectTransform.anchorMin = new Vector2(0, 0);
		rectTransform.anchorMax = new Vector2(0, 0);

        var eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;

        entry.callback.AddListener((eventData) => { DisplayTooltip(rectTransform.transform.position, value); });
        eventTrigger.triggers.Add(entry);

        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;

        exit.callback.AddListener((eventData) => { toolTip.SetActive(false); });
        eventTrigger.triggers.Add(exit);

        return gameObject;
	}

    private void DisplayTooltip(Vector3 position, int value)
    {
        var rectTransform = toolTipText.GetComponent<RectTransform>();
        toolTipText.text = "$" + value.ToString();
        toolTip.transform.position = position + new Vector3(rectTransform.rect.width, 0);
        toolTip.SetActive(true);
    }

    private void DestroyGameObjects()
    {
        if(gameObjectList != null && gameObjectList.Count > 0){
            foreach (GameObject gameObject in gameObjectList)
            {
                Destroy(gameObject);
            }
        }
    }

	private void ShowGraph(List<float> valueList, int maxVisibleValueAmount = -1, Func<float, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null)
	{
        var negativeValues = valueList.Find(x => x < 0);

        var hasNegative = negativeValues >= 0 ? false : true;

        if(getAxisLabelX == null)
		{
			getAxisLabelX = delegate (float _i) { return _i.ToString(); };
		} 

		if (getAxisLabelY == null)
		{
			getAxisLabelY = delegate (float _f)
            {
				var yLabel = _f.ToString();
               
				return yLabel.Contains(".") ? yLabel.Split('.')[0] : yLabel;

			};
		}

		if (maxVisibleValueAmount <= 0)
		{
			maxVisibleValueAmount = valueList.Count;
		}

        DestroyGameObjects();

		gameObjectList.Clear();

		var graphHeight = graphContainer.sizeDelta.y;
		var graphWidth = graphContainer.sizeDelta.x;

		float yMaximum = valueList[0];
		float yMinimum = valueList[0];

		var xSize = graphWidth / (maxVisibleValueAmount + 1);

		for (int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++)
		{
            
			var value = valueList[i];
            if(value > yMaximum)
			{
				yMaximum = value;
			}
            if( value < yMinimum)
			{
				yMinimum = value;
			}
		}

		var yDifference = yMaximum - yMinimum;

        if(yDifference <= 0)
		{
			yDifference = 5f;
		}

		yMaximum = yMaximum + (yDifference * 0.2f);
        yMinimum = yMinimum - (yDifference * 0.2f);

		var xIndex = 0;

        //yMinimum = 0;
        GameObject lastDotGameObject = null;
		for (int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++)
		{
			var xPosition = (xSize + xIndex * xSize) - 44f;
			var yPosition = ((valueList[i] - yMinimum )/ (yMaximum - yMinimum)) * graphHeight;
			GameObject dotGameObject = CreateDot(new Vector2((xPosition + 6f), yPosition), (int)valueList[i]);
			gameObjectList.Add(dotGameObject);
			if (lastDotGameObject != null)
			{
				var dotConnectonGameObject = CreateDotConnection(lastDotGameObject.GetComponent<RectTransform>().anchoredPosition, dotGameObject.GetComponent<RectTransform>().anchoredPosition);
				gameObjectList.Add(dotConnectonGameObject);
			}
			lastDotGameObject = dotGameObject;

			RectTransform labelX = Instantiate(labelTemplateX);
			labelX.SetParent(graphContainer);
			labelX.gameObject.SetActive(true);
			labelX.anchoredPosition = new Vector2((xPosition), -10f);

			labelX.GetComponent<Text>().text = getAxisLabelX(i);
			gameObjectList.Add(labelX.gameObject);

			
			xIndex++;
		}

		var seperatorCount = 10;

        var maxNegativeLabelValue = 0f;
        var zeroLinePosY = 0f;

        for (var i = 0; i <= seperatorCount; i++)
		{
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer);
            labelY.gameObject.SetActive(true);
            var normalizedValue = i * 1f / seperatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
            labelY.transform.localPosition += new Vector3(-15, 0);
            var yLabel = getAxisLabelY(yMinimum + (normalizedValue * (yMaximum - yMinimum)));
            yLabel = yLabel.Contains(".") ? yLabel.Split('.')[0] : yLabel;

            var cleanedLabel = CleanLabel(yLabel);

            if (i == 0)
            {
             
                    maxNegativeLabelValue = cleanedLabel;
                
            }
            else
            {
                if (cleanedLabel < 0)
                {
 maxNegativeLabelValue = Mathf.Max(maxNegativeLabelValue, cleanedLabel);
                    zeroLinePosY = labelY.transform.position.y;

                    var eventTrigger = ZeroLineMarker.AddComponent<EventTrigger>();

                    EventTrigger.Entry entry = new EventTrigger.Entry();
                    entry.eventID = EventTriggerType.PointerEnter;

                    entry.callback.AddListener((eventData) => { DisplayTooltip(ZeroLineMarker.transform.position, 0); });
                    eventTrigger.triggers.Add(entry);

                    EventTrigger.Entry exit = new EventTrigger.Entry();
                    exit.eventID = EventTriggerType.PointerExit;

                    exit.callback.AddListener((eventData) => { toolTip.SetActive(false); });
                    eventTrigger.triggers.Add(exit);
                    
                   
                }
            }
           

            labelY.GetComponent<Text>().text = yLabel;
            gameObjectList.Add(labelY.gameObject);
        }

        if(maxNegativeLabelValue < 0)
        {
            var aboveZero = valueList.FindAll(v => v > 0);

            Debug.Log(aboveZero.Count);
            if(aboveZero.Count > 0)
            {
                ZeroLineMarker.transform.position = new Vector3(ZeroLineMarker.transform.position.x, zeroLinePosY + 20f);
                ZeroLineMarker.SetActive(true);
            }
        }
    }

    private float CleanLabel(string label)
    {
        var finalLabel = string.Empty;
        if (label.Contains("$"))
        {
            finalLabel = label.Split('$')[1];
        }
        else
        {
            finalLabel = label;
        }

        return float.Parse(finalLabel);
    }

	private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
	{
		GameObject gameObject = new GameObject("dotConnection", typeof(Image));
		gameObject.transform.SetParent(graphContainer, false);
		gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 127);
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
}
