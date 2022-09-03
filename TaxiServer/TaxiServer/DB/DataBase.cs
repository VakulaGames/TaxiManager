using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace TaxiServer.DB
{
    public class DataBase
    {
        private MainWindow _mainWindow;
        SqlDataAdapter _dataAdapter;
        DataTable _dataTable;

        public DataBase(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            Init();
        }

        private static SqlConnectionStringBuilder _strCon = new SqlConnectionStringBuilder()
        {
            DataSource = @"(localdb)\MSSQLLocalDB",
            InitialCatalog = @"taxi_db",
            IntegratedSecurity = true
        };

        public SqlConnection sqlConnection = new SqlConnection() { ConnectionString = _strCon.ConnectionString };

        private void Init()
        {
            try
            {
                sqlConnection.Open();
            }
            catch (Exception e)
            {
                _mainWindow.ShowMessage(e.Message);
            }

            _dataTable = new DataTable();
            _dataAdapter = new SqlDataAdapter();

            string sql = @"SELECT * FROM Driver";
            _dataAdapter.SelectCommand = new SqlCommand(sql, sqlConnection);

            _dataAdapter.Fill(_dataTable);
        }

        public void DisplayNewRow(string ID)
        {
            string sql = $"SELECT * FROM Driver WHERE TelegramID = {ID}";
            _dataAdapter.SelectCommand = new SqlCommand(sql, sqlConnection);

            _dataAdapter.Fill(_dataTable);
        }

        public ObservableCollection<Driver> AllDrivers()
        {
            string sqlExpression = $"SELECT * FROM Driver";
            SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            ObservableCollection<Driver> drivers = new ObservableCollection<Driver>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Driver driver = new Driver(
                        int.Parse(reader[0].ToString()),
                        int.Parse(reader[1].ToString()),
                        bool.Parse(reader[2].ToString()),
                        reader[3].ToString(),
                        reader[4].ToString(),
                        reader[5].ToString(),
                        int.Parse(reader[6].ToString()),
                        int.Parse(reader[7].ToString()),
                        int.Parse(reader[9].ToString()),
                        int.Parse(reader[10].ToString())
                        );
                    drivers.Add(driver);
                }
            }

            reader.Close();
            return drivers;
        }
    }
}
