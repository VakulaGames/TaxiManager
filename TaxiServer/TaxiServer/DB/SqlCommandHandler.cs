using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TaxiServer.DB
{
    internal class SqlCommandHandler
    {
        private SqlConnection _connection;
        string _tableName;

        internal SqlCommandHandler(SqlConnection sqlConnection, string tableName)
        {
            _connection = sqlConnection;
            _tableName = tableName;
        }

        internal async Task AddRow(IDBObject dBObject)
        {
            SqlCommand command = new SqlCommand(dBObject.AddToDBCommand, _connection);
            await command.ExecuteNonQueryAsync();
        }

        internal async Task<DriverRegStatus> GetRegStatus(string telegramID)
        {
            #region example

            //var sql = @"
            //SELECT 
            //    Workers.id as 'ID',
            //    Workers.workerName as 'workerName',
            //    Bosses.workerName  as 'bossesName',
            //    Bosses.departmentName  as 'departmentName',
            //    [Description].[value] as 'descriptionValue'
            //FROM  Workers, Bosses, [Description]
            //WHERE Workers.idBoss = Bosses.id and Workers.idDescription = [Description].id
            //";

            #endregion

            string sqlExpression = $"SELECT * FROM Driver Where TelegramID = {telegramID}";
            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            SqlDataReader reader = await command.ExecuteReaderAsync();

            int index = 0;

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    index = int.Parse(reader[6].ToString());
                }
            }
            await reader.CloseAsync();
            return GetRegStatusFromIndex(index);
        }

        internal async Task EditRow(string telegramID, string field, int value)
        {
            string sqlExpression = $"UPDATE Driver SET {field}={value} WHERE TelegramID='{telegramID}'";
            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            await command.ExecuteNonQueryAsync();
        }

        internal async Task EditRow(string telegramID, string field, string value)
        {
            string sqlExpression = $"UPDATE Driver SET {field}= N'{value}' WHERE TelegramID='{telegramID}'";
            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            await command.ExecuteNonQueryAsync();
        }

        internal async Task DeleteRow(string telegramID)
        {
            string sqlExpression = $"DELETE  FROM Driver WHERE TelegramID ='{telegramID.ToString()}'";

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            await command.ExecuteNonQueryAsync();
        }

        private DriverRegStatus GetRegStatusFromIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return DriverRegStatus.notRegistered;
                case 1:
                    return DriverRegStatus.nameInput;
                case 2:
                    return DriverRegStatus.autoInput;
                case 3:
                    return DriverRegStatus.phoneInput;
                case 4:
                    return DriverRegStatus.photoInput;
                case 5:
                    return DriverRegStatus.waitApproved;
                case 6:
                    return DriverRegStatus.approwed;
                default:
                    return DriverRegStatus.blocked;
            }
        }
    }
}
