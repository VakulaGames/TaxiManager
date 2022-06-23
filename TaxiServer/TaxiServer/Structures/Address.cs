namespace TaxiServer
{
    public struct Address
    {
        public string AddressName { get; }
        public string Location { get; }

        public Address(string addressName, string location)
        {
            AddressName = addressName;
            Location = location;
        }
    }
}
