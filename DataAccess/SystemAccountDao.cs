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
    public static class SystemAccountDao
    {
        public static bool IsExist(string accountName)
        {
            using (SqlConnection conn = Utilities.GetConnection())
            {
                SqlCommand comm = new SqlCommand(@"select UserName from ApplicationUser where AccountName = @accountName", conn);
                comm.Parameters.Add("@accountName", SqlDbType.VarChar, 255);
                comm.Parameters["@accountName"].Value = accountName;
                try
                {
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();

                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else return false;
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


        public static SystemAccount Login(Account anAccount)
        {           
           List<string> roles = new List<string>();
           using (SqlConnection conn = Utilities.GetConnection())
           {
               SqlCommand comm = new SqlCommand(@"select UserName, Status
                                                                                from ApplicationUser 
                                                                                where AccountName = @accountName and Password=@pwd", conn);
               comm.Parameters.Add("@accountName", SqlDbType.VarChar, 255);
               comm.Parameters.Add("@pwd", SqlDbType.VarChar, 255);
               comm.Parameters["@accountName"].Value = anAccount.Name;
               comm.Parameters["@pwd"].Value = anAccount.Password;
               try
               {
                   conn.Open();

                   SqlDataReader reader = comm.ExecuteReader();

                   if (reader.Read())
                   {
                       SystemAccount account = new SystemAccount();
                       account.Status =(AccountStatus)Enum.Parse(typeof(AccountStatus),reader["Status"].ToString());
                       account.UserId = anAccount.Name;
                       account.UserName = reader["UserName"].ToString();
                       account.UserRole = GetRoles(account);

                       return account;
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

        public static List<string> GetRoles(SystemAccount anAccount)
        {
            List<string> roles = new List<string>();
            using (SqlConnection conn = Utilities.GetConnection())
            {
                SqlCommand comm = new SqlCommand(@"select Name from ApplicationRole, UserRole where UserRole.UserName = @accountName and UserRole.RoleId=ApplicationRole.Id", conn);
                comm.Parameters.Add("@accountName", SqlDbType.VarChar, 255);
                comm.Parameters["@accountName"].Value = anAccount.UserId;
                try
                {
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                       roles.Add(reader["Name"].ToString());
                    }
                    return roles;
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
