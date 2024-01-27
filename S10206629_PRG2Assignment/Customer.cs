
 class Customer
{
    // Initiliaze
    public string Name { get; set; }
    public int MemberId { get; set; }
    public DateTime Dob { get; set; }
    public Order CurrentOrder { get; set; }
    public List<Order> OrderHistory { get; set; }
    public PointCard Rewards { get; set; }

    // Constructors
    public Customer(){}

    public Customer(string name, int memberId, DateTime dob)
    {
        Name = name;
        MemberId = memberId;
        Dob = dob;
        OrderHistory = new List<Order>();
        Rewards = new PointCard();
    }

    // Class methods
    public Order MakeOrder()
    {
        CurrentOrder = new Order();
        return CurrentOrder;
    }

    public bool IsBirthday()
    {
        return DateTime.Today.Month == Dob.Month && DateTime.Today.Day == Dob.Day;
    }

    public override string ToString()
    {
        return $"Customer: {Name}, Member ID: {MemberId}, DOB: {Dob.ToShortDateString()}, Rewards Points: {Rewards.Points}";
    }
}