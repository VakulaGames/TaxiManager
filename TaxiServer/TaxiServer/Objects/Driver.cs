using System;
using TaxiServer.DB;

namespace TaxiServer
{
    public class Driver : IDBObject
    {
        public string TelegramID { get; private set; }
        public string Name { get; private set; }
        public string Automobile { get; private set; }
        public string PhoneNumber { get; private set; }
        public string[] DBFields { get { return new string[] {TelegramID,Name,Automobile,PhoneNumber};}}

        public string AddToDBCommand
        {
            get
            {
                return $"INSERT INTO Driver (TelegramID, Name, Automobile, PhoneNumber) " +
                    $"VALUES ('{TelegramID}','{Name}','{Automobile}','{PhoneNumber}')";
            }
        }

        public Driver(string id)
        {
            TelegramID = id;
            Name = "";
            Automobile = "";
            PhoneNumber = "";
        }

        public void EditDriver( string name, string automobile, string phoneNumber)
        {
            Name = name;
            Automobile = automobile;
            PhoneNumber = phoneNumber;
        }
    }
}
