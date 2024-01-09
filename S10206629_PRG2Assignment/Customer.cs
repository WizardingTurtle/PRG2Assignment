// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

public class Customer
{
    public string Name;
    public int MemberId;
    public DateTime Dob;
    public Order CurrentOrder;
    private List<Order> OrderHistory;
    public PointCard Rewards;

    public Customer()
    {
        OrderHistory = new List<Order>();
        Rewards = new PointCard();
    }

    public Customer(string name, int memberId, DateTime dob)
    {
        Name = name;
        MemberId = memberId;
        Dob = dob;
        OrderHistory = new List<Order>();
        Rewards = new PointCard();
    }


    public Order MakeOrder()
    {
        CurrentOrder = new Order();
        return CurrentOrder;
    }

    public bool IsBirthday()
    {
        return DateTime.Today.Month == Dob.Month && DateTime.Today.Day == Dob.Day;
    }

    public override string ToString()
    {
        return $"Customer: {Name}, Member ID: {MemberId}, DOB: {Dob.ToShortDateString()}, Rewards Points: {Rewards.Points}";
    }
}