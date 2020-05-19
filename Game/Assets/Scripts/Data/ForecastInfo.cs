using System;
using UnityEngine;

[Serializable]
public class ForecastInfo 
{
	public string date;
	public string Store;
	public string Item;
	public int pred_value;

    public ForecastInfo(string date, string Store, string Item, int pred_value)
	{
		this.date = date;
		this.pred_value = pred_value;

	}

	public override string ToString()
	{
		return "Prediction: " + pred_value + " on date: " + date;
	}
}
