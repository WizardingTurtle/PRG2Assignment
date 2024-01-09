using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10206629_PRG2Assignment
{
    internal class Cone : IceCream
    {
        public bool Dipped {  get; set; }  
        public Cone() { }
        public Cone(string option, int scoops, List<Flavour> flavour, List<Topping> topping, bool dipped) 
        {
            this.Option = option;
            this.Scoops = scoops;
            this.Flavours = flavour;
            this.Toppings = topping;
            this.Dipped = dipped;
        }

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
                if (flavour.Premium = true)
                {
                    price += 2;
                }
            }

            //toppings
            price += this.Toppings.Count();

            //dipped
            if (this.Dipped) { price += 2; }

            return price;
        }
    }
}
