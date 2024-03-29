﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10206629_PRG2Assignment
{
     class Cup : IceCream
    {
        public Cup() { }
        public Cup(string option, int scoops, List<Flavour> flavour, List<Topping> topping) : base(option, scoops, flavour, topping) { }

        public override double CalculatePrice()
        {
            double price = 0;
            // scoops price
            if (this.Scoops == 1)
            {
                price = 4.00;
            }
            else if (this.Scoops == 2) 
            {
                price = 5.50;
            }
            else
            {
                price = 6.50;
            }

            // flavour price
            foreach (Flavour flavour in Flavours)
            {
                if (flavour.Premium)
                {
                    price += 2;
                }
            }

            //toppings
            price += this.Toppings.Count();

            return price;
        }

        public override string ToString()
        {
            string flavs = "";
            foreach (Flavour flav in Flavours)
            {
                flavs += flav.Quantity.ToString() + " ";
                flavs += flav.Type + " ";
            }
            string tops = "";
            foreach (Topping top in Toppings)
            {

                tops += top.Type + " ";
            }

            return "Option: " + Option + " Scoops: " + Scoops + " Flavours: " + flavs + " Toppings: " + tops;
        }
    }
}
