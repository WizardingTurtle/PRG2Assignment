//==========================================================
// Student Number : S10259948
// Student Name : Goh Jun Kai
// Partner Name : Rafol Emmanuel Legaspi
//==========================================================

namespace S10206629_PRG2Assignment
{
    class Cone : IceCream
    {
        public bool Dipped { get; set; }
        public Cone() { }
        public Cone(string option, int scoops, List<Flavour> flavour, List<Topping> topping, bool dipped) : base(option, scoops, flavour, topping)
        {
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
                if (flavour.Premium)
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

            return "Option: " + Option + " Scoops: " + Scoops + " Flavours: " + flavs + " Toppings: " + tops + " Dipped: " + Dipped;
        }
    }
}