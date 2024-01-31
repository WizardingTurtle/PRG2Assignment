//==========================================================
// Student Number : S10259948
// Student Name : Goh Jun Kai
// Partner Name : Rafol Emmanuel Legaspi
//==========================================================

//stuff to fix: fix advanced features, data validation

using S10206629_PRG2Assignment;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections;

namespace IceCreamShopManagement
{
    class Program
    {
        static List<Customer> customers = new List<Customer>();
        static List<Order> orders = new List<Order>();
        static Queue<Order> goldMemberOrders = new Queue<Order>();
        static Queue<Order> regularMemberOrders = new Queue<Order>();

        static void InitializeQueue()
        {
            foreach (Customer customer in customers)
            {
    
                if (customer.CurrentOrder != null && customer.CurrentOrder.IceCreamList.Count > 0 && customer.CurrentOrder.TimeFulfilled == null)
                {
                    bool NotExistGQ = goldMemberOrders.Any(o => o.Id == customer.CurrentOrder.Id) == false;
                    bool NotExistRQ = regularMemberOrders.Any(o => o.Id == customer.CurrentOrder.Id) == false;
                    if (NotExistGQ && NotExistRQ)
                    {
                        if (customer.Rewards.Tier == "Gold")
                        {
                            goldMemberOrders.Enqueue(customer.CurrentOrder);
                        }
                        else
                        {
                            regularMemberOrders.Enqueue(customer.CurrentOrder);
                        }
                    }
                }
            }
        }


