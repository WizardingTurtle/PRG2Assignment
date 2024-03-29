﻿// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;


 class PointCard
{
    // Initiliaze
    public int Points { get; set; }
    public int PunchCard { get; set; }
    public string Tier { get; set; }

    // Constructors
    public PointCard(){}

    public PointCard(int points, int punchCard)
    {
        Points = points;
        PunchCard = punchCard;

        if (Points >= 100)
        {
            Tier = "Gold";
        }
        else if (Points >= 50 && Tier != "Gold")
        {
            Tier = "Silver";
        }
        else if (Tier != "Gold" || Tier != "silver")
        {
            Tier = "Ordinary";
        }
    }

    // Class methods
    public void AddPoints(int pointsToAdd)
    {
        Points += pointsToAdd;
        if (Points >= 100)
        {
            Tier = "Gold";
        }
        else if (Points >= 50 && Tier != "Gold")
        {
            Tier = "Silver";
        }
    }

    public void RedeemPoints(int pointsToRedeem)
    {
        if (pointsToRedeem <= Points)
        {
            Points -= pointsToRedeem;
        }
        else
        {
            Console.WriteLine("Not enough points to redeem.");
        }
    }

    public void Punch()
    {
        PunchCard += 1;
    }

    public override string ToString()
    {
        return $"Points: {Points}, PunchCard: {PunchCard}, Tier: {Tier}";
    }
}

