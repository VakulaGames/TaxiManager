using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaxiServer.DB;

namespace TaxiServer
{
    public partial class MainWindow : Window
    {
        private DriversManager _driversManager;
        private Storage _storage;
        private DriverBot _driverBot;
        private DataBase _dataBase;

        public MainWindow()
        {
            InitializeComponent();

            //_driversManager = new DriversManager(this);
            //_storage = new Storage();
            _dataBase = new DataBase(this);
            _driverBot = new DriverBot(this, _dataBase);
            _driverBot.OnRegisteredEvent += UpdateTab;

            //driversList.ItemsSource = _driversManager.Drivers;

        }

        public void ShowMessage(string text)
        {
            MessageBox.Show(text);
        }

        private void UpdateTab(string ID)
        {
            this.Dispatcher.Invoke(() =>
            {
                _dataBase.DisplayNewRow(ID);
            });
        }

        private void driversList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (driversList.SelectedIndex != -1)
            {
                selectedDriver.Text = $"{driversList.SelectedIndex}" ;
            }
            else
            {
                selectedDriver.Text = "";
            }
        }

        private void DeleteDriver(object sender, RoutedEventArgs e)
        {
            _driversManager.Drivers.RemoveAt(driversList.SelectedIndex);
            _storage.SaveDrivers(_driversManager.Drivers);
        }

        private void AddNewDriver(object sender, RoutedEventArgs e)
        {
            new DriverEdit(_driversManager).Show();
        }

        private void EditDriver(object sender, RoutedEventArgs e)
        {
            if (driversList.SelectedIndex != -1)
            {
                int index = driversList.SelectedIndex;
                new DriverEdit(_driversManager, index).Show();
            }
            else
            {
                MessageBox.Show("Сначала выберите водителя");
            }
        }

    }
}
