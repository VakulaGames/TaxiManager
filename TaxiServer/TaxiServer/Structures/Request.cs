using System;

namespace TaxiServer
{
    public struct Request
    {
        public DateTime CreateData { get; }
        public Address Departure { get; }
        public Address Destination { get; }
        public Passenger Passenger { get; }
        public Driver Driver { get; private set; }
        public string Status { get; private set; }

        public Request(Address departure, Address destination, Passenger passanger)
        {
            CreateData = DateTime.Now;

            Departure = departure;
            Destination = destination;
            Passenger = passanger;

            Driver = new Driver();
            Status = "Поиск машины";
        }

        public void AssignDriver(Driver driver)
        {
            Driver = driver;
            Status = "водитель назначен";
        }

        public void CancellRequest()
        {
            Status = "Заявка отменена";
        }

        public void EndRequest()
        {
            Status = "Заявка завершена";
        }
    }
}
