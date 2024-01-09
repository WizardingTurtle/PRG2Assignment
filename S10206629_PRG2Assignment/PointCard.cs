// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;


public class PointCard
{
    public int Points;
    public int PunchCard;
    public string Tier;

    // Constructors
    public PointCard()
    {
        
    }

    public PointCard(int points, int punchCard)
    {
        Points = points;
        PunchCard = punchCard;
    }

    public void AddPoints(int pointsToAdd)
    {
        Points += pointsToAdd;
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
    }

    public override string ToString()
    {
        return $"Points: {Points}, PunchCard: {PunchCard}, Tier: {Tier}";
    }
}

