using System;
using Microsoft.Data.SqlClient;
using BKR.Classes;

namespace BKR.Utilities
{
    public class SQLUtilities
    {
        public static void SQLExecute(string SQLString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Constants.SQL_CONNECTION_STRING))
                {
                    connection.Open();
                    string sql = SQLString;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void ClearTables()
        {
            SQLExecute("truncate table tblContract");
            SQLExecute("truncate table tblCustomer");
            SQLExecute("truncate table tblBKR_Delta");
            SQLExecute("truncate table tblBKR_Master");
            SQLExecute("truncate table tblRegistration");

        }
    }
}
