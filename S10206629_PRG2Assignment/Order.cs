// See https://aka.ms/new-console-template for more information
using S10206629_PRG2Assignment;
using System;
using System.Collections.Generic;

 class Order
{
    public int Id { get; set; }
    public DateTime TimeReceived { get; set; }
    public DateTime? TimeFulfilled { get; set; }
    public List<IceCream> IceCreamList { get; set; }

    // Constructors
    public Order(){}

    public Order(int id, DateTime timeReceived)
    {
        Id = id;
        TimeReceived = timeReceived;
        IceCreamList = new List<IceCream>();
    }

    // Class methods
    public void ModifyIceCream(int iceCreamIndex)
    {
        // to edit later when question is being done
    }

    public void AddIceCream(IceCream iceCream)
    {
        IceCreamList.Add(iceCream);
    }

    public void DeleteIceCream(int iceCreamIndex)
    {
        IceCreamList.RemoveAt(iceCreamIndex);
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

