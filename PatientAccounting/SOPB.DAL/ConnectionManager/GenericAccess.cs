using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace SOPB.Accounting.DAL.ConnectionManager
{
    static class GenericAccess
    {
        public static DbCommand CreateCommand()
        {
            var command = ConnectionManager.SqlCommand;
            command.CommandType = CommandType.StoredProcedure;
            return command;
        } 
    }
}
