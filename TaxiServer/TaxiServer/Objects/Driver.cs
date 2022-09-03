using System;
using TaxiServer.DB;

namespace TaxiServer
{
    public class Driver : IDBObject
    {
        public int CallSign { get;private set; }
        public int TelegramID { get; private set; }
        public bool WaitingWorOrders { get; private set; }
        public string Name { get; private set; }
        public string Automobile { get; private set; }
        public string PhoneNumber { get; private set; }
        public int RegStatus { get; private set; }
        public int Balans { get; private set; }
        public int ReviewCount { get; private set; }
        public int StarCount { get; private set; }
        public float Reputation
        {
            get
            {
                return (float)StarCount/(float)ReviewCount * 5;
            }
        }
        public string[] DBFields { get { return new string[] {TelegramID.ToString(),Name,Automobile,PhoneNumber};}}

        public string AddToDBCommand
        {
            get
            {
                return $"INSERT INTO Driver (TelegramID, Name, Automobile, PhoneNumber) " +
                    $"VALUES ({TelegramID},'{Name}','{Automobile}','{PhoneNumber}')";
            }
        }

        public Driver(int callSign, int telegramID, bool waitingForOrders, string name,
            string automobile, string phoneNumber, int regStatus, int balans, int reviewCount, int starCount)
        {
            CallSign = callSign;
            TelegramID = telegramID;
            WaitingWorOrders = waitingForOrders;
            Name = name;
            Automobile = automobile;
            PhoneNumber = phoneNumber;
            RegStatus = regStatus;
            Balans = balans;
            ReviewCount = reviewCount;
            StarCount = starCount;
        }

        public Driver(int telegramId)
        {
            CallSign = 0;
            TelegramID = telegramId;
            WaitingWorOrders= false;
            Name = "";
            Automobile = "";
            PhoneNumber = "";
            RegStatus = 0;
            Balans=0;
            ReviewCount=0;
            StarCount=0;
        }

        public void EditDriver( string name, string automobile, string phoneNumber)
        {
            Name = name;
            Automobile = automobile;
            PhoneNumber = phoneNumber;
        }
    }
}
