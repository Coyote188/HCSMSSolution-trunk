using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using HCSMS.Model;


namespace HCSMS.DataAccess
{
    public static class Utilities
    {
        
        public static string GetDbConnectionString( )
        {
            string connString=null;


            connString = "Server=SERVER\\SQLEXPRESS; Initial Catalog=restuarant; Integrated Security=SSPI;Asynchronous Processing=True;";
            //ConnectionStringSettingsCollection collect = ConfigurationManager.ConnectionStrings;
            //foreach(ConnectionStringSettings coll in collect)
            //connString = coll.ConnectionString;
           
            return connString;
        }
        public static SqlConnection GetConnection()
        {
            //if (conn == null)
                return new SqlConnection(Utilities.GetDbConnectionString());
           // else
            //    return conn;
        }
        public static void TransactionExecuteNonQuery(SqlConnection connection, List<SqlCommand> commandList)
        {
            SqlTransaction tran=null;
            try
            {
                connection.Open();
                tran = connection.BeginTransaction();
                foreach (SqlCommand comm in commandList)
                {
                    comm.Connection = connection;
                    comm.Transaction = tran;
                }                
                foreach (SqlCommand comm in commandList)
                {
                    comm.ExecuteNonQuery();
                }
                tran.Commit();
            }
            catch (SqlException sqlException)
            {
                tran.Rollback();
                throw sqlException;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
