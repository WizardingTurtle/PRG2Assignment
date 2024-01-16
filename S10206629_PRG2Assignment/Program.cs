//==========================================================
// Student Number : S10206629
// Student Name : Rafol Emmanuel Legaspi
// Partner Name : Goh Jun Kai
//==========================================================

// To add code to create customers and orders from data files
// Read and create customers and pointcards based on customers.csv
using S10206629_PRG2Assignment;
using System.Diagnostics.Metrics;

string fileName = ".\\datafiles\\customers.csv";
string[] lines = File.ReadAllLines(fileName);

List<Customer> customersList = new List<Customer>();
for (int i = 1; i < lines.Length; i++)
{
    string[] line = lines[i].Split(',');

    // create customer
    Customer customer = new Customer(line[0], Convert.ToInt32(line[1]), Convert.ToDateTime(line[2]));
    // modify customer's point card
    customer.Rewards = new PointCard(Convert.ToInt32(line[4]), Convert.ToInt32(line[5]));
    customer.Rewards.PunchCard= Convert.ToInt32(line[5]);
    // add customer to list
    customersList.Add(customer);
}

// Read and create orders based on orders.csv
fileName = ".\\datafiles\\orders.csv";
lines = File.ReadAllLines(fileName);
// Create orderList and memberOrderDic 
List<Order> ordersList = new List<Order>();
Dictionary<int, int> memberOrderDic = new Dictionary<int, int>();
// loop through lines of orders.csv
for (int i = 1; i < lines.Length; i++)
{
    string[] line = lines[i].Split(',');

    // check if order exists before creating a new order
    bool IdExist = ordersList.Any(item => item.Id == Convert.ToInt32(line[0]));

    if (IdExist) 
    {
        // Find index of order in list
        int Oindex = ordersList.FindIndex(order => order.Id == Convert.ToInt32(line[0]));
        // Create Flavour List
        List<Flavour> flavours = new List<Flavour>();
        // loop through line index for flavours
        for (int f = 8; f < 11; f++)
        {
            //check if flavour is not null
            if (line[f] != "")
            {
                //check if flavour exists in list
                bool TypeExist = flavours.Any(item => item.Type == line[f]);
                // increment quantity of flavour if true
                if (TypeExist)
                {
                    //find flavour in list by index + checking if type is the flavour
                    int index = flavours.FindIndex(item => item.Type == line[f]);
                    flavours[index].Quantity += 1;
                }

                // Create flavour and add to list
                else
                {
                    bool premium = false;
                    //check if flavour is premium
                    if (line[f] == "Durian" || line[f] == "Ube" || line[f] == "Sea Salt")
                    {
                        premium = true;
                    }
                    else
                    {
                        premium = false;
                    }

                    flavours.Add(new Flavour(line[f], premium, 1));
                }
            }
        }

        // Create Toppings List
        List<Topping> toppings = new List<Topping>();
        // Add toppings to topping list
        for (int t = 11; t < 15; t++)
        {
            if (line[t] != "")
            {
                toppings.Add(new Topping(line[t]));
            }
        }

        // Now we have figured out the general attributes of the ice cream - we must now figure out the icecream option
        // check icecream type create the object, then add the object to order in orderlist
        if (line[4] == "Cup")
        {
            IceCream ic = new Cup(line[4], Convert.ToInt32(line[5]), flavours, toppings);
        }
        else if (line[4] == "Cone")
        {
            // check if cone is dipped
            if (line[6] == "TRUE")
            {
                ordersList[Oindex].IceCreamList.Add(new Cone(line[4], Convert.ToInt32(line[5]), flavours, toppings, true));
            }
            else
            {
                ordersList[Oindex].IceCreamList.Add(new Cone(line[4], Convert.ToInt32(line[5]), flavours, toppings, false));
            }
        }
        else if (line[4] == "Waffle")
        {
            ordersList[Oindex].IceCreamList.Add(new Waffle(line[4], Convert.ToInt32(line[5]), flavours, toppings, line[7]));
        }
    }

    else 
    {
        // add memberid and orderid into a dictionary
        memberOrderDic.Add(Convert.ToInt32(line[0]), Convert.ToInt32(line[1]));
        // Create order object
        Order order = new Order(Convert.ToInt32(line[0]), Convert.ToDateTime(line[2]));
        order.TimeFulfilled = Convert.ToDateTime(line[3]);

        // Create Flavour List
        List<Flavour> flavours = new List<Flavour>();
        // loop through line index for flavours
        for (int f = 8; f < 11; f++)
        {   
            //check if flavour is not null
            if (line[f] != "") 
            {
                //check if flavour exists in list
                bool TypeExist = flavours.Any(item => item.Type == line[f]);
                // increment quantity of flavour if true
                if (TypeExist) 
                {
                    //find flavour in list by index + checking if type is the flavour
                    int index = flavours.FindIndex(item => item.Type == line[f]);
                    flavours[index].Quantity += 1;
                }

                // Create flavour and add to list
                else
                {
                    bool premium = false;
                    //check if flavour is premium
                    if (line[f] == "Durian" || line[f] == "Ube" || line[f] == "Sea Salt")
                    {
                         premium = true; 
                    }
                    else
                    {
                         premium = false;
                    }

                    flavours.Add(new Flavour(line[f],premium,1));
                }
            } 
        }

        // Create Toppings List
        List<Topping> toppings = new List<Topping>();
        // Add toppings to topping list
        for (int t = 11; t < 15; t++)
        {
            if (line[t] != "")
            {
                toppings.Add(new Topping(line[t]));
            }
        }   

        // Now we have figured out the general attributes of the ice cream - we must now figure out the icecream option
        // check icecream type create the object, then add the object to order
        if (line[4] == "Cup")
        {
            IceCream ic = new Cup(line[4] ,Convert.ToInt32(line[5]) ,flavours ,toppings);
        }
        else if (line[4] == "Cone")
        {
            // check if cone is dipped
            if (line[6] =="TRUE")
            {
                order.IceCreamList.Add(new Cone(line[4], Convert.ToInt32(line[5]), flavours, toppings, true));
            }
            else
            {
                order.IceCreamList.Add(new Cone(line[4], Convert.ToInt32(line[5]), flavours, toppings, false));
            }
        }
        else if (line[4] == "Waffle")
        {
            order.IceCreamList.Add(new Waffle(line[4], Convert.ToInt32(line[5]), flavours, toppings, line[7]));
        }

        // add order into list
        ordersList.Add(order);

    }
}

