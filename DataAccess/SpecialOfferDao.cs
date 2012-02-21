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
    public static class SpecialOfferDao
    {

        public static void InsertSpecialSale(string itemId, List<SpecialOffer> offerList)
        {
            List<SqlCommand> commands = new List<SqlCommand>();

            foreach (SpecialOffer offer in offerList)
            {
                SqlCommand comm = new SqlCommand(@"insert into SpecialSale(SpecialOfferId,SaleItemId,StartDate,FinishDate)
                                                                                 values(@SpecialOfferId, @SaleItemId, @StartDate, @FinishDate)");


                comm.Parameters.Add("@SpecialOfferId", SqlDbType.Char, 10);
                comm.Parameters.Add("@SaleItemId", SqlDbType.Char, 10);
                comm.Parameters.Add("@StartDate", SqlDbType.DateTime);
                comm.Parameters.Add("@FinishDate", SqlDbType.DateTime);

                comm.Parameters["@SpecialOfferId"].Value = offer.Id;
                comm.Parameters["@SaleItemId"].Value = itemId;
                comm.Parameters["@StartDate"].Value = offer.StartDate;
                comm.Parameters["@FinishDate"].Value = offer.FinishDate;


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
                    throw new HCSMSException("Transaction Errors!", ex);
                }
            }
        }
    }
}
