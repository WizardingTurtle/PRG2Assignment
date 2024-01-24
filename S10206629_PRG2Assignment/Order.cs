
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
            "2. Delete Ice Cream\r\n" +
            "0. Return to Menu\r\n");
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
                        while (true)
                        {
                            // display toppings in list and prompt user to add, remove or change
                            Console.WriteLine("--- Toppings ---");
                            foreach (Topping top in IceCreamList[iceCreamIndex].Toppings)
                            {
                                Console.WriteLine(top.ToString());
                            }
                            Console.WriteLine("----------------");

                            Console.WriteLine(
                                "------   Option   ------\r\n" +
                                "1. Add a topping\r\n" +
                                "2. Remove a topping\r\n" +
                                "3. Change a topping\r\n" +
                                "0. return to modify menu\r\n" +
                                "------------------------");
                            Console.Write("Enter option: ");
                            string read = Console.ReadLine();

                            // Add topping code
                            if (read == "1")
                            {
                                if (IceCreamList[iceCreamIndex].Toppings.Count <4)
                                {
                                    Console.WriteLine("\r\n" +
                                        "--- Toppings ---\r\n" +
                                        "<O Sprinkles\r\n" +
                                        "<O Mochi\r\n" +
                                        "<O Sago\r\n" +
                                        "<O Oreos\r\n" +
                                        "----------------");
                                    Console.WriteLine();
                                    // prompt user to enter topping name and then add it to the list
                                    Console.Write("Enter the desired topping: ");
                                    string topping = Console.ReadLine();
                                    if (topping == "Sprinkles" || topping == "Mochi" || topping == "Sago" || topping == "Oreos")
                                    {
                                        IceCreamList[iceCreamIndex].Toppings.Add(new Topping(topping));
                                        Console.WriteLine("{0} topping successfully added", topping);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Topping");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ice cream already has 4 toppings, remove or change a topping");
                                }
                            }
                            // remove topping code
                            else if (read == "2")
                            {
                                // remove topping if there is at least 1 in the list
                                if (IceCreamList[iceCreamIndex].Toppings.Count > 0)
                                {
                                    Console.Write("Enter topping to remove: ");
                                    string topping = Console.ReadLine();
                                    if (IceCreamList[iceCreamIndex].Toppings.Any(t => t.Type == topping))
                                    {
                                        int index = IceCreamList[iceCreamIndex].Toppings.FindIndex(item => item.Type == topping);
                                        IceCreamList[iceCreamIndex].Toppings.RemoveAt(index);
                                        Console.WriteLine("{0} topping successfully removed", topping);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Are you blind? THERE ARE NO TOPPINGS!");
                                }
                            }
                            else if (read == "3")
                            {
                                if (IceCreamList[iceCreamIndex].Toppings.Count > 0)
                                {
                                    Console.Write("Enter topping to change: ");
                                    string topping = Console.ReadLine();
                                    if (IceCreamList[iceCreamIndex].Toppings.Any(t => t.Type == topping))
                                    {
                                        // remember index and then reuse topping string as prompt for new topping
                                        int index = IceCreamList[iceCreamIndex].Toppings.FindIndex(item => item.Type == topping);

                                        Console.WriteLine("\r\n" +
                                        "--- Toppings ---\r\n" +
                                        "<O Sprinkles\r\n" +
                                        "<O Mochi\r\n" +
                                        "<O Sago\r\n" +
                                        "<O Oreos\r\n" +
                                        "----------------");
                                        Console.WriteLine();

                                        Console.Write("Enter new topping: ");
                                        topping = Console.ReadLine();
                                        if (topping == "Sprinkles" || topping == "Mochi" || topping == "Sago" || topping == "Oreos")
                                        {
                                            string oldTop = IceCreamList[iceCreamIndex].Toppings[index].Type;
                                            IceCreamList[iceCreamIndex].Toppings[index].Type = topping;
                                            Console.WriteLine("{0} topping successfully changed to {1}", oldTop, topping);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Are you blind? THERE ARE NO TOPPINGS!");
                                }
                            }
                            else if (read == "0")
                            {
                                Console.WriteLine("Returning to modify menu");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Option");
                            }
                        }
                    }
                    // last 2 else ifs check if the object is of a specific class before changing the respective attributess
                    else if (property == "dipped")
                    {
                        
                        if (IceCreamList[iceCreamIndex] is Cone coneD && coneD.Dipped == true)
                        {
                            coneD.Dipped = false;
                            Console.WriteLine("Ice cream cone has been set to undipped");
                        }
                        else if (IceCreamList[iceCreamIndex] is Cone coneU && coneU.Dipped == false)
                        {
                            coneU.Dipped = true;
                            Console.WriteLine("Ice cream cone has been set to dipped");
                        }
                        else {Console.WriteLine("Ice Cream is not a cone");}
                    }
                    else if (property == "waffleflavour")
                    {
                        if (IceCreamList[iceCreamIndex] is Waffle waffle)
                        {
                            Console.WriteLine(
                                "--- Waffle Flavours ---\r\n" +
                                "<O Original\r\n" +
                                "<O Red Velvet\r\n" +
                                "<O Charcoal\r\n" +
                                "<O Pandan\r\n" +
                                "-----------------------");
                            Console.WriteLine();

                            Console.Write("Enter new waffle flavour: ");
                            string read = Console.ReadLine();
                            if (read == "Original" || read == "Red Velvet" || read == "Charcoal" || read == "Pandan")
                            {
                                if (read == waffle.WaffleFlavour)
                                {
                                    Console.WriteLine("Mate that's already the flavour of the waffle");
                                }
                                else
                                {
                                    waffle.WaffleFlavour = read;
                                    Console.WriteLine("Waffle flavour successfully changed to {0}", read);
                                }
                            }
                            else {Console.WriteLine("Invalid Waffle Flavour");}
                        }
                        else {Console.WriteLine("Ice Cream is not a waffle");}
                    }
                    else {Console.WriteLine("Invalid property name");}
                }
            } 
            // delete ice cream and return to menu
            else if (option == "2")
            {
                // important prompt incase user big finger's option not on purpose
                Console.Write("ARE YOU SURE? Enter DELETE to confirm or enter anything else to invalidate operation: ");
                string read = Console.ReadLine ();

                if (read == "DELETE")
                {
                    IceCreamList.RemoveAt(iceCreamIndex);
                    Console.WriteLine("Ice Cream removed, returning to menu\r\n");
                    return;
                }
                else
                {
                    Console.WriteLine("Operation invalid, returning to modify menu\r\n");
                }
            }
            // changes are saved and user goes back to main code in shame
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

