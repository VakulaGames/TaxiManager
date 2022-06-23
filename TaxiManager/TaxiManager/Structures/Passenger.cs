namespace TaxiManager
{
    public struct Passenger
    {
        public string TelegramID { get; }
        public string Name { get; }
        public string PhoneNumber { get; }
        
        public Passenger(string ID, string name, string phoneNumber)
        {
            TelegramID = ID;
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}
