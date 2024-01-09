// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

public class Order
{
    public int Id;
    public DateTime TimeReceived;
    public DateTime? TimeFulfilled;
    public List<IceCream> IceCreamList;   

    public Order()
    {
    }

    public Order(int id, DateTime timeReceived)
    {
        Id = id;
        TimeReceived = timeReceived;
        IceCreamList = new List<IceCream>();
    }
    public void ModifyIceCream(int iceCreamIndex)
    {
    }

    public void AddIceCream(IceCream iceCream)
    {
        IceCreamList.Add(iceCream);
    }

    public void DeleteIceCream(int iceCreamIndex)
    {
    }

    public double CalculateTotal()
    {
        return 0.0;
    }

    public override string ToString()
    {
        return $"Order ID: {Id}, Time Received: {TimeReceived}, Time Fulfilled: {TimeFulfilled}, Total: {CalculateTotal()}";
    }
}

