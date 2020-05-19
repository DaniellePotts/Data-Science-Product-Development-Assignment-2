using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public string Name;
    public double Money;
    public int Level;
    public int StoreCount;
    public string CurrentDate;

    public List<Store> Stores = new List<Store>();
    public List<Product> Products = new List<Product>();
    public SalesInfo SalesInfo = new SalesInfo();
    public List<Customer> Customers = new List<Customer>(); 
}