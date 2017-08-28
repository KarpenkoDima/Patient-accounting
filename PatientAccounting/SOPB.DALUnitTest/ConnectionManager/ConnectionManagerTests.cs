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
        public ConnectionManagerTests()
        {
            DAL.ConnectionManager.ConnectionManager.SetConnection("Катя", "1");
        }
        [TestMethod()]
        public void GetConnectionSuccessTest()
        {
            DAL.ConnectionManager.ConnectionManager.SetConnection("Катя", "1");
            SqlConnection conn = DAL.ConnectionManager.ConnectionManager.Connection;
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
           DAL.ConnectionManager.ConnectionManager.SetConnection("Катя", "1");
            SqlConnection conn = DAL.ConnectionManager.ConnectionManager.Connection;
            conn.Open();
            Assert.IsTrue(conn.State == ConnectionState.Open);
        }
        [TestMethod()]
        public void OpenCloseConnectionAnyMoreTest()
        {
            DAL.ConnectionManager.ConnectionManager.SetConnection("Катя", "1");
            SqlConnection conn = DAL.ConnectionManager.ConnectionManager.Connection;

            int i = 10;
            while (--i > 0)
            {
                if(conn.State == ConnectionState.Closed)
                    conn.Open();
                Assert.IsTrue(conn.State == ConnectionState.Open);

                conn.Close();
                Assert.IsTrue(conn.State == ConnectionState.Closed);
            }
        }
        [TestMethod()]
        public void GetConnectionAndConnectionAnyMoreTest()
        {
            DAL.ConnectionManager.ConnectionManager.SetConnection("Катя", "1");
            SqlConnection conn = ConnectionManager.Connection;
            SqlConnection conn2 = ConnectionManager.Connection;

            int i = 10;
            while (--i > 0)
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                Assert.IsTrue(conn.State == ConnectionState.Open);

                if (conn2.State == ConnectionState.Closed)
                {
                    conn2.Open();
                }
                Assert.IsTrue(conn2.State == ConnectionState.Open);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Assert.IsTrue(conn.State == ConnectionState.Closed);
                if (conn2.State == ConnectionState.Open)
                {
                    conn2.Close();
                }
                Assert.IsTrue(conn2.State == ConnectionState.Closed);
            }
        }

        [TestMethod()]
        public void CreateManyManConnectionAndConnectionAnyMoreTest()
        {
            DAL.ConnectionManager.ConnectionManager.SetConnection("Катя", "1");
            SqlConnection conn = DAL.ConnectionManager.ConnectionManager.Connection;
          

            int i = 10000;
            while (--i > 0)
            {
                SqlConnection conn2 = ConnectionManager.Connection;
            }
        }
    }
}