using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOPB.Accounting.DAL.ConnectionManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOPB.Accounting.DAL.ConnectionManager.Tests
{
    [TestClass()]
    public class ConnectionManagerTests
    {
        [TestMethod()]
        public void GetConnectionSuccessTest()
        {
            SqlConnection conn = ConnectionManager.GetConnection("Катя", "1");
            Assert.AreNotEqual(conn.ConnectionString, string.Empty);
        }

        [TestMethod()]
        public void TestConnectionTest()
        {
            Assert.IsTrue(ConnectionManager.TestConnection("Катя", "1"));
        }
        [TestMethod()]
        public void OpenConnectionTest()
        {
            SqlConnection conn = ConnectionManager.GetConnection("Катя", "1");
            conn.Open();
            Assert.IsTrue(conn.State == ConnectionState.Open);
        }
        [TestMethod()]
        public void OpenCloseConnectionAnyMoreTest()
        {
            SqlConnection conn = ConnectionManager.GetConnection("Катя", "1");
            
            int i = 10;
            while (--i > 0)
            {
                conn.Open();
                Assert.IsTrue(conn.State == ConnectionState.Open);

                conn.Close();
                Assert.IsTrue(conn.State == ConnectionState.Closed);
            }
        }
    }
}