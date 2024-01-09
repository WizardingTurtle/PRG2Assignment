using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10206629_PRG2Assignment
{
    internal abstract class IceCream
    {
        public string Option {  get; set; }
        public int Scoops { get; set; }
        public List<Flavour> Flavours { get; set; } = new List<Flavour>(new Flavour[4]);
        public List<Topping> Toppings { get; set; } = new List<Topping>(new Topping[3]);

        public IceCream() { }
        public IceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
        {
            this.Option = option;
            this.Scoops = scoops;
            this.Flavours = flavours;
            this.Toppings = toppings;
        }
        public abstract double CalculatePrice();
        public override string ToString()
        {
            return Option + Scoops + Flavours + Toppings;
        }
    }
}
