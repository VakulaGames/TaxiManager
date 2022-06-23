using System;
using System.Collections.ObjectModel;

namespace TaxiServer
{
    public class DriversManager
    {
        public ObservableCollection<Driver> Drivers { get; private set; }

        private Storage _storage = new Storage();

        public DriversManager()
        {
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

        public void RemoveDriver(int index)
        {
            Drivers.Remove(Drivers[index]);

            Save();
        }

        public void Save()
        {
            _storage.SaveDrivers(Drivers);
        }
    }
}
