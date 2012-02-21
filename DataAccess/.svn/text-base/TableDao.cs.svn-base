using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using HCSMS.Model;
using HCSMS.Model.Application;

namespace HCSMS.DataAccess
{
    public static class TableDao
    {
        public static DinningTable GetDinningTable(string tableNumber)
        {
            using (SqlConnection conn = Utilities.GetConnection())
            {
                SqlCommand comm = new SqlCommand("select Id, GuestAmount, ArrivedTime, Note from TableInUse where TableId= @tableNumber ", conn);

                comm.Parameters.Add("@tableNumber", SqlDbType.Char, 10);
                comm.Parameters["@tableNumber"].Value = tableNumber;

                try
                {
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();

                    if (reader.Read())
                    {
                        return covertToDinningTable(reader["ID"].ToString(), reader.GetInt32(1), reader.GetDateTime(2), reader["Note"].ToString());
                    }
                    else
                        return null;


                }
                catch (SqlException sqlException)
                {
                    throw new HCSMSException(sqlException.Message);
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }
        public static List<Table> GetAvailableTable()
        {
            List<Table> tableList = new List<Table>();
            using (SqlConnection conn = Utilities.GetConnection())
            {
                SqlCommand comm = new SqlCommand(@"select [Table].Id, Location, [Table].Name, SeatAmount, Status, TableType.Name as Type
                                                                                from [Table], TableType
                                                                                where ( [Table].Id not in (select TableId from TableInUse))
                                                                                           and ([Table].Id not in (select TableId from ReservationTable, CustomerReservation
                                                                                                                                where DATEADD(hour, 2, ArrivedTime)>=GetDate()
                                                                                                                                and ReservationTable.CustomerReservationId = CustomerReservation.Id))
                                                                                            and [Table].TableTypeId = TableType.Id", conn);

                try
                {
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        tableList.Add(convertToTable(reader["ID"].ToString(), reader["Location"].ToString(), reader["Name"].ToString(), reader.GetInt32(3), reader["Status"].ToString(), reader["Type"].ToString()));
                    }
                    return tableList;
                }
                catch (SqlException sqlException)
                {
                    throw new HCSMSException(sqlException.Message);
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }
        public static List<Table> GetAvailablePreorderTable(DateTime reserveDate)
        {
            List<Table> tableList = new List<Table>();
            using (SqlConnection conn = Utilities.GetConnection())
            {
                SqlCommand comm = new SqlCommand(@"select [Table].Id, Location, [Table].Name, SeatAmount, Status, TableType.Name as Type
                                                                                from [Table], TableType
                                                                                where ( [Table].Id not in (select TableId from ReservationTable, CustomerReservation
                                                                                                                                where DATEADD(hour, 2, ArrivedTime)>=@reserveDate
                                                                                                                                and ReservationTable.CustomerReservationId = CustomerReservation.Id)
                                                                                            and [Table].TableTypeId = TableType.Id", conn);

                comm.Parameters.Add("@reserveDate", SqlDbType.DateTime);
                comm.Parameters["@reserveDate"].Value = reserveDate;

                try
                {
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        tableList.Add(convertToTable(reader["ID"].ToString(), reader["Location"].ToString(), reader["Name"].ToString(), reader.GetInt32(3), reader["Status"].ToString(), reader["Type"].ToString()));
                    }
                    return tableList;
                }
                catch (SqlException sqlException)
                {
                    throw new HCSMSException(sqlException.Message);
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }
        public static List<Table> GetTableInReserve(string reserveId)
        {
            List<Table> tableList = new List<Table>();
            using (SqlConnection conn = Utilities.GetConnection())
            {
                SqlCommand comm = new SqlCommand(@"select [Table].Id, Location, [Table].Name, SeatAmount, Status, TableType.Name as Type
                                                                                from [Table], TableType, ReservationTable
                                                                                where [Table].Id = ReservationTable.TableId
                                                                                          and [Table].TableTypeId = TableType.Id
                                                                                          and ReservationTable.CustomerReservationId = @reserveId", conn);
                comm.Parameters.Add("@reserveId", SqlDbType.Char, 10);

                comm.Parameters["@reserveId"].Value = reserveId;

                try
                {
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        tableList.Add(convertToTable(reader["ID"].ToString(), reader["Location"].ToString(), reader["Name"].ToString(), reader.GetInt32(3), reader["Status"].ToString(), reader["Type"].ToString()));
                    }
                    return tableList;
                }
                catch (SqlException sqlException)
                {
                    throw new HCSMSException(sqlException.Message);
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }
        public static Table GetTable(string tableNumber)
        {            
            using (SqlConnection conn = Utilities.GetConnection())
            {
                SqlCommand comm = new SqlCommand(@"select [Table].Id, Location, [Table].Name, SeatAmount, Status, TableType.Name as Type
                                                                                from [Table], TableType
                                                                                where   and [Table].TableTypeId = TableType.Id
                                                                                            and [Table].Id = @tableId", conn);
                comm.Parameters.Add("@tableId", SqlDbType.Char, 10);

                comm.Parameters["@tableId"].Value = tableNumber;

                try
                {
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();

                    if (reader.Read())
                    {
                        return convertToTable(reader["ID"].ToString(), reader["Location"].ToString(), reader["Name"].ToString(), reader.GetInt32(3), reader["Status"].ToString(), reader["Type"].ToString());
                    }
                    else return null;
                }
                catch (SqlException sqlException)
                {
                    throw new HCSMSException(sqlException.Message);
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }
        public static List<Table> GetTable(QueryCriteria cirteria)
        {
            using (SqlConnection conn = Utilities.GetConnection())
            {
                SqlCommand comm = new SqlCommand(searchTable(cirteria), conn);               

                try
                {
                    conn.Open();
                    List<Table> tableList = new List<Table>();
                    SqlDataReader reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        tableList.Add(convertToTable(reader["ID"].ToString(), reader["Location"].ToString(), reader["Name"].ToString(), reader.GetInt32(3), reader["Status"].ToString(), reader["Type"].ToString()));
                    }
                    return tableList;
                }
                catch (SqlException sqlException)
                {
                    throw new HCSMSException(sqlException.Message);
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public static void ChangeTable(string dinningId, string oldTableId, string newTableId)
        {
            List<SqlCommand> commands = new List<SqlCommand>();

            commands.Add(changeOneTable(dinningId, oldTableId, newTableId));
           
            try
            {
                using (SqlConnection conn = Utilities.GetConnection())
                {
                    Utilities.TransactionExecuteNonQuery(conn, commands);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number >= 50000)
                {
                    throw new HCSMSException(ex.Message);
                }
                else
                {
                    throw new HCSMSException("Transaction Errors!", ex);
                }
            }
        }
        public static void MergeTable(string toBeMeregedDinningId, string meregeDinningId)
        {
            SqlCommand comm = new SqlCommand("mergeDinningTable");
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add("@toBeMeregedId", SqlDbType.Char, 10);
            comm.Parameters.Add("@meregeId", SqlDbType.Char, 10);

            comm.Parameters["@toBeMeregedId"].Value = toBeMeregedDinningId;
            comm.Parameters["@meregeId"].Value = meregeDinningId;

            List<SqlCommand> commands = new List<SqlCommand>();
            commands.Add(comm);

            try
            {
                using (SqlConnection conn = Utilities.GetConnection())
                {
                    Utilities.TransactionExecuteNonQuery(conn, commands);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number >= 50000)
                {
                    throw new HCSMSException(ex.Message);
                }
                else
                {
                    throw new HCSMSException("Transaction Errors!", ex);
                }
            }
        }
        public static void InsertDinningTable(string tableId, DinningTable dinning)
        {
            List<SqlCommand> commands = new List<SqlCommand>();
           
            SqlCommand comm = new SqlCommand(@"insert into DinningTable(Id, TableId, GuestAmount, ArrivedTime, Note)
                                                                             values(@Id, @TableId, @GuestAmount, @ArrivedTime, @Note)");


            comm.Parameters.Add("@Id", SqlDbType.Char, 10);
            comm.Parameters.Add("@TableId", SqlDbType.Char, 10);
            comm.Parameters.Add("@GuestAmount", SqlDbType.Int);
            comm.Parameters.Add("@ArrivedTime", SqlDbType.DateTime);
            comm.Parameters.Add("@Note", SqlDbType.Text);

            comm.Parameters["@Id"].Value = dinning.Id;
            comm.Parameters["@TableId"].Value = tableId;
            comm.Parameters["@GuestAmount"].Value = dinning.GuestAmount;
            comm.Parameters["@ArrivedTime"].Value = dinning.ArrivedTime;
            comm.Parameters["@Note"].Value = dinning.Note;       

            commands.Add(comm);
           
            try
            {
                using (SqlConnection conn = Utilities.GetConnection())
                {
                    Utilities.TransactionExecuteNonQuery(conn, commands);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number >= 50000)
                {
                    throw new HCSMSException(ex.Message);
                }
                else
                {
                    throw new HCSMSException("Transaction Errors !", ex);
                }
            }
        }

        private static DinningTable covertToDinningTable(string id, int guestAmount, DateTime arrivedTime, string note)
        {
            DinningTable table = new DinningTable();
            table.Id = id.Trim();
            table.GuestAmount = guestAmount;
            table.ArrivedTime = arrivedTime;
            table.Note = note.Trim();

            return table;
        }
        private static Table convertToTable(string id, string location, string name, int seatAmount, string status, string type)
        {
            Table table = new Table();

            table.Number = id;
            table.Location = location;
            table.Name = name;
            table.SeatAmount = seatAmount;
            table.Status =(TableStatus) Enum.Parse(typeof(TableStatus),status);
            table.Type = type;

            return table;
        }
        private static SqlCommand changeOneTable(string dinningId, string oldTableId, string newTableId)
        {
            SqlCommand comm = new SqlCommand("changeDinningTable");
             comm.CommandType = CommandType.StoredProcedure;

             comm.Parameters.Add("@dinningId", SqlDbType.Char, 10);
             comm.Parameters.Add("@oldTableId", SqlDbType.Char, 10);
             comm.Parameters.Add("@newTableId", SqlDbType.Char, 10);

             comm.Parameters["@dinningId"].Value = dinningId;
             comm.Parameters["@oldTableId"].Value = oldTableId;
             comm.Parameters["@newTableId"].Value = newTableId;

             return comm;
         }
        private static string searchTable(QueryCriteria cirteria)
        {
            string search = @"select [Table].Id, Location, [Table].Name, SeatAmount, Status , TableType.Name as Type
                                       from [Table], TableType
                                       where   and [Table].TableTypeId = TableType.Id";
            if (cirteria != null)
            {
                search = search + " and " + cirteria.Name + " " + cirteria.Value;
            }
            return search;
        }
    }
      
}