// Add orders to customer order history
foreach (Order order in ordersList) 
{
    // check if order id exist in memberorderdic
    if(memberOrderDic.Any(item => item.Key == order.Id))
    {
        // Find index of customer in customer list
        int index = customersList.FindIndex(item => item.MemberId == memberOrderDic[order.Id]);
        customersList[index].OrderHistory.Add(order);

    }
}

    //Main Program Loop
    void MenuStart()
{
    while (true) 
    {
        Console.WriteLine
        (
        "-----      I.C.Treats Menu     -----\r\n" +
        "[1] List all customers\r\n" +
        "[2] List all current orders\r\n" +
        "[3] Register a new customer\r\n" +
        "[4] Create a customer's order\r\n" +
        "[5] Display order details of customer\r\n" +
        "[6] Modify order details\r\n" +
        "[0] Exit Program\r\n" +
        "------------------------------------"
        );
        Console.Write("Please Choose an option: ");
        string option = Console.ReadLine();

        if (option == "1")
        {
        //Placeholder
        }
        else if (option == "2")
        {
            listAllOrders();
        }
        else if (option == "3")
        {
        //Placeholder
        }
        else if (option == "4")
        {
        //Placeholder
        }
        else if (option == "5")
        {
            displayCustomerOrder();
        }
        else if (option == "7")
        {
        //Placeholder
        }
        else if (option == "0")
        {
        Console.WriteLine("Closing the program - bye bye!");
        break;
        }
    }
}
//Feature 1 List all customers
void listAllCustomers()
{

}
//Feature 2 List all current orders
void listAllOrders()
{
    // List Gold Member Current Orders
    Console.WriteLine("Gold member's current orders");
    foreach (Customer customer in customersList)
    {
        if (customer.Rewards.Tier == "Gold")
        {
            Console.WriteLine(customer.CurrentOrder.ToString());
        }
    }
    // List regular queue Current Orders
    Console.WriteLine("Regular queue current orders");
    foreach (Customer customer in customersList)
    {
        if (customer.Rewards.Tier != "Gold")
        {
            Console.WriteLine(customer.CurrentOrder.ToString());
        }
    }
}
//Feature 3 Register a new customer

//Feature 4 Create a customer’s order

//Feature 5 Display order details of a customer
void displayCustomerOrder()
{
    // Display Customers
    Console.WriteLine("{0,10} {1}", "Member Id", "Member Name");
    foreach (Customer customer in customersList)
    {
        Console.WriteLine("{0,3} {1}",customer.MemberId,customer.Name);
    }
    Console.WriteLine();

    // Input
    Console.Write("Select a customer by ID: ");
    int Id = Convert.ToInt32(Console.ReadLine());

    // Display Customer's Orders
    int index = customersList.FindIndex(c => c.MemberId == Id);
    Console.WriteLine("Current Order");
    if (customersList[index].CurrentOrder != null)
    {
        Console.WriteLine(customersList[index].CurrentOrder.ToString());
    }
    else
    {
        Console.WriteLine("No order");
    }
    Console.WriteLine();

    Console.WriteLine("Past Orders");
    foreach (Order order in customersList[index].OrderHistory)
    {
        Console.WriteLine(order.ToString());
    }
}
//Feature 6 Modify order details




// Start Program
MenuStart();