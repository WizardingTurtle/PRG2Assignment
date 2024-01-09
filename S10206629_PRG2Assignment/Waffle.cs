using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10206629_PRG2Assignment
{
    internal class Waffle : IceCream
    {
        public string WaffleFlavour {  get; set; }  
        public Waffle() { }
        public Waffle(string option, int scoops, List<Flavour> flavour, List<Topping> topping, string waffleflavour) 
        {
            this.Option = option;
            this.Scoops = scoops;
            this.Flavours = flavour;
            this.Toppings = topping;
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
                if (flavour.Premium = true)
                {
                    price += 2;
                }
            }

            //toppings
            price += this.Toppings.Count();

            //waffle flavour
            if (this.WaffleFlavour == "Red velvet" || this.WaffleFlavour ==  "charcoal" || this.WaffleFlavour == "pandan") { price += 3; }

            return price;
        }
    }
}
