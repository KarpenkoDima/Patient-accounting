using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SOPB.Accounting.DAL.ConnectionManager
{
    /// <summary>
    /// What are You?
    /// </summary>
    public static class RoleForUser
    {
        public static string GetRoleForUser(string login, string password)
        {
            string role = String.Empty;
            try
            {
                SqlConnection connection = ConnectionManager.GetConnection(login, password);
                

                using (SqlCommand command = ConnectionManager.SqlCommand)
                {
                    command.CommandText = "uspGetRoleForUser";
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@role",
                        Size = 50,
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(parameter);

                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    role = ((string) command.Parameters["@role"].Value).Trim();
                }
            }
            catch (SqlException)
            {

                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return role;
        }
    }
}
