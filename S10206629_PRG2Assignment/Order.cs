
using S10206629_PRG2Assignment;
 class Order
{
    public int Id { get; set; }
    public DateTime TimeReceived { get; set; }
    public DateTime? TimeFulfilled { get; set; }
    public List<IceCream> IceCreamList { get; set; }

    // Constructors
    public Order(){}

    public Order(int id, DateTime timeReceived)
    {
        Id = id;
        TimeReceived = timeReceived;
        IceCreamList = new List<IceCream>();
    }

    // Class methods

    // whoever thought it would be funny to make the student make a menu to modify an ice cream - i hope ur ice cream punch card loses 1 punch point
    public void ModifyIceCream(int iceCreamIndex)
    {
        IceCreamList[iceCreamIndex].ToString();

        while (true)
        {
            // display info and get user to do something
            Console.WriteLine(
            "1. Modify Ice Cream Details\r\n" +
            "2. Delete Ice Cream" +
            "0. Return to Menu");
            Console.Write("Select an option: ");

            string option = Console.ReadLine();
            // modify ice cream option
            if (option == "1")
            {
                while (true)
                {
                    
                    IceCreamList[iceCreamIndex].ToString();

                    Console.Write(
                        "Select which property of the ice cream you want to modify\r\n" +
                        "Type 0 to return to menu\r\n" +
                        "Enter property or return to menu: ");

                    string property = Console.ReadLine().ToLower();
                    
                    if (property == "option")
                    {
                        Console.WriteLine(
                            "- Option -" +
                            "<O Cup\r\n" +
                            "<O Cone\r\n" +
                            "<O Waffle");
                        Console.Write("Enter option: ");
                        string flav = Console.ReadLine().ToLower();
                        if (flav == IceCreamList[iceCreamIndex].Option)
                        {
                            Console.WriteLine("This is already the option of the icecream");
                        }
                        else
                        {
                            // change icecream to new class since option change
                            if (flav == "cup")
                            {

                                IceCreamList[iceCreamIndex] = new Cup(
                                    "Cup",
                                    IceCreamList[iceCreamIndex].Scoops,
                                    IceCreamList[iceCreamIndex].Flavours,
                                    IceCreamList[iceCreamIndex].Toppings);
                            }
                            else if (flav == "cone")
                            {
                                IceCreamList[iceCreamIndex] = new Cone(
                                    "Cone",
                                    IceCreamList[iceCreamIndex].Scoops,
                                    IceCreamList[iceCreamIndex].Flavours,
                                    IceCreamList[iceCreamIndex].Toppings,
                                    false);
                            }
                            else if (flav == "waffle")
                            {
                                IceCreamList[iceCreamIndex] = new Waffle(
                                    "Waffle",
                                    IceCreamList[iceCreamIndex].Scoops,
                                    IceCreamList[iceCreamIndex].Flavours,
                                    IceCreamList[iceCreamIndex].Toppings,
                                    "Original");
                            }
                        }
                    }
                    else if (property == "scoops")
                    {
                        Console.WriteLine(
                            "Minimum scoops: 1\r\n" +
                            "    Max scoops: 3\r\n");
                        Console.Write("Enter no. of scoops: ");
                        // check if scoops is within valid range
                        try
                        {
                            int count = Convert.ToInt32(Console.ReadLine());
                            if (count < 1 || count > 3)
                            {
                                Console.WriteLine("no. of scoops not within range");
                            }
                            else
                            {
                                // add new flavours/ increase quantity of existing flavours if more scoops added
                                Console.WriteLine();
                                if (count > IceCreamList[iceCreamIndex].Scoops)
                                {

                                    int addCount = count - IceCreamList[iceCreamIndex].Scoops;

                                    Console.WriteLine(
                                        "-----     IceCream Flavours    -----\r\n" +
                                        "Vanilla\r\n" +
                                        "Chocolate\r\n" +
                                        "Strawberry\r\n" +
                                        "Durian\r\n" +
                                        "Ube\r\n" +
                                        "Sea salt\r\n" +
                                        "------------------------------------");
                                    Console.WriteLine();

                                    // Adding Flavours;
                                    for (int i = 0; i < addCount + 1; i++)
                                    {
                                        Console.WriteLine("--- Flavours ---");
                                        foreach (Flavour f in IceCreamList[iceCreamIndex].Flavours)
                                        {
                                            Console.WriteLine(f.ToString());
                                        }
                                        Console.WriteLine("----------------");

                                        Console.WriteLine("{0} flavours left to add", addCount - i);
                                        Console.Write("Enter Flavour to add: ");
                                        string flav = Console.ReadLine();
                                        if (flav == "Vanilla" || flav == "Chocolate" || flav == "Strawberry" || flav == "Durian" || flav == "Ube" || flav == "Sea Salt")
                                        {
                                            bool TypeExist = IceCreamList[iceCreamIndex].Flavours.Any(item => item.Type == flav);
                                            // increment quantity of flavour if true
                                            if (TypeExist)
                                            {
                                                //find flavour in list by index + checking if type is the flavour
                                                int ind = IceCreamList[iceCreamIndex].Flavours.FindIndex(item => item.Type == flav);
                                                IceCreamList[iceCreamIndex].Flavours[ind].Quantity += 1;
                                            }
                                            // Create flavour and add to list
                                            else
                                            {
                                                bool premium = false;
                                                //check if flavour is premium
                                                if (flav == "Durian" || flav == "Ube" || flav == "Sea Salt")
                                                {
                                                    premium = true;
                                                }
                                                else
                                                {
                                                    premium = false;
                                                }
                                                IceCreamList[iceCreamIndex].Flavours.Add(new Flavour(flav, premium, 1));
                                            } 
                                        }
                                        else
                                        {
                                            --i;
                                            Console.WriteLine("invalid flavour");
                                        }
                                        // i increments once a loop is complete
                                        i++;
                                    }
                                }
                                else if (count < IceCreamList[iceCreamIndex].Scoops)
                                {
                                    int subCount = IceCreamList[iceCreamIndex].Scoops - count;
                                    for (int i = 0; i < subCount + 1; i++)
                                    {
                                        Console.WriteLine("--- Flavours ---");
                                        foreach (Flavour flav in IceCreamList[iceCreamIndex].Flavours)
                                        {
                                            Console.WriteLine(flav.ToString());
                                        }
                                        Console.WriteLine("----------------");

                                        Console.WriteLine("{0} flavours left to remove", subCount-i);
                                        Console.Write("Choose which flavour to remove or decrease by 1 in quantity: ");
                                        string read = Console.ReadLine().ToLower();

                                        // grab flavourindex and then decrease flavour quantity and remove if it becomes 0
                                        bool TypeExist = IceCreamList[iceCreamIndex].Flavours.Any(item => item.Type == read);
                                        if (TypeExist)
                                        {
                                            int index = IceCreamList[iceCreamIndex].Flavours.FindIndex(item => item.Type.ToLower() == read);
                                            IceCreamList[iceCreamIndex].Flavours[index].Quantity -= 1;
                                            if (IceCreamList[iceCreamIndex].Flavours[index].Quantity <= 0)
                                            {
                                                IceCreamList[iceCreamIndex].Flavours.RemoveAt(index);
                                            }
                                        }
                                        else
                                        {
                                            --i;
                                            Console.WriteLine("invalid flavour");
                                        }
                                        i++;
                                    }                                     
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Invalid value");
                        }
                    }
                    else if (property == "Flavours")
                    {
                        Console.WriteLine("--- Flavours ---");
                        foreach (Flavour flav in IceCreamList[iceCreamIndex].Flavours)
                        {
                            Console.WriteLine(flav.ToString());
                        }
                        Console.WriteLine("----------------");
                        Console.Write("Enter existing flavour to change: ");
                        string flavOld = Console.ReadLine();

                        bool TypeExist = IceCreamList[iceCreamIndex].Flavours.Any(item => item.Type == flavOld);
                        if (TypeExist)
                        {
                            Console.WriteLine(
                                        "-----     IceCream Flavours    -----\r\n" +
                                        "Vanilla\r\n" +
                                        "Chocolate\r\n" +
                                        "Strawberry\r\n" +
                                        "Durian\r\n" +
                                        "Ube\r\n" +
                                        "Sea salt\r\n" +
                                        "------------------------------------");
                            Console.WriteLine();

                            Console.Write("Enter new flavour: ");
                            string flavNew = Console.ReadLine();

                            // check if flavour already exists in the list and scold the user for not using their brain
                            TypeExist = IceCreamList[iceCreamIndex].Flavours.Any(item => item.Type == flavNew);
                            if (TypeExist)
                            {
                                Console.WriteLine("It's the same flavour... are you ok?");
                            }
                            // check if flavour is valid and then change accordingly
                            else if (flavNew == "Vanilla" || flavNew == "Chocolate" || flavNew == "Strawberry" || flavNew == "Durian" || flavNew == "Ube" || flavNew == "Sea Salt")
                            {
                                //find index of flavour in flavourlist of the icecream
                                int index = IceCreamList[iceCreamIndex].Flavours.FindIndex(item => item.Type == flavOld);
                                //check if flavour is premium
                                bool premium = false;                                
                                if (flavNew == "Durian" || flavNew == "Ube" || flavNew == "Sea Salt")
                                {
                                    premium = true;
                                }
                                else
                                {
                                    premium = false;
                                }
                                IceCreamList[iceCreamIndex].Flavours[index] = new Flavour(flavNew,premium, IceCreamList[iceCreamIndex].Flavours[index].Quantity);
                            }
                        }
                    }
                    else if (property == "Toppings")
                    {

                    }
                    else if (property == "dipped")
                    {

                    }
                    else if (property == "waffleflavour")
                    {

                    }
                    }
                } 

            // delete ice cream option
            else if (option == "2")
            {

            }
            // do nothing and go back to main code in shame
            else if (option == "0")
            {
                Console.WriteLine("Returning to menu");
                return;
            }
        }

        

    }

    public void AddIceCream(IceCream iceCream)
    {
        IceCreamList.Add(iceCream);
    }

    public void DeleteIceCream(int iceCreamIndex)
    {
        IceCreamList.RemoveAt(iceCreamIndex);
    }

    public double CalculateTotal()
    {
        return 0.0;
    }

    public override string ToString()
    {
        return $"Order ID: {Id}, Time Received: {TimeReceived}, Time Fulfilled: {TimeFulfilled}, Total: {CalculateTotal()}";
    }
}