        static void Main()
        {
            LoadData();  // Load data at the beginning

            // Main menu loop
            while (true)
            {
                Console.WriteLine("\n===== Ice Cream Shop Management System =====");
                Console.WriteLine("1. List all customers");
                Console.WriteLine("2. List all current orders");
                Console.WriteLine("3. Register a new customer");
                Console.WriteLine("4. Create a customer’s order");
                Console.WriteLine("5. Display order details of a customer");
                Console.WriteLine("6. Modify order details");
                Console.WriteLine("7. Access advanced menu");
                Console.WriteLine("8. Exit");

                Console.Write("Choose an option: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        ListAllCustomers();
                        break;
                    case "2":
                        ListAllCurrentOrders();
                        break;
                    case "3":
                        RegisterNewCustomer();
                        break;
                    case "4":
                        CreateCustomersOrder();
                        break;
                    case "5":
                        DisplayOrderDetailsOfCustomer();
                        break;
                    case "6":
                        ModifyOrderDetails();
                        break;
                    case "7":
                        AdvancedMenu();
                        break;
                    case "8":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void LoadData()
        {
            LoadCustomersFromCSV("C:\\Users\\user\\Downloads\\ProgrammingAs\\ProgramminAssign\\ProgramminAssign\\datafiles\\customers.csv");
            LoadOrdersFromCSV("C:\\Users\\user\\Downloads\\ProgrammingAs\\ProgramminAssign\\ProgramminAssign\\datafiles\\orders.csv");
        }

        

        static void LoadCustomersFromCSV(string filename)
        {
            try
            {
                string[] lines = File.ReadAllLines(filename);
                customers = new List<Customer>();

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] line = lines[i].Split(',');

                    // create customer
                    Customer customer = new Customer(line[0], Convert.ToInt32(line[1]), DateTime.ParseExact(line[2], "dd/MM/yyyy", null));
                    // modify customer's point card
                    customer.Rewards = new PointCard(Convert.ToInt32(line[4]), Convert.ToInt32(line[5]));
                    customer.Rewards.PunchCard = Convert.ToInt32(line[5]);
                    // add customer to list
                    customers.Add(customer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading customers from CSV: {ex.Message}");
            }
        }

        static void LoadOrdersFromCSV(string filename)
        {
            try
            {
                string[] lines = File.ReadAllLines(filename);
                orders = new List<Order>();
                Dictionary<int, int> memberOrderDic = new Dictionary<int, int>();

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] line = lines[i].Split(',');

                    // check if order exists before creating a new order
                    bool IdExist = orders.Any(item => item.Id == Convert.ToInt32(line[0]));

                    if (IdExist)
                    {
                        // Find index of order in list
                        int Oindex = orders.FindIndex(order => order.Id == Convert.ToInt32(line[0]));
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
                            orders[Oindex].IceCreamList.Add(new Cup(line[4], Convert.ToInt32(line[5]), new List<Flavour>(flavours), new List<Topping>(toppings)));
                        }
                        else if (line[4] == "Cone")
                        {
                            // check if cone is dipped
                            if (line[6] == "TRUE")
                            {
                                orders[Oindex].IceCreamList.Add(new Cone(line[4], Convert.ToInt32(line[5]), new List<Flavour>(flavours), new List<Topping>(toppings), true));
                            }
                            else
                            {
                                orders[Oindex].IceCreamList.Add(new Cone(line[4], Convert.ToInt32(line[5]), new List<Flavour>(flavours), new List<Topping>(toppings), false));
                            }
                        }
                        else if (line[4] == "Waffle")
                        {
                            orders[Oindex].IceCreamList.Add(new Waffle(line[4], Convert.ToInt32(line[5]), new List<Flavour>(flavours), new List<Topping>(toppings), line[7]));
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
                        // check icecream type create the object, then add the object to order
                        if (line[4] == "Cup")
                        {
                            order.IceCreamList.Add(new Cup(line[4], Convert.ToInt32(line[5]), new List<Flavour>(flavours), new List<Topping>(toppings)));
                        }
                        else if (line[4] == "Cone")
                        {
                            // check if cone is dipped
                            if (line[6] == "TRUE")
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
                        orders.Add(order);
                    }
                }

                // Add orders to customer order history
                foreach (Order order in orders)
                {
                    // check if order id exist in memberorderdic
                    if (memberOrderDic.Any(item => item.Key == order.Id))
                    {
                        // Find index of customer in customer list
                        int index = customers.FindIndex(item => item.MemberId == memberOrderDic[order.Id]);
                        customers[index].OrderHistory.Add(order);

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading orders from CSV: {ex.Message}");
            }
        }

        static void ListAllCustomers()
        {
            Console.WriteLine("\n===== List of All Customers =====");
        

            foreach (var customer in customers)
            {
                Console.WriteLine(customer.ToString());
            }
        }

        static void ListAllCurrentOrders()
        {
            Console.WriteLine();
            // List Gold Member Current Orders
            Console.WriteLine("Gold member's current orders");
            foreach (Order currentOrder in goldMemberOrders)
            {
                Console.WriteLine(currentOrder.ToString());
            }
            // List regular queue Current Orders
            Console.WriteLine("Regular queue current orders");
            foreach (Order currentOrder in regularMemberOrders)
            {
                Console.WriteLine(currentOrder.ToString());
            }
            Console.WriteLine();
        }

        static void RegisterNewCustomer()
        {
            // Prompt user for customer information
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine();

            // Validate customer name
            while (string.IsNullOrWhiteSpace(name) || name.Any(char.IsDigit) || name.All(char.IsWhiteSpace))
            {
                Console.Write("Invalid name. Enter a valid name: ");
                name = Console.ReadLine();
            }

            Console.Write("Enter customer ID number: ");
            int idNumber;
            while (!int.TryParse(Console.ReadLine(), out idNumber) || idNumber <= 0 || idNumber.ToString().Length != 6 || idNumber.ToString().StartsWith("0"))
            {
                Console.Write("Invalid ID. Enter a valid 6-digit ID (cannot start with zero): ");
            }

            Console.Write("Enter customer date of birth (dd/MM/yyyy): ");
            DateTime dob;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, DateTimeStyles.None, out dob) || dob > DateTime.Now)
            {
                Console.Write("Invalid date of birth. Enter a valid date (dd/MM/yyyy): ");
            }

            // Create a customer object with the provided information
            Customer newCustomer = new Customer(name, idNumber, dob);

            // Create a PointCard object and assign it to the customer
            newCustomer.Rewards = new PointCard(0, 0); // You might want to initialize points and punch card values based on your logic

            // Check for duplicate customer ID
            while (customers.Any(c => c.MemberId == idNumber))
            {
                Console.Write("Customer with the same ID already exists. Enter a different ID: ");
                idNumber = Convert.ToInt32(Console.ReadLine());
            }

            // Append the customer information to the customers.csv file
            AppendCustomerToCSV(newCustomer);

            // Add the new customer to the list
            customers.Add(newCustomer);

            Console.WriteLine("Customer registered successfully!");
        }

        static void AppendCustomerToCSV(Customer customer)
        {
            try
            {
                // Append the customer information to the customers.csv file
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

        static void CreateCustomersOrder()
        {
            // List customers from customers.csv
            Console.WriteLine("\n===== List of All Customers =====");
            Console.WriteLine("{0,-15} {1,-10} {2,-15} {3,-10} {4,-15} {5,-10}",
                "Name", "Member ID", "DOB", "Membership", "Points", "Punch Card");

            foreach (var customer in customers)
            {
                Console.WriteLine("{0,-15} {1,-10} {2,-15} {3,-10} {4,-15} {5,-10}",
                    customer.Name, customer.MemberId, customer.Dob.ToShortDateString(),
                    customer.Rewards.Tier, customer.Rewards.Points, customer.Rewards.PunchCard);
            }

            // Prompt user to select a customer
            Console.Write("\nEnter the Member ID of the customer: ");
            int selectedMemberId;

            while (!int.TryParse(Console.ReadLine(), out selectedMemberId) || selectedMemberId <= 0)
            {
                Console.Write("Invalid Member ID. Enter a valid Member ID: ");
            }

            // Retrieve the selected customer
            Customer selectedCustomer = customers.FirstOrDefault(c => c.MemberId == selectedMemberId);

            if (selectedCustomer != null)
            {
                // Create an order object with the current timestamp as the ID
                Order newOrder = new Order((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds, DateTime.Now);

                do
                {
                    // Prompt user to enter ice cream order
                    string option;
                    do
                    {
                        Console.Write("\nEnter ice cream option (Cup, Cone, Waffle): ");
                        option = Console.ReadLine()?.Trim();
                        if (!IsValidIceCreamOption(option))
                        {
                            Console.WriteLine("Invalid ice cream option. Please enter Cup, Cone, or Waffle.");
                        }
                    } while (!IsValidIceCreamOption(option));

                    Console.Write("Enter number of scoops (1-3): ");
                    int scoops;
                    while (!int.TryParse(Console.ReadLine(), out scoops) || scoops < 1 || scoops > 3)
                    {
                        Console.WriteLine("Invalid number of scoops. Please enter a number between 1 and 3.");
                        Console.Write("Enter number of scoops (1-3): ");
                    }

                    // Create lists to store flavors and toppings
                    List<Flavour> flavours = new List<Flavour>();
                    List<Topping> toppings = new List<Topping>();

                    // Prompt user to enter flavors
                    for (int i = 1; i <= scoops; i++)
                    {
                        string flavorInput;
                        do
                        {
                            Console.Write($"Enter flavor {i} (Vanilla, Chocolate, Strawberry, Durian, Ube, Sea Salt): ");
                            flavorInput = Console.ReadLine()?.Trim();
                            if (!IsValidFlavor(flavorInput))
                            {
                                Console.WriteLine("Invalid flavor. Please enter a valid flavor with a capital letter.");
                            }
                        } while (!IsValidFlavor(flavorInput));

                        bool premium = IsPremiumFlavor(flavorInput);
                        flavours.Add(new Flavour(flavorInput, premium, 1));
                    }

                    // Prompt user to enter toppings
                    for (int i = 1; i <= 4; i++)
                    {
                        Console.Write($"Enter topping {i} (Sprinkles, Mochi, Sago, Oreos, or leave blank to skip): ");
                        string toppingInput = Console.ReadLine()?.Trim();

                        if (string.IsNullOrWhiteSpace(toppingInput))
                        {
                            // Skip topping if input is empty
                            break;
                        }

                        while (!IsValidTopping(toppingInput))
                        {
                            Console.Write($"Invalid topping. Enter topping {i} (Sprinkles, Mochi, Sago, Oreos): ");
                            toppingInput = Console.ReadLine()?.Trim();
                        }

                        toppings.Add(new Topping(toppingInput));
                    }

                    // Create the proper ice cream object with the information given
                    IceCream iceCream;

                    switch (option.ToLower())
                    {
                        case "cup":
                            iceCream = new Cup(option, scoops, flavours, toppings);
                            break;
                        case "cone":
                            Console.Write("Is the cone dipped? (Y/N): ");
                            bool isDipped = Console.ReadLine()?.ToUpper() == "Y";
                            iceCream = new Cone(option, scoops, flavours, toppings, isDipped);
                            break;
                        case "waffle":
                            Console.Write("Enter waffle flavour: ");
                            string waffleFlavour = Console.ReadLine()?.Trim();
                            iceCream = new Waffle(option, scoops, flavours, toppings, waffleFlavour);
                            break;
                        default:
                            Console.WriteLine("Invalid ice cream option. Please try again.");
                            continue;
                    }

                    // Add the ice cream object to the order
                    newOrder.IceCreamList.Add(iceCream);

                    // Prompt the user if they would like to add another ice cream
                    Console.Write("Do you want to add another ice cream to the order? (Y/N): ");
                } while (Console.ReadLine()?.ToUpper() == "Y");

                // Remove the existing order from the queue
                if (selectedCustomer.CurrentOrder != null)
                {
                    if (selectedCustomer.Rewards.Tier == "Gold")
                    {
                        goldMemberOrders = new Queue<Order>(goldMemberOrders.Where(order => order != selectedCustomer.CurrentOrder));
                    }
                    else
                    {
                        regularMemberOrders = new Queue<Order>(regularMemberOrders.Where(order => order != selectedCustomer.CurrentOrder));
                    }
                }

                // Link the new order to the customer's current order
                selectedCustomer.CurrentOrder = newOrder;

                // Append the order to the appropriate order queue
                if (selectedCustomer.Rewards.Tier == "Gold")
                {
                    goldMemberOrders.Enqueue(newOrder);
                }
                else
                {
                    regularMemberOrders.Enqueue(newOrder);
                }

                Console.WriteLine("Order has been made successfully!");
            }
            else
            {
                Console.WriteLine("Invalid Member ID. Customer not found.");
            }
        }

        static bool IsValidIceCreamOption(string option)
        {
            return option.Equals("Cup", StringComparison.OrdinalIgnoreCase) && char.IsUpper(option[0]) ||
                   option.Equals("Cone", StringComparison.OrdinalIgnoreCase) && char.IsUpper(option[0]) ||
                   option.Equals("Waffle", StringComparison.OrdinalIgnoreCase) && char.IsUpper(option[0]);
        }

        static bool IsValidFlavor(string flavor)
        {
            string[] validFlavors = { "Vanilla", "Chocolate", "Strawberry", "Durian", "Ube", "Sea Salt" };
            return validFlavors.Contains(flavor, StringComparer.OrdinalIgnoreCase) && char.IsUpper(flavor[0]);
        }

        static bool IsValidTopping(string topping)
        {
            string[] validToppings = { "Sprinkles", "Mochi", "Sago", "Oreos" };
            return validToppings.Contains(topping, StringComparer.OrdinalIgnoreCase);
        }

        static bool IsPremiumFlavor(string flavor)
        {
            string[] premiumFlavors = { "Durian", "Ube", "Sea Salt" };
            return premiumFlavors.Contains(flavor, StringComparer.OrdinalIgnoreCase);
        }

        static void DisplayOrderDetailsOfCustomer()
        {
            // Display Customers
            Console.WriteLine();
            Console.WriteLine("{0,-10} {1}", "Member Id", "Member Name");
            foreach (Customer customer in customers)
            {
                Console.WriteLine("{0,-10} {1}", customer.MemberId, customer.Name);
            }
            Console.WriteLine();

            // Input
            Console.Write("Select a customer by ID: ");

            // checks if input is valid int else return to menu
            int Id = 0;
            if (!int.TryParse(Console.ReadLine(), out Id))
            {
                Console.WriteLine("Invalid ID");
                return;
            }
            // checks if id exists in customerList else return to menu
            bool IdNoExist = customers.Any(c => c.MemberId == Id) == false;
            if (IdNoExist)
            {
                Console.WriteLine("ID does not exist");
                return;
            }

            // Display Customer's Orders
            int index = customers.FindIndex(c => c.MemberId == Id);
            Console.WriteLine();
            Console.WriteLine("Current Order ---");
            if (customers[index].CurrentOrder != null)
            {
                Console.WriteLine(customers[index].CurrentOrder.ToString());
                foreach (IceCream ic in customers[index].CurrentOrder.IceCreamList)
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
            foreach (Order order in customers[index].OrderHistory)
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

        static void ModifyOrderDetails()
        {
            // Display Customers
            Console.WriteLine("{0,-10} {1}", "Member Id", "Member Name");
            foreach (Customer customer in customers)
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
            bool IdNoExist = customers.Any(c => c.MemberId == Id) == false;
            if (IdNoExist)
            {
                Console.WriteLine("ID does not exist");
                return;
            }

            // Get index of customer in customerlist and display the icecreams of the currentorder
            int index = customers.FindIndex(c => c.MemberId == Id);
            if (customers[index].CurrentOrder != null)
            {
                Console.WriteLine("Icecreams in current order");
                Console.WriteLine("{0,-8} {1}", "Index", "Details");

                foreach (IceCream ic in customers[index].CurrentOrder.IceCreamList)
                {
                    // find index of icecream in list
                    int indexic = customers[index].CurrentOrder.IceCreamList.FindIndex(i => i == ic);
                    Console.WriteLine("{0,-8} {1}", (indexic + 1).ToString() + ".", ic.ToString());
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
                    if (customers[index].CurrentOrder.IceCreamList.Count > 1)
                    {
                        Console.Write("Choose an ice cream to modify by index: ");
                        int indexic = 0;
                        if (!int.TryParse(Console.ReadLine(), out indexic))
                        {
                            Console.WriteLine("Invalid ID");
                            return;
                        }

                        // Modify icecream details
                        customers[index].CurrentOrder.ModifyIceCream(indexic - 1);
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
                    for (int i = 0; i < scoops; i++)
                    {
                        Console.Write("Enter Flavour {0}: ", i + 1);
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
                        Console.Write("Enter Topping {0}: ", i + 1);
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
                        customers[index].CurrentOrder.AddIceCream(new Cup(type, scoops, flavList, topList));
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
                        customers[index].CurrentOrder.AddIceCream(new Cone(type, scoops, flavList, topList, dip));
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
                            if (read == "Original" || read == "Red Velvet" || read == "Charcoal" || read == "Pandan")
                            {
                                wafFlav = read;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Waffle Flavour");
                            }
                        }
                        customers[index].CurrentOrder.AddIceCream(new Waffle(type, scoops, flavList, topList, wafFlav));
                        Console.WriteLine("Ice cream waffle added to current order!");
                    }


                }
                // Delete ice cream from order
                else if (option == "3")
                {
                    if (customers[index].CurrentOrder.IceCreamList.Count > 1)
                    {
                        Console.Write("Choose an ice cream to remove by index: ");
                        int indexic = Convert.ToInt32(Console.ReadLine()) - 1;

                        Console.Write("ARE YOU SURE? Enter DELETE to confirm or enter anything else to invalidate operation: ");
                        string read = Console.ReadLine();

                        if (read == "DELETE")
                        {
                            customers[index].CurrentOrder.IceCreamList.RemoveAt(indexic);
                            Console.WriteLine("Ice Cream removed, returning to menu\r\n");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Operation invalid, returning to modify menu\r\n");
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
        //advanced
        //advanced menu

        static void AdvancedMenu()
        {
            while (true)
            {
                InitializeQueue();
                foreach (Order order in regularMemberOrders)
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
                    "--------- Advanced Features Menu ---------\r\n" +
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
                    ProcessAndCheckoutOrder();
                }
                // feature b 
                else if (option == "2")
                {
                    DisplayMonthlyChargesForYear();
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
        static void ProcessAndCheckoutOrder()
        {
            Console.WriteLine();
            Order firstOrder = null;
            // check & dequeue for goldqueue before regular queue - return if no orders in either queues
            if (goldMemberOrders.Count > 0)
            {
                firstOrder = goldMemberOrders.Dequeue();
            }
            else if (regularMemberOrders.Count > 0)
            {
                firstOrder = regularMemberOrders.Dequeue();
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
            int index = customers.FindIndex(c => c.CurrentOrder != null && c.CurrentOrder.Id == firstOrder.Id);
            currentCustomer = customers[index];

            // Display customer punchard details
            Console.WriteLine("Customer {0}'s PunchCard details", currentCustomer.Name);
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
                Console.WriteLine("PointCard points: {0}", currentCustomer.Rewards.Points);
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
        //b
        static void DisplayMonthlyChargesForYear()
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
                foreach (Order order in orders)
                {
                    // check if order is fulfilled within the month of that year
                    if (order.TimeFulfilled != null && order.TimeFulfilled.Value.Month == dateyear.Month && order.TimeFulfilled.Value.Year == dateyear.Year)
                    {
                        monthTotal += order.CalculateTotal();
                    }
                }
                // display monthcost per month of the year
                Console.WriteLine("{0} {1,5} ${2:0.00}", dateyear.ToString("MMM"), dateyear.Year.ToString() + ":", monthTotal);

                // add monthTotal to yearTotal
                yearTotal += monthTotal;

                // increment month until loop is done
                dateyear = dateyear.AddMonths(1);
            }
            // display yeartotal
            Console.WriteLine("\r\nTotal:    ${1:0.00}", dateyear.Year, yearTotal);

        }




    }

}

