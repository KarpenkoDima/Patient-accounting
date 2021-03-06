﻿using System;
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
                ConnectionManager.SetConnection("Катя", "1");
               SqlConnection connection = ConnectionManager.Connection;
                

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "uspGetRoleForUser";
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@Role",
                        Size = 50,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(parameter);

                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    role = ((string) command.Parameters["@Role"].Value).Trim();
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
