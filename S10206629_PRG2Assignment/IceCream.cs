
namespace S10206629_PRG2Assignment
{
     abstract class IceCream
    {
        public string Option {  get; set; }
        public int Scoops { get; set; }
        public List<Flavour> Flavours { get; set; }
        public List<Topping> Toppings { get; set; }

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
