using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TaxiServer.DB
{
    internal class SqlCommandHandler
    {
        private SqlConnection _connection;
        string _tableName;

        public SqlCommandHandler(SqlConnection sqlConnection, string tableName)
        {
            _connection = sqlConnection;
            _tableName = tableName;
        }

        public void AddRow(IDBObject dBObject)
        {
            SqlCommand cmd = new SqlCommand(dBObject.AddToDBCommand, _connection);
            cmd.ExecuteNonQuery();
        }

        public async Task<DriverRegStatus> GetRegStatus(string TelegramID)
        {
            string sqlExpression = "SELECT * FROM Driver";

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            SqlDataReader reader = await command.ExecuteReaderAsync();

            return DriverRegStatus.notRegistered;
        }
    }
}
