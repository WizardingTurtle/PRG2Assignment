//==========================================================
// Student Number : S10206629
// Student Name : Rafol Emmanuel Legaspi
// Partner Name : Goh Jun Kai
//==========================================================

using S10206629_PRG2Assignment;
using System;
using System.Security.Cryptography.X509Certificates;

string fileName = ".\\datafiles\\customers.csv";
string[] lines = File.ReadAllLines(fileName);

// initialize customerlist from customer.csv
List<Customer> customersList = new List<Customer>();
for (int i = 1; i < lines.Length; i++)
{
    string[] line = lines[i].Split(',');

    // create customer
    Customer customer = new Customer(line[0], Convert.ToInt32(line[1]), DateTime.ParseExact(line[2], "dd/MM/yyyy", null));
    // modify customer's point card
    customer.Rewards = new PointCard(Convert.ToInt32(line[4]), Convert.ToInt32(line[5]));
    customer.Rewards.PunchCard= Convert.ToInt32(line[5]);
    // add customer to list
    customersList.Add(customer);
}

// Read and create orders based on orders.csv
fileName = ".\\datafiles\\orders.csv";
lines = File.ReadAllLines(fileName);

// initalize orderlist and memberOrderDic
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
        // check icecream type to2 create the object, then add the object to order in orderlist
        if (line[4] == "Cup")
        {
            ordersList[Oindex].IceCreamList.Add(new Cup(line[4], Convert.ToInt32(line[5]), new List<Flavour>(flavours), new List<Topping>(toppings)));
        }
        else if (line[4] == "Cone")
        {
            // check if cone is dipped
            if (line[6] == "TRUE")
            {
                ordersList[Oindex].IceCreamList.Add(new Cone(line[4], Convert.ToInt32(line[5]), new List<Flavour>(flavours), new List<Topping>(toppings), true));
            }
            else
            {
                ordersList[Oindex].IceCreamList.Add(new Cone(line[4], Convert.ToInt32(line[5]), new List<Flavour>(flavours), new List<Topping>(toppings), false));
            }
        }
        else if (line[4] == "Waffle")
        {
            ordersList[Oindex].IceCreamList.Add(new Waffle(line[4], Convert.ToInt32(line[5]), new List<Flavour>(flavours), new List<Topping>(toppings), line[7]));
        }
    }

    // creates new order
    else 
    {
        // add memberidI(value) and orderid(Key) into a dictionary
        memberOrderDic.Add(Convert.ToInt32(line[0]), Convert.ToInt32(line[1]));
        // Create order object
        Order order = new Order(Convert.ToInt32(line[0]), DateTime.ParseExact(line[2], "dd/MM/yyyy HH:mm", null));
        order.TimeFulfilled = DateTime.ParseExact(line[3], "dd/MM/yyyy HH:mm", null);

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
            order.IceCreamList.Add(new Cup(line[4] ,Convert.ToInt32(line[5]) , new List<Flavour>(flavours), new List<Topping>(toppings)));
        }
        else if (line[4] == "Cone")
        {
            // check if cone is dipped
            if (line[6] =="TRUE")
            {
                order.IceCreamList.Add(new Cone(line[4], Convert.ToInt32(line[5]), new List<Flavour>(flavours), new List<Topping>(toppings), true));
            }
            else
            {
                order.IceCreamList.Add(new Cone(line[4], Convert.ToInt32(line[5]), new List<Flavour>(flavours), new List<Topping>(toppings), false));
            }
        }
        else if (line[4] == "Waffle")
        {
            order.IceCreamList.Add(new Waffle(line[4], Convert.ToInt32(line[5]), new List<Flavour>(flavours), new List<Topping>(toppings), line[7]));
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

// Initialize Queue
Queue<Order> GoldQueue = new Queue<Order>();
Queue<Order> RegularQueue = new Queue<Order>();

void InitializeQueue()
{
    foreach (Customer customer in customersList)
    {
        // checks for 3 conditions
        // current order exists, current order has more than 1 icecream, current order does not exist in either queue, current order is not fulfilled
        if (customer.CurrentOrder != null && customer.CurrentOrder.IceCreamList.Count > 0 && customer.CurrentOrder.TimeFulfilled == null)
        {
            bool NotExistGQ = GoldQueue.Any(o => o.Id == customer.CurrentOrder.Id) == false;
            bool NotExistRQ = RegularQueue.Any(o => o.Id == customer.CurrentOrder.Id) == false;
            if (NotExistGQ && NotExistRQ)
            {
                if (customer.Rewards.Tier == "Gold")
                {
                    GoldQueue.Enqueue(customer.CurrentOrder);
                }
                else
                {
                    RegularQueue.Enqueue(customer.CurrentOrder);
                }
            }
        }
    }
}


    //Main Program Loop
    void MenuStart()
{
    while (true) 
    {
        // Update Queues
        InitializeQueue();

        Console.WriteLine
        (
        "-----      I.C.Treats Menu     -----\r\n" +
        "[1] List all customers\r\n" +
        "[2] List all current orders\r\n" +
        "[3] Register a new customer\r\n" +
        "[4] Create a customer's order\r\n" +
        "[5] Display order details of customer\r\n" +
        "[6] Modify order details\r\n" +
        "[9] Advanced Features\r\n" +
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
            RegisterNewCustomer();
        }
        else if (option == "4")
        {
        
        }
        else if (option == "5")
        {
            displayCustomerOrder();
        }
        else if (option == "6")
        {
            modifyCustomerOrder();
        }
        else if (option == "9")
        {
            AMenuStart();
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
    Console.WriteLine();
    // List Gold Member Current Orders
    Console.WriteLine("Gold member's current orders");
    foreach (Order currentOrder in GoldQueue)
    {
            Console.WriteLine(currentOrder.ToString());
    }
    // List regular queue Current Orders
    Console.WriteLine("Regular queue current orders");
    foreach (Order currentOrder in RegularQueue)
    {
        Console.WriteLine(currentOrder.ToString());
    }
    Console.WriteLine();
}
//Feature 3 Register a new customer
static void AppendCustomerToCSV(Customer customer)
{
    try
    {        // Append the customer information to the customers.csv file
        using (StreamWriter sw = File.AppendText("C:\\Users\\user\\Downloads\\ProgrammingAs\\ProgramminAssign\\ProgramminAssign\\datafiles\\customers.csv"))
        {
            sw.WriteLine($"{customer.Name},{customer.MemberId},{customer.Dob.ToString("dd/MM/yyyy")},{customer.Rewards.Tier},{customer.Rewards.Points},{customer.Rewards.PunchCard}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error appending customer to CSV: {ex.Message}");
    }
}

void RegisterNewCustomer()
{
    /*
     
    // Prompt user for customer information
    Console.Write("Enter customer name: ");
    string name = Console.ReadLine();

    Console.Write("Enter customer ID number: ");
    int idNumber = Convert.ToInt32(Console.ReadLine());

    Console.Write("Enter customer date of birth (dd/MM/yyyy): ");
    DateTime dob = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

    // Create a customer object with the provided information
    Customer newCustomer = new Customer(name, idNumber, dob);

    // Create a PointCard object and assign it to the customer
    newCustomer.Rewards = new PointCard(0, 0);

    // You might want to initialize points and punch card values based on your logic
    // Append the customer information to the customers.csv file
    AppendCustomerToCSV(newCustomer);

    // Add the new customer to the list
    customersList.Add(newCustomer);
    Console.WriteLine("Customer registered successfully!");

    */
}

//Feature 4 Create a customer’s order

//Feature 5 Display order details of a customer
void displayCustomerOrder()
{
    // Display Customers
    Console.WriteLine();
    Console.WriteLine("{0,-10} {1}", "Member Id", "Member Name");
    foreach (Customer customer in customersList)
    {
        Console.WriteLine("{0,-10} {1}",customer.MemberId,customer.Name);
    }
    Console.WriteLine();

    // Input
    Console.Write("Select a customer by ID: ");

    // checks if input is valid int else return to menu
    int Id = 0;
    if(!int.TryParse(Console.ReadLine(), out Id))
    {
        Console.WriteLine("Invalid ID");
        return;
    }
    // checks if id exists in customerList else return to menu
    bool IdNoExist = customersList.Any(c => c.MemberId == Id) == false;
    if (IdNoExist)
    {
        Console.WriteLine("ID does not exist");
        return;
    }

    // Display Customer's Orders
    int index = customersList.FindIndex(c => c.MemberId == Id);
    Console.WriteLine();
    Console.WriteLine("Current Order ---");
    if (customersList[index].CurrentOrder != null)
    {
        Console.WriteLine(customersList[index].CurrentOrder.ToString());
        foreach (IceCream ic in customersList[index].CurrentOrder.IceCreamList)
        {
            Console.WriteLine(ic.ToString());
        }
    }
    else
    {
        Console.WriteLine("No order");
    }
    Console.WriteLine();

    Console.WriteLine("Order History ---");
    foreach (Order order in customersList[index].OrderHistory)
    {
        Console.WriteLine(order.ToString());
        Console.WriteLine("Ice creams ---");
        foreach (IceCream ic in order.IceCreamList)
        {
            Console.WriteLine(ic.ToString());   
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}
//Feature 6 Modify order details
void modifyCustomerOrder()
{
    // Display Customers
    Console.WriteLine("{0,-10} {1}", "Member Id", "Member Name");
    foreach (Customer customer in customersList)
    {
        Console.WriteLine("{0,-10} {1}", customer.MemberId, customer.Name);
    }
    Console.WriteLine();

    // Find customer
    Console.Write("Select a customer by ID: ");

    // check if ID is not a proper int
    int Id = 0;
    if (!int.TryParse(Console.ReadLine(), out Id))
    {
        Console.WriteLine("Invalid ID");
        return;
    }

    // Check if ID exists else return to main menu
    bool IdNoExist = customersList.Any(c => c.MemberId == Id) == false;
    if (IdNoExist)
    {
        Console.WriteLine("ID does not exist");
        return;
    }

    // Get index of customer in customerlist and display the icecreams of the currentorder
    int index = customersList.FindIndex(c => c.MemberId == Id);
    if (customersList[index].CurrentOrder != null)
    {
        Console.WriteLine("Icecreams in current order");
        Console.WriteLine("{0,-8} {1}", "Index", "Details");

        foreach (IceCream ic in customersList[index].CurrentOrder.IceCreamList)
        {
            // find index of icecream in list
            int indexic = customersList[index].CurrentOrder.IceCreamList.FindIndex(i => i == ic);
            Console.WriteLine("{0,-8} {1}",(indexic+1).ToString() + ".", ic.ToString());
        }

        Console.WriteLine();
        Console.WriteLine
            (
            "------------------------------------\r\n" +
            "[1] choose an existing ice cream object to modify\r\n" +
            "[2] add an entirely new ice cream object to the order\r\n" +
            "[3] choose an existing ice cream object to delete from the order\r\n" +
            "------------------------------------"
            );
        Console.WriteLine();

        Console.Write("Choose an option: ");
        string option = Console.ReadLine();
         
        // Choose ice cream from order and modify it
        if (option == "1")
        {
            // check if order has at least 1 ice cream else reject
            if (customersList[index].CurrentOrder.IceCreamList.Count >= 1)
            {
                Console.Write("Choose an ice cream to modify by index: ");
                int indexic = 0;
                if (!int.TryParse(Console.ReadLine(), out indexic))
                {
                    Console.WriteLine("Invalid ID");
                    return;
                }

                // Modify icecream details
                customersList[index].CurrentOrder.ModifyIceCream(indexic-1);
            }
            else
            {
                Console.WriteLine("Order must always have at least one ice cream");
            }          
        }
        // Add new ice cream to order
        else if (option == "2")
        {
            // Choosing Ice-Cream Option
            Console.WriteLine(
                "-----     Ice-Cream options    -----\r\n" +
                "Cup\r\n" +
                "Cone\r\n" +
                "Waffle\r\n" +
                "------------------------------------");
            string type = "";
            // loop until user gets it right
            while (true)
            {
                Console.Write("Enter the Ice-Cream option: ");
                type = Console.ReadLine();
                if (type == "Cup" || type == "Cone" || type == "Waffle")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Option");
                }
            }

            // Choose from 1-3 scoops
            int scoops = 1;
            Console.WriteLine("Minimum 1 scoop, Max 3 scoops");
            string read = "";
            // loop until user gets it right
            while (true)
            {
                Console.Write("Enter the number of scoops: ");
                read = Console.ReadLine();
                if (read == "1" || read == "2" || read == "3")
                {
                    scoops = Convert.ToInt32(read);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid scoop count");
                }
            }
            
                       
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

            // Adding Flavours
            List<Flavour> flavList = new List<Flavour>();
            for (int i = 0 ;i < scoops ;i++)
            {
                Console.Write("Enter Flavour {0} (Case Sensitive): ",i+1);
                string flav = Console.ReadLine();
                if (flav == "Vanilla" || flav == "Chocolate" || flav == "Strawberry" || flav == "Durian" || flav == "Ube" || flav == "Sea Salt")
                {
                    bool TypeExist = flavList.Any(item => item.Type == flav);
                    // increment quantity of flavour if true
                    if (TypeExist)
                    {
                        //find flavour in list by index + checking if type is the flavour
                        int ind = flavList.FindIndex(item => item.Type == flav);
                        flavList[ind].Quantity += 1;
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
                        flavList.Add(new Flavour(flav, premium, 1));
                    }
                }
                else
                {
                    --i;
                    Console.WriteLine("invalid flavour");
                }
            }
            // Adding Toppings
            List<Topping> topList = new List<Topping>();
            // Choose from 0-4 Toppings
            Console.WriteLine("\r\n" +
                "--- Toppings ---\r\n" +
                "<O Sprinkles\r\n" +
                "<O Mochi\r\n" +
                "<O Sago\r\n" +
                "<O Oreos\r\n" +
                "----------------");
            Console.WriteLine();
            int toppings = 0;
            Console.WriteLine("Minimum 0 toppings Maximum 4 toppings");
            // loop until user gets it right
            while (true)
            {
                Console.Write("Enter the number of toppings: ");
                read = Console.ReadLine();
                if (read == "1" || read == "2" || read == "3" || read == "4" || read == "0")
                {
                    toppings = Convert.ToInt32(read);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid toppings count");
                }
            }

            for (int i = 0; i < toppings; i++)
            {
                Console.Write("Enter Topping {0}: ", i+1);
                string top = Console.ReadLine();
                if (top == "Sprinkles" || top == "Mochi" || top == "Sago" || top == "Oreos")
                {
                    topList.Add(new Topping(top));
                }
                else
                {
                    --i;
                    Console.WriteLine("invalid topping");
                }
            }

            // check ice cream type and add to selected order list
            if (type == "Cup")
            {
                customersList[index].CurrentOrder.AddIceCream(new Cup(type, scoops, flavList, topList));
                Console.WriteLine("Ice cream cup added to current order!");
            }
            else if (type == "Cone")
            {
                // check if cone is dipped --- imprison the user until they get it right               
                bool dip = false;
                while (true) 
                {
                    Console.Write("Enter yes or no if cone is dipped: ");
                    read = Console.ReadLine();
                    if (read.ToLower() == "yes")
                    {
                        dip = true;
                        break;
                    }
                    else if (read.ToLower() == "no")
                    {
                        dip = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("invalid response");
                    }
                }
                customersList[index].CurrentOrder.AddIceCream(new Cone(type, scoops, flavList, topList, dip));
                Console.WriteLine("Ice cream cone added to current order!");
            }
            else if (type == "Waffle")
            {
                string wafFlav = "Original";
                // imprison the user if they get it wrong
                Console.WriteLine(
                    "--- Waffle Flavours ---\r\n" +
                    "<O Original\r\n" +
                    "<O Red Velvet\r\n" +
                    "<O Charcoal\r\n" +
                    "<O Pandan\r\n" +
                    "-----------------------");
                Console.WriteLine();
                while (true)
                {
                    Console.Write("Enter Waffle Flavour: ");
                    read = Console.ReadLine();
                    if (read == "Original" || read == "Red Velvet" || read =="Charcoal" || read == "Pandan")
                    {
                        wafFlav = read;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Waffle Flavour");
                    }
                }
                customersList[index].CurrentOrder.AddIceCream(new Waffle(type, scoops, flavList, topList, wafFlav));
                Console.WriteLine("Ice cream waffle added to current order!");
            }


        }
        // Delete ice cream from order
        else if (option == "3")
        {
            if (customersList[index].CurrentOrder.IceCreamList.Count > 1)
            {
                Console.Write("Choose an ice cream to remove by index: ");
                int indexic = 0;
                // check if consolereadline is integer & within index range
                if (!int.TryParse(Console.ReadLine(), out indexic) || indexic <= 0 || indexic >= customersList[index].CurrentOrder.IceCreamList.Count)
                {
                    Console.WriteLine("Invalid index value, returning to menu\r\n");
                }
                else
                {
                    Console.Write("ARE YOU SURE? Enter DELETE to confirm or enter anything else to invalidate operation: ");
                    string read = Console.ReadLine();

                    if (read == "DELETE")
                    {
                        customersList[index].CurrentOrder.DeleteIceCream(indexic-1);
                        Console.WriteLine("Ice Cream removed, returning to menu\r\n");
                    }
                    else
                    {
                        Console.WriteLine("Operation invalid, returning to modify menu\r\n");
                    }
                }           
            }
            else
            {
                Console.WriteLine("Order must always have at least one ice cream");
            }       
        }
    }
    else
    {
        Console.WriteLine("No current order available");
    }
}


// {>>> ADVANCED FEATURES <<<}

// Advanced Menu
void AMenuStart()
{
    while (true)
    {
        InitializeQueue();
        foreach (Order order in RegularQueue)
        {
            Console.WriteLine(order.ToString());
            foreach (IceCream ic in order.IceCreamList)
            {
                ic.ToString();
            }
            Console.WriteLine(order.IceCreamList.Count);
        }


        Console.WriteLine();
        Console.WriteLine(
            "----- <<< Advanced Features Menu >>> -----\r\n" +
            "[1] Process an order and checkout\r\n" +
            "[2] Display monthly charged amounts breakdown\r\n" +
            "    & total charged amounts for the year\r\n" +
            "[0] Return to Main Menu\r\n" +
            "------------------------------------------");
        Console.Write("Enter an option: ");
        string option = Console.ReadLine();
        // feature a
        if (option == "1")
        {
            advancedProcessOC();
        }
        // feature b 
        else if (option == "2")
        {  
            advancedDisplayChargedAmounts();
        }
        else if (option == "0")
        {
            Console.WriteLine("Exiting Advanced Menu, returning to Main Menu\r\n");
            break;
        }
        else
        {
            Console.WriteLine("Invalid Option\r\n");
        }    
    }
}

// a Process an order and checkout
void advancedProcessOC()
{
    Console.WriteLine();
    Order firstOrder = null;
    // check & dequeue for goldqueue before regular queue - return if no orders in either queues
    if (GoldQueue.Count > 0)
    {
        firstOrder = GoldQueue.Dequeue();
    }
    else if (RegularQueue.Count > 0)
    {
        firstOrder = RegularQueue.Dequeue();
    }
    else
    {
        Console.WriteLine("No current orders\r\n");
        return;
    }
    // Display ice creams in FirstOrder
    Console.WriteLine(
        "{0} Ice creams from order\r\n" +
        "-----------------------------", firstOrder.IceCreamList.Count);
    foreach (IceCream ic in firstOrder.IceCreamList)
    {
        Console.WriteLine(ic.ToString());
       
    }
    Console.WriteLine("-----------------------------");

    // Display total bill
    Console.WriteLine("Total bill: ${0:0.00}\r\n", firstOrder.CalculateTotal());

    //Initialize finalbill
    double finalBill = firstOrder.CalculateTotal();

    // Display customer membership status & points

    Customer currentCustomer = new Customer();
    // Find index of customer in customer list by matching id
    int index = customersList.FindIndex(c => c.CurrentOrder != null && c.CurrentOrder.Id == firstOrder.Id);
    currentCustomer = customersList[index];

    // Display customer punchard details
    Console.WriteLine("Customer {0}'s PunchCard details",currentCustomer.Name);
    Console.WriteLine(currentCustomer.Rewards.ToString());

    IceCream expensiveIC = null;
    // Check customer birthday for discount special
    if (currentCustomer.Dob.ToShortDateString == DateTime.Now.ToShortDateString)
    {
        // assign expensiveIC as most expensive icecream
        expensiveIC = firstOrder.IceCreamList.MaxBy(ic => ic.CalculatePrice());
        finalBill -= expensiveIC.CalculatePrice();
    }

    // Punchcard for each icecream and check if order is eligible for punchcard discount special
    IceCream firstIC = null;
    // checks punchcard is at the  10th punch
    if (currentCustomer.Rewards.PunchCard == 10)
    {
        currentCustomer.Rewards.PunchCard = 0;
        // check if first icecream in order is not affected by birthday discount and is the first punch discount of the order
        if (firstOrder.IceCreamList[0] != expensiveIC && expensiveIC != null)
        {
            firstIC = firstOrder.IceCreamList[0];
            finalBill -= firstIC.CalculatePrice();
            Console.WriteLine("PunchCard redeemed! 11th ice cream is free of charge!");
        }
        // checks if expensive icecream is first item in order
        else if (firstOrder.IceCreamList[0] == expensiveIC && expensiveIC != null)
        {
            firstIC = firstOrder.IceCreamList[1];
            finalBill -= firstIC.CalculatePrice();
            Console.WriteLine("PunchCard redeemed! 11th ice cream is free of charge!");

        }
    }

    // check if customer is valid for reedeming points and prompt if user wants to redeem points
    if (currentCustomer.Rewards.Tier != "Ordinary" && currentCustomer.Rewards.Points > 0)
    {
        Console.WriteLine("PointCard points: {0}",currentCustomer.Rewards.Points);
        Console.Write("Enter no. of points to redeem: ");
        int points = 0;

        if (!int.TryParse(Console.ReadLine(), out points))
        {
            Console.WriteLine("Invalid points value, points will not be redeemed\r\n");
        }

        if (currentCustomer.Rewards.Points < points)
        {
            currentCustomer.Rewards.RedeemPoints(points);
        }
        else
        {
            currentCustomer.Rewards.RedeemPoints(points);
            finalBill -= points * 0.02;
        } 
    }

    // Display final bill
    Console.WriteLine("Final bill: ${0:0.00}", finalBill);
    Console.WriteLine("Press any key to confirm payment...");
    Console.ReadKey();

    // punch for each icecream until punch card reaches 10 points
    for (int i = 0; i < firstOrder.IceCreamList.Count || currentCustomer.Rewards.PunchCard == 10; i++)
    {
        currentCustomer.Rewards.Punch();
    }

    // add points to customer
    int addPoints = Convert.ToInt32(Math.Floor(finalBill * 0.72));
    currentCustomer.Rewards.AddPoints(addPoints);

    // confirm order time fulfilled with datetime now
    firstOrder.TimeFulfilled = DateTime.Now;

    currentCustomer.OrderHistory.Add(firstOrder);
    currentCustomer.CurrentOrder = null;
}
// b Display monthly charged amounts breakdown & total charged amounts for the year
void advancedDisplayChargedAmounts()
{
    Console.Write("Enter the year: ");
    int year = 0;

    // check if year is valid
    if (!int.TryParse(Console.ReadLine(), out year))
    {
        Console.WriteLine("Invalid year value\r\n");
        return;
    }

    DateOnly dateyear = new DateOnly();
    // parse year into DateOnly and return if nonsesical
    if (year > 2020)
    {
         dateyear = new DateOnly(year - 1, 1, 1);
    }
    else
    {
        Console.WriteLine("ha ha real funny - next time put a plausible date\r\n");
        return;
    }

    double yearTotal = 0;
    // loop 11 times (each month in a year starting from jan till dec)
    Console.WriteLine();
    for (int i = 1; i < 13; i++)
    {
        double monthTotal = 0;
        foreach (Order order in ordersList)
        {
            // check if order is fulfilled within the month of that year
            if (order.TimeFulfilled != null && order.TimeFulfilled.Value.Month == dateyear.Month && order.TimeFulfilled.Value.Year == dateyear.Year)
            {
                monthTotal += order.CalculateTotal();
            } 
        }
        // display monthcost per month of the year
        Console.WriteLine("{0} {1,5} ${2:0.00}", dateyear.ToString("MMM"),  dateyear.Year.ToString() + ":" ,  monthTotal);

        // add monthTotal to yearTotal
        yearTotal += monthTotal;

        // increment month until loop is done
        dateyear = dateyear.AddMonths(1);
    }
    // display yeartotal
    Console.WriteLine("\r\nTotal:    ${1:0.00}", dateyear.Year, yearTotal);

}

// initializing test customer 

Customer TestCustomer = new Customer("Test the Tester", 112233, DateTime.ParseExact("01/11/1966", "dd/MM/yyyy", null));
TestCustomer.Rewards = new PointCard(0, 0);
Order TestOrder = new Order(37, DateTime.ParseExact("27/10/2023 13:28", "dd/MM/yyyy HH:mm", null));

//initializing ice creams for current order 

IceCream TestCup = new Cup
    (
    "Cup",
    1, 
    new List<Flavour> { new Flavour("Vanilla", false, 1) }, 
    new List<Topping> { new Topping("Oreos") }
    );

Console.WriteLine(TestCup.ToString());
Console.WriteLine(TestCup.Flavours[0].ToString());

IceCream TestCone = new Cone
    ("Cone", 
    2,
    new List<Flavour> { new Flavour("Vanilla", false, 2) },
    new List<Topping> { new Topping("Oreos") }, 
    false);
Console.WriteLine(TestCone.ToString());
Console.WriteLine(TestCone.Flavours[0].ToString());

IceCream TestWaffle = new Waffle
    ("Waffle",
    3,
    new List<Flavour> { new Flavour("Vanilla", false, 3) },
    new List<Topping> { new Topping("Oreos") },
    "Original"
    );
Console.WriteLine(TestWaffle.ToString());
Console.WriteLine(TestWaffle.Flavours[0].ToString());

TestOrder.IceCreamList.Add(TestCup);
TestOrder.IceCreamList.Add(TestCone);
TestOrder.IceCreamList.Add(TestWaffle);

TestCustomer.CurrentOrder = TestOrder;

customersList.Add(TestCustomer);
ordersList.Add(TestOrder);
memberOrderDic.Add(TestOrder.Id, TestCustomer.MemberId);

foreach (Order order in ordersList)
{
    Console.WriteLine(order.Id);
}

foreach (IceCream ic in TestCustomer.CurrentOrder.IceCreamList)
{
    Console.WriteLine(ic.ToString());
}


Console.WriteLine();

// Start Program
MenuStart();