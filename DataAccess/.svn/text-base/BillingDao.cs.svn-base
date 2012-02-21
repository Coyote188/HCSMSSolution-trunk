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
    public static class BillingDao
    {
        private static Bill convertToBill(string Id, string dinningId, DateTime billTime,string note, decimal totalMoney, BillingInfo billInfo)
        {
            Bill bill = new Bill();

            bill.BillingInfo.Add(billInfo);
            bill.BillingTable.Id = dinningId;
            bill.BillTime = billTime;
            bill.Id = Id;
            bill.Note = note;
            bill.TotalMoney = totalMoney;

            return bill;
        }

        public static Bill GetBill(string dinningId)
        {
            using (SqlConnection conn = Utilities.GetConnection())
            {
                SqlCommand comm = new SqlCommand(@"select Bill.Id,DinningTable.TableId, TotalMoney, BillTime, Note, Name 
                                                                                 from DinningTable, Bill, BillType
                                                                                 where DinningTable.Id = @dinningId
                                                                                            and Bill.TypeId = BillType.Id", conn);
                comm.Parameters.Add("@dinningId", SqlDbType.Char, 10);
                comm.Parameters["@dinningId"].Value = dinningId;
                try
                {
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();

                    if (reader.Read())
                    {
                        List<string> cardId = new List<string>();
                        cardId.Add(reader["TableId"].ToString());
                        BillingInfo info = new BillingInfo();
                        return convertToBill(reader["Id"].ToString(), dinningId, reader.GetDateTime(3),reader["Note"].ToString(),reader.GetDecimal(2), info);
                    }
                    else
                    {
                        throw new HCSMSException("No Type Found");
                    }
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
        public static void PayBill(Bill aBill)
        {
            List<SqlCommand> commands = new List<SqlCommand>();

            SqlCommand comm = new SqlCommand(@"insert into Bill(Id, TotalMoney, BillTime, Note, Type)
                                                                             values(@Id, @TotalAmount, @BillTime, @Note, @Type)");

            comm.Parameters.Add("@Id", SqlDbType.Char, 10);
            comm.Parameters.Add("@TotalMoney", SqlDbType.Decimal);
            comm.Parameters.Add("@BillTime", SqlDbType.DateTime);
            comm.Parameters.Add("@Note", SqlDbType.Text);
            comm.Parameters.Add("@Type", SqlDbType.Char, 10);
            

            comm.Parameters["@Id"].Value = aBill.Id;
            comm.Parameters["@TotalMoney"].Value = aBill.TotalMoney;
            comm.Parameters["@BillTime"].Value = aBill.BillTime;
            comm.Parameters["@Note"].Value = aBill.Note;
            comm.Parameters["@Type"].Value = aBill.Type;

            commands.Add(comm);
            foreach (BillingInfo info in aBill.BillingInfo)
            {
                comm = new SqlCommand(@"insert into BillUsingCard(BillingId, CardId, Money)
                                                                             values(@BillingId, @CardId, @Money))");

                comm.Parameters.Add("@BillingId", SqlDbType.Char, 10);
                comm.Parameters.Add("@CardId", SqlDbType.Char, 10);
                comm.Parameters.Add("@Money", SqlDbType.Decimal);

                comm.Parameters["@BillingId"].Value = aBill.Id;
                comm.Parameters["@CardId"].Value = info.Card.Id;
                comm.Parameters["@Money"].Value = info.Money;
                commands.Add(comm);
            }
            comm = new SqlCommand(@"Update DinningTable
                                                            set BillId=@BillngId
                                                         where Id=@DinningId");

            comm.Parameters.Add("@BillingId", SqlDbType.Char, 10);
            comm.Parameters.Add("@DinningId", SqlDbType.Char, 10);

            comm.Parameters["@BillingId"].Value = aBill.Id;
            comm.Parameters["@DinningId"].Value = aBill.BillingTable.Id;
            commands.Add(comm);

            foreach (BillingInfo info in aBill.BillingInfo)
            {
                comm = new SqlCommand(@"update MemberCard
                                                                                set CurrentMoney=CurrentMoney-@spent
                                                                                ,    MoneyCosumed=MoneyCosumed+@spent
                                                                             where Id=@cardId ");
                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.Add("@spent", SqlDbType.Char, 10);
                comm.Parameters.Add("@cardId", SqlDbType.Char, 10);

                comm.Parameters["@spent"].Value = info.Money;
                comm.Parameters["@cardId"].Value = info.Card.Id;

                commands.Add(comm);
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
                    throw new HCSMSException("Transaction Errors !", ex);
                }
            }
        }
    }
}
