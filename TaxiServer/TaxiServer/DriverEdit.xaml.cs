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
using System.Windows.Shapes;

namespace TaxiServer
{
    /// <summary>
    /// Логика взаимодействия для DriverEdit.xaml
    /// </summary>
    public partial class DriverEdit : Window
    {
        private Driver _driver;
        private DriversManager _driversManager;
        private int _indexSelectDriver;

        /// <summary>
        /// initialize window for Add new driver
        /// </summary>
        /// <param name="driversManager"></param>
        /// <param name="index"></param>
        public DriverEdit(DriversManager driversManager)
        {
            InitializeComponent();
            this.Title = "Создание нового водителя";
            _driversManager = driversManager;
            _driver = new Driver("0");
            //UpdateFields();
        }

        /// <summary>
        /// initialize window for edit driver
        /// </summary>
        /// <param name="driversManager"></param>
        /// <param name="index"></param>
        public DriverEdit(DriversManager driversManager, int index)
        {
            InitializeComponent();
            this.Title = "Изменение водителя";
            _driversManager = driversManager;
            _indexSelectDriver = index;
            //UpdateFields();
        }

        //private void UpdateFields()
        //{
        //    this.callSign.Text = _driver.Callsign;
        //    this.name.Text = _driver.Name;
        //    this.automobile.Text = _driver.Automobile;
        //}

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ApplyClick(object sender, RoutedEventArgs e)
        {
            //_driver.EditDriver(this.name.Text, this.automobile.Text, "Нужно добавить в DriverEdit", this.callSign.Text);

            //_driversManager.AddDriver(_driver);

            this.Close();
        }
    }
}
