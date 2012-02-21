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
    public static class ItemDao
    {
        private static SqlCommand changeOneItem(string dinningId, string oldItemId, string newItemId)
        {
             SqlCommand comm = new SqlCommand("changeItemOrderList");
             comm.CommandType = CommandType.StoredProcedure;

             comm.Parameters.Add("@dinningId", SqlDbType.Char, 10);
             comm.Parameters.Add("@oldItemId", SqlDbType.Char, 10);
             comm.Parameters.Add("@newItemId", SqlDbType.Char, 10);

             comm.Parameters["@dinningId"].Value = dinningId;
             comm.Parameters["@oldItemId"].Value = oldItemId;
             comm.Parameters["@newItemId"].Value = newItemId;

             return comm;
         }
        private static SqlCommand deorderOneItem(string dinningId, string itemId)
        {
            SqlCommand comm = new SqlCommand(@"delete from ItemOrderList
                                                                             where DinningTableId =@dinningId and ItemId=@itemId" );
           

            comm.Parameters.Add("@dinningId", SqlDbType.Char, 10);
            comm.Parameters.Add("@itemId", SqlDbType.Char, 10);

            comm.Parameters["@dinningId"].Value = dinningId;
            comm.Parameters["@itemId"].Value = itemId;

            return comm;
        }
        private static SqlCommand insertOneItem(string id, string measurement, string name
                                                                      , decimal pricePerUnit, string description, string status,decimal amount, string typeId)
        {
            SqlCommand comm = new SqlCommand(@"insert into Item(Id, Measurement, Name, PricePerUnit, Description, Status, Amount, ItemTypeId)
                                                                             values(@Id, @Measurement, @Name, @PricePerUnit, @Description, @Status, @Amount, @ItemTypeId)");


            comm.Parameters.Add("@Id", SqlDbType.Char, 10);
            comm.Parameters.Add("@Measurement", SqlDbType.VarChar, 255);
            comm.Parameters.Add("@Name", SqlDbType.VarChar, 255);
            comm.Parameters.Add("@PricePerUnit", SqlDbType.Decimal);
            comm.Parameters.Add("@Description", SqlDbType.Text);
            comm.Parameters.Add("@Status", SqlDbType.VarChar, 255);
            comm.Parameters.Add("@ItemTypeId", SqlDbType.Char, 10);
            comm.Parameters.Add("@Amount", SqlDbType.Decimal);

            comm.Parameters["@Id"].Value = id;
            comm.Parameters["@Measurement"].Value = measurement;
            comm.Parameters["@Name"].Value = name;
            comm.Parameters["@PricePerUnit"].Value = pricePerUnit;
            comm.Parameters["@Description"].Value = description;
            comm.Parameters["@Status"].Value = status;
            comm.Parameters["@ItemTypeId"].Value = typeId;
            comm.Parameters["@Amount"].Value = amount;

            return comm;
        }
        private static SqlCommand updateOneItem(string id, string measurement, string name
                                                                     , decimal pricePerUnit, string description, string status, decimal amount, string typeId)
        {
            SqlCommand comm = new SqlCommand(@"update Item
                                                                                set Measurement=@Measurement
                                                                                    , Name=@Name
                                                                                    , PricePerUnit=@PricePerUnit
                                                                                    , Description=@Description
                                                                                    , Status=@Status
                                                                                    , Amount=@Amount
                                                                                    , ItemTypeId=@ItemTypeId)
                                                                             where Id=@Id");


            comm.Parameters.Add("@Id", SqlDbType.Char, 10);
            comm.Parameters.Add("@Measurement", SqlDbType.VarChar, 255);
            comm.Parameters.Add("@Name", SqlDbType.VarChar, 255);
            comm.Parameters.Add("@PricePerUnit", SqlDbType.Decimal);
            comm.Parameters.Add("@Description", SqlDbType.Text);
            comm.Parameters.Add("@Status", SqlDbType.VarChar, 255);
            comm.Parameters.Add("@ItemTypeId", SqlDbType.Char, 10);
            comm.Parameters.Add("@Amount", SqlDbType.Decimal);

            comm.Parameters["@Id"].Value = id;
            comm.Parameters["@Measurement"].Value = measurement;
            comm.Parameters["@Name"].Value = name;
            comm.Parameters["@PricePerUnit"].Value = pricePerUnit;
            comm.Parameters["@Description"].Value = description;
            comm.Parameters["@Status"].Value = status;
            comm.Parameters["@ItemTypeId"].Value = typeId;
            comm.Parameters["@Amount"].Value = amount;

            return comm;
        }
        private static SqlCommand insertOneSaleItem(string itemId, decimal salePricePerUnit, bool isRecommend)
        {
            SqlCommand comm = new SqlCommand(@"insert into SaleItem(SalePricePerUnit, IsRecommend, ItemId)
                                                                             values(@SalePricePerUnit, @IsRecommend, @ItemId)");


            comm.Parameters.Add("@ItemId", SqlDbType.Char, 10);
            comm.Parameters.Add("@SalePricePerUnit", SqlDbType.Decimal);
            comm.Parameters.Add("@IsRecommend", SqlDbType.Bit);

            comm.Parameters["@ItemId"].Value = itemId;
            comm.Parameters["@SalePricePerUnit"].Value = salePricePerUnit;
            comm.Parameters["@IsRecommend"].Value = isRecommend;

            return comm;
        }
        private static SqlCommand updateOneSaleItem(string itemId, decimal salePricePerUnit, bool isRecommend)
        {
            SqlCommand comm = new SqlCommand(@"update SaleItem
                                                                                set SalePricePerUnit=@SalePricePerUnit
                                                                                     , IsRecommend=@IsRecommend 
                                                                             where ItemId=@ItemId)");


            comm.Parameters.Add("@ItemId", SqlDbType.Char, 10);
            comm.Parameters.Add("@SalePricePerUnit", SqlDbType.Decimal);
            comm.Parameters.Add("@IsRecommend", SqlDbType.Bit);

            comm.Parameters["@ItemId"].Value = itemId;
            comm.Parameters["@SalePricePerUnit"].Value = salePricePerUnit;
            comm.Parameters["@IsRecommend"].Value = isRecommend;

            return comm;
        }
        private static SqlCommand insertOneItemOrder(string itemId, string dinningId, string note, decimal amount)
        {
            SqlCommand comm = new SqlCommand(@"insert into ItemOrderList(ItemId, DinningTableId, Amount, Note)
                                                                             values(@ItemId, @DinningTableId, @Amount, @Note)");


            comm.Parameters.Add("@ItemId", SqlDbType.Char, 10);
            comm.Parameters.Add("@DinningTableId", SqlDbType.Char,10);
            comm.Parameters.Add("@Amount", SqlDbType.Decimal);
            comm.Parameters.Add("@Note", SqlDbType.Text);

            comm.Parameters["@ItemId"].Value = itemId;
            comm.Parameters["@DinningTableId"].Value = dinningId;
            comm.Parameters["@Amount"].Value = note;
            comm.Parameters["@Note"].Value = amount;

            return comm;
        }
        private static SaleItem convertToSaleItem(string id, string measurement, string name, decimal pricePerUnit, string description
                                                                        , string status, decimal amount, string type, decimal salePricePerUnit, bool isRecommend)
        {
            SaleItem item = new SaleItem();
            item.Id = id;
            item.Amount = amount;
            item.Description = description;
            item.IsRecommended = isRecommend;
            item.Measurement = measurement;
            item.Name = name;
            item.PricePerUnit = pricePerUnit;
            item.SalePricePerUnit = salePricePerUnit;
            item.Status = (ItemStatus)Enum.Parse(typeof(ItemStatus), status);
            item.Type = (ItemType)Enum.Parse(typeof(ItemType), type);
            
            return item;

        }
        private static string searchSaleItem(QueryCriteria cirteria)
        {
            string search = @"select Id, Measurement, Item.Name, PricePerUnit, Description, Status, Amount
                                                , ItemType.Name as Type, SalePricePerUnit, IsRecommend 
                                        from Item,SaleItem,ItemType
                                        where Item.Id = SaleItem.ItemId and Item.ItemTypeId = ItemType.ID";
            if (cirteria != null)
            {
                search = search + " and " + cirteria.Name + " " + cirteria.Value;
            }
            return search;
        }
                                                                        

        public static void ChangeItem(string dinningId, Dictionary<string, string> itemIdPair)
        {
            List<SqlCommand> commands = new List<SqlCommand>();

            foreach (KeyValuePair<string, string> pair in itemIdPair)
            {
                commands.Add(changeOneItem(dinningId,pair.Key,pair.Value));
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
                if (ex.Number >=  50000)
                {
                    throw new HCSMSException(ex.Message);
                }
                else
                {
                    throw new HCSMSException("Transaction Errors !",ex);
                }
            }
        }
        public static void DeorderItem(string dinningId, List<string> itemIdList)
        {
            List<SqlCommand> commands = new List<SqlCommand>();

            foreach (string id in itemIdList)
            {
                commands.Add(deorderOneItem(dinningId,id));
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
                if (ex.Number >=  50000)
                {
                    throw new HCSMSException(ex.Message);
                }
                else
                {
                    throw new HCSMSException("Transaction Errors !",ex);
                }
            }
        }
        public static void InsertSaleItem(List<SaleItem> itemList)
        {
            List<SqlCommand> commands = new List<SqlCommand>();

            foreach (SaleItem saleItem in itemList)
            {
                commands.Add(insertOneItem(saleItem.Id, saleItem.Measurement, saleItem.Name, saleItem.PricePerUnit, saleItem.Description, saleItem.Status.ToString(), saleItem.Amount, GetItemTypeId(saleItem.Type.ToString())));
                commands.Add(insertOneSaleItem(saleItem.Id, saleItem.SalePricePerUnit, saleItem.IsRecommended));
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
        public static void InsertItemOrder(string dinningId, List<Item> itemList)
        {
            List<SqlCommand> commands = new List<SqlCommand>();

            foreach (Item saleItem in itemList)
            {
                commands.Add(insertOneItemOrder(saleItem.Id, dinningId,saleItem.Description, saleItem.Amount));                
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
        public static void StopSupplyItem(List<Item> itemList)
        {
            List<SqlCommand> commands = new List<SqlCommand>();
            foreach(Item item in itemList)
            {
            SqlCommand comm = new SqlCommand(@"update Item
                                                                                set Status = 'Deleted'
                                                                             where Id = @Id");


            comm.Parameters.Add("@Id", SqlDbType.Char, 10);

            comm.Parameters["@Id"].Value = item.Id;

           
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
        public static void UpdateSaleItem(List<SaleItem> itemList)
        {
            List<SqlCommand> commands = new List<SqlCommand>();

            foreach (SaleItem saleItem in itemList)
            {
                commands.Add(updateOneSaleItem(saleItem.Id, saleItem.SalePricePerUnit, saleItem.IsRecommended));
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
        public static void UpdateItem(List<Item> itemList)
        {
            List<SqlCommand> commands = new List<SqlCommand>();

            foreach (Item saleItem in itemList)
            {
                commands.Add(updateOneItem(saleItem.Id, saleItem.Measurement, saleItem.Name, saleItem.PricePerUnit, saleItem.Description, saleItem.Status.ToString(), saleItem.Amount, GetItemTypeId(saleItem.Type.ToString())));                
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
        public static List<SaleItem> GetSaleItemByCondition(QueryCriteria criteria)
        {
            List<SaleItem> itemList = new List<SaleItem>();
            using (SqlConnection conn = Utilities.GetConnection())
            {
                SqlCommand comm = new SqlCommand(searchSaleItem(criteria), conn);
              
                try
                {
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();

                   while(reader.Read())
                    {
                       int i=0;
                        itemList.Add(convertToSaleItem(reader[i++].ToString(),reader[i++].ToString(),reader[i++].ToString(),reader.GetDecimal(i++)
                                                                        ,reader[i++].ToString(),reader[i++].ToString(),reader.GetDecimal(i++),reader[i++].ToString()
                                                                        ,reader.GetDecimal(i++),reader.GetBoolean(i++)));
                    }
                   return itemList;
                    
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
        public static List<SaleItem> GetRecommendSaleItem()
        {
            QueryCriteria criteria = new QueryCriteria();
            criteria.Name = "IsRecommend";
            criteria.Value = "=True";
            return GetSaleItemByCondition(criteria);
        }
        public static ItemType GetItemType(string typeId)
        {
            using (SqlConnection conn = Utilities.GetConnection())
            {
                SqlCommand comm = new SqlCommand(@"select Name from ItemType where Id = @typeId", conn);
                comm.Parameters.Add("@typeId", SqlDbType.Char, 10);
                comm.Parameters["@typeId"].Value = typeId;
                try
                {
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();

                    if (reader.Read())
                    {
                        return ((ItemType)Enum.Parse(typeof(ItemType), reader["Name"].ToString()));
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
        public static List<ItemType> GetAllItemType()
        {
            List<ItemType> itemTypes = new List<ItemType>();
            using (SqlConnection conn = Utilities.GetConnection())
            {
                SqlCommand comm = new SqlCommand(@"select Name from ItemType", conn);
                try
                {
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        itemTypes.Add( (ItemType)Enum.Parse(typeof(ItemType),reader["Name"].ToString()));
                    }
                    return itemTypes;
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
        public static string GetItemTypeId(string name)
        {
            using (SqlConnection conn = Utilities.GetConnection())
            {
                SqlCommand comm = new SqlCommand(@"select Id from ItemType where name = @typeName", conn);
                comm.Parameters.Add("@typeName", SqlDbType.VarChar, 255);
                comm.Parameters["@typeName"].Value = name;
                try
                {
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();

                    if (reader.Read())
                    {
                        return reader["Id"].ToString();
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


       
    }
}