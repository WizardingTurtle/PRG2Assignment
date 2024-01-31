//==========================================================
// Student Number : S10259948
// Student Name : Goh Jun Kai
// Partner Name : Rafol Emmanuel Legaspi
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10206629_PRG2Assignment
{
    class Waffle : IceCream
    {
        public string WaffleFlavour { get; set; }
        public Waffle() { }
        public Waffle(string option, int scoops, List<Flavour> flavour, List<Topping> topping, string waffleflavour) : base(option, scoops, flavour, topping)
        {
            this.WaffleFlavour = waffleflavour;
        }

        public override double CalculatePrice()
        {
            double price = 0;
            // scoops price
            if (this.Scoops == 1)
            {
                price = 7.00;
            }
            else if (this.Scoops == 2)
            {
                price = 8.50;
            }
            else
            {
                price = 9.50;
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

            //waffle flavour
            if (this.WaffleFlavour == "Red velvet" || this.WaffleFlavour == "charcoal" || this.WaffleFlavour == "pandan") { price += 3; }

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

            return "Option: " + Option + " Scoops: " + Scoops + " Flavours: " + flavs + " Toppings: " + tops + " Waffle Flavour: " + WaffleFlavour;
        }
    }
}