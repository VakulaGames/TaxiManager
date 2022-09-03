using System;
using System.Collections.ObjectModel;
using TaxiServer.DB;
using Telegram.Bot.Args;

namespace TaxiServer
{
    public class DriversManager
    {
        public ObservableCollection<Driver> Drivers { get; private set; }

        private MainWindow _mainWindow;
        private DataBase _dataBase;

        public DriversManager(MainWindow mainWindow, DataBase dataBase)
        {
            _mainWindow = mainWindow;
            _dataBase = dataBase;
            Drivers = _dataBase.AllDrivers();
        }

        public Driver GetDriver(int telegramID)
        {
            foreach (Driver driver in Drivers)
            {
                if (driver.TelegramID == telegramID) return driver;
            }
            return null;
        }
    }
}
