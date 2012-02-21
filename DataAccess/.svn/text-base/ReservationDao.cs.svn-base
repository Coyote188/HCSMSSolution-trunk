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
    public static class ReservationDao
    {
       
        private static string newCustomerReservationId()
        {
            return null;
        }
        private static SqlCommand insertReservationTable(string tableId, string reserveInfoId)
        {
            SqlCommand comm = new SqlCommand(@"insert into ReservationTable(TableId, CustomerReservationId)
                                                                             values(@tableId, @info)");


          
            comm.Parameters.Add("@tableId", SqlDbType.Char, 10);
            comm.Parameters.Add("@info", SqlDbType.Char, 10);

        
            comm.Parameters["@tableId"].Value = tableId;
            comm.Parameters["@info"].Value = reserveInfoId;

            return comm;
        }
        private static PreorderTable covertToPreOrderTable(string id, DateTime arrivedTime, string customerName, decimal prepaidMoney, int guestAmount, string phone, string memberId)
        {
            PreorderTable table = new PreorderTable();
            table.Id = id.Trim();
            table.PeopleAmount = guestAmount;
            table.ArrivedTime = arrivedTime;
            table.MemberInfo.Id = memberId;
            table.PerpaidMoney = prepaidMoney;
            table.Phone = phone;
            table.CustomerName = customerName.Trim();

            return table;
        }
       

        public static void DeleteReservationTable(string customerReservationId)
        {
            List<SqlCommand> commands = new List<SqlCommand>();

            SqlCommand comm = new SqlCommand(@"delete from CustomerReservation
                                                                             where CustomerReservationId = @Id");


            comm.Parameters.Add("@id", SqlDbType.Char, 10);

            comm.Parameters["@id"].Value = customerReservationId;
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

        public static void InsertCustomerReservation(PreorderTable reserveInfo)
        {
            List<SqlCommand> commands = new List<SqlCommand>();

            SqlCommand comm = new SqlCommand(@"insert into CustomerReservation(Id, ArrivedTime, CustomerName, PrepaidMoney, GuestAmount, Phone, MemberId)
                                                                             values(@Id, @ArrivedTime, @CustomerName, @PrepaidMoney, @GuestAmount, @Phone, @MemberId)");


            comm.Parameters.Add("@Id", SqlDbType.Char, 10);
            comm.Parameters.Add("@ArrivedTime", SqlDbType.DateTime);
            comm.Parameters.Add("@CustomerName", SqlDbType.VarChar,255);
            comm.Parameters.Add("@PrepaidMoney", SqlDbType.Decimal);
            comm.Parameters.Add("@GuestAmount", SqlDbType.Int);
            comm.Parameters.Add("@Phone", SqlDbType.VarChar,255);
            comm.Parameters.Add("@MemberId", SqlDbType.Char, 10);
                   
            comm.Parameters["@Id"].Value = reserveInfo.Id;
            comm.Parameters["@ArrivedTime"].Value = reserveInfo.ArrivedTime;
            comm.Parameters["@CustomerName"].Value = reserveInfo.CustomerName;
            comm.Parameters["@PrepaidMoney"].Value = reserveInfo.PerpaidMoney;
            comm.Parameters["@GuestAmount"].Value = reserveInfo.PeopleAmount;
            comm.Parameters["@Phone"].Value = reserveInfo.Phone;
            comm.Parameters["@MemberId"].Value = reserveInfo.MemberInfo.Id;

            commands.Add(comm);
            foreach (Table table in reserveInfo.ReserveTables)
            {
                commands.Add(insertReservationTable(table.Number, reserveInfo.Id));
            }

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

        public static void UpdateCustomerReservaition(PreorderTable reserveInfo)
        {
            List<SqlCommand> commands = new List<SqlCommand>();

            SqlCommand comm = new SqlCommand(@"update CustomerReservation
                                                                                set ArrivedTime=@ArrivedTime
                                                                                set CustomerName=@CustomerName
                                                                                set PrepaidMoney=@PrepaidMoney
                                                                                set GuestAmount=@GuestAmount
                                                                                set Phone=@Phone
                                                                                set MemberId=@MemberId
                                                                             where Id=@Id");


            comm.Parameters.Add("@Id", SqlDbType.Char, 10);
            comm.Parameters.Add("@ArrivedTime", SqlDbType.DateTime);
            comm.Parameters.Add("@CustomerName", SqlDbType.VarChar, 255);
            comm.Parameters.Add("@PrepaidMoney", SqlDbType.Decimal);
            comm.Parameters.Add("@GuestAmount", SqlDbType.Int);
            comm.Parameters.Add("@Phone", SqlDbType.VarChar, 255);
            comm.Parameters.Add("@MemberId", SqlDbType.Char, 10);

    

            comm.Parameters["@Id"].Value = reserveInfo.Id;
            comm.Parameters["@ArrivedTime"].Value = reserveInfo.ArrivedTime;
            comm.Parameters["@CustomerName"].Value = reserveInfo.CustomerName;
            comm.Parameters["@PrepaidMoney"].Value = reserveInfo.PerpaidMoney;
            comm.Parameters["@GuestAmount"].Value = reserveInfo.PeopleAmount;
            comm.Parameters["@Phone"].Value = reserveInfo.Phone;
            comm.Parameters["@MemberId"].Value = reserveInfo.MemberInfo.Id;

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

        public static List<PreorderTable> GetReservation()
        {
            List<PreorderTable> tableList = new List<PreorderTable>();
            using (SqlConnection conn = Utilities.GetConnection())
            {
                 SqlCommand comm = new SqlCommand(@"select Id, ArrivedTime, CustomerName, PrepaidMoney, GuestAmount, Phone, MemberId 
                                                                                  from CustomerReservation
                                                                                  where ArrivedTime>=GETDATE()");         

                try
                {
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        PreorderTable tmp = new PreorderTable();
                        int i=0;
                        tmp = covertToPreOrderTable(reader[i++].ToString(), reader.GetDateTime(i++), reader[i++].ToString(), reader.GetDecimal(i++), reader.GetInt32(i++), reader[i++].ToString(), reader[i++].ToString());
                        List<Table> tables= TableDao.GetTableInReserve(reader["Id"].ToString());
                        tmp.TableAmount = tables.Count;
                        tmp.ReserveTables = tables;
                        tmp.MemberInfo.Id = reader["MemberId"].ToString( );
                        tableList.Add(tmp);
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
    }
}
