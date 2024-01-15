//==========================================================
// Student Number : S10206629
// Student Name : Rafol Emmanuel Legaspi
// Partner Name : Goh Jun Kai
//==========================================================

// To add code to create customers and orders from data files
// Read and create customers and pointcards based on customers.csv
using S10206629_PRG2Assignment;

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

    customersList.Add(customer)
}

// Read and create orders based on orders.csv
string fileName = ".\\datafiles\\orders.csv";
string[] lines = File.ReadAllLines(fileName);
List<Order> ordersList = new List<Order>();
for (int i = 1; i < lines.Length; i++)
{
    string[] line = lines[i].Split(',');

    // check if order exists before creating a new order
    bool IdExist = ordersList.Any(item => item.Id == Convert.ToInt32(line[0]);

    if (IdExist) 
    {
        //Placeholder
    }

    else 
    { 
        // Create order object
        Order order = new Order(Convert.ToInt32(line[0]), Convert.ToDateTime(line[2]);
        order.TimeFulfilled = Convert.ToDateTime(line[3]);

        // Create Flavour List
        List<Flavour> flavours = new List<Flavour>();
        for (int f = 8; f < 11; f++)
        {   
            //check if flavour is not null
            if (line[f] != '') 
            {
                //check if flavour exists in list
                bool TypeExist = flavours.Any(item => item.Type == line[f]);
                // increment quantity of flavour if true
                if (TypeExist) 
                {
                    int index = myList.FindIndex(flav => flav.Contains(line[f]));
                    flavours[index].Quantity += 1;
                }

                // Create flavour and add to list
                else
                {
                    //check if flavour is premium
                    if (line[f] == "Durian" || line[f] == "Ube" || line[f] == "Sea Salt")
                    {
                        bool premium = true; 
                    }
                    else
                    {
                        bool premium = false;
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
            if (line[t] != '')
            {
                toppings.Add(new Topping(line[t]))
            }
        }
        // Create Icecream object
        IceCream ic = new IceCream(line[4], Convert.ToInt32(line[5]), flavours, toppings);

        // Painstakingly check icecream type and flavours and create the object
        if (line[4] == "Cup")
        {
            IceCream ic = new Cup();
        }
        else if (line[4] == "Cone")
        {
            IceCream ic = new Cone();
        }
        else if (line[4] == "Waffle")
        {
            IceCream ic = new Waffle();
        }
    }

    customersList.Add(customer)
}
//Main Program Loop
void MenuStart()
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
    Console.Write("Please Choose an option");
    String option = Console.ReadLine(); 
    
    if ( option = '1' )
    {
        //Placeholder
    }
    else if (option = '2')
    {
        //Placeholder
    }
    else if (option = '3')
    {
        //Placeholder
    }
    else if (option = '4')
    {
        //Placeholder
    }
    else if (option = '5')
    {
        //Placeholder
    }
    else if (option = '6')
    {
        //Placeholder
    }
    else if (option = '0')
    {
        Console.WriteLine("Closing the program - bye bye!");
        break;
    }

//Feature 1 List all customers

//Feature 2 List all current orders

//Feature 3 Register a new customer

//Feature 4 Create a customer’s order

//Feature 5 Display order details of a customer

//Feature 6 ) Modify order details