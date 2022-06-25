using System;
using System.Collections.ObjectModel;
using Telegram.Bot.Args;

namespace TaxiServer
{
    public class DriversManager
    {
        public ObservableCollection<Driver> Drivers { get; private set; }

        private Storage _storage = new Storage();

        private MainWindow _mainWindow;

        public DriversManager(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;

            Drivers = _storage.LoadDrivers();
        }

        public void AddDriver(Driver newDriver)
        {
            if (!Drivers.Contains(newDriver))
            {
                Drivers.Add(newDriver);
            }

            Save();
        }

        public void AddDriver()
        {
            //Drivers.Add(new Driver());
        }

        public void RemoveDriver(int index)
        {
            Drivers.Remove(Drivers[index]);

            Save();
        }

        public void Save()
        {
            _storage.SaveDrivers(Drivers);
        }

        public Driver GetDriver(string id)
        {
            foreach (Driver driver in Drivers)
            {
                if (driver.TelegramID == id) return driver;
            }
            return null;
        }
    }
}
