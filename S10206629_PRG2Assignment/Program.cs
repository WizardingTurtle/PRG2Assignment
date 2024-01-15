//==========================================================
// Student Number : S10206629
// Student Name : Rafol Emmanuel Legaspi
// Partner Name : Goh Jun Kai
//==========================================================

// To add code to create customers and orders from data files

// Read and create customers and pointcards based on customers.csv
string fileName = "~\\datafiles\\customers.csv";
string[] lines = File.ReadAllLines(fileName);

List<Customer> customersList = new List<Customer>();
for (int i = 1; i < lines.Length; i++)
{
    string[] line = lines[i].Split(',');

    // create customer
    Customer customer = new Customer(line[0], Convert.ToInt32(line[1]), Convert.ToDateTime(line[2]));
    // modify customer's point card
    customer.Rewards = new PointCard(Convert.ToInt32(line[4]), Convert.ToInt32(line[5]));

    customersList.Add(customer)
}

// Read and create orders based on orders.csv

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