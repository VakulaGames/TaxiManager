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

namespace TaxiServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DriversManager _driversManager;
        Storage _storage;

        public MainWindow()
        {
            InitializeComponent();

            _driversManager = new DriversManager();
            _storage = new Storage();

            driversList.ItemsSource = _driversManager.Drivers;

        }

        private void driversList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (driversList.SelectedIndex != -1)
            {
                selectedDriver.Text = _driversManager.Drivers[driversList.SelectedIndex].Name;
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
