using System;
using System.Data;
using System.Data.SqlClient;

namespace TaxiServer.DB
{
    public class DataBase
    {
        private MainWindow _mainWindow;
        SqlDataAdapter _dataAdapter;
        DataTable _dataTable;
        DataRowView _dataRow;

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

            _mainWindow.driversList.DataContext = _dataTable.DefaultView;
        }
    }
}
