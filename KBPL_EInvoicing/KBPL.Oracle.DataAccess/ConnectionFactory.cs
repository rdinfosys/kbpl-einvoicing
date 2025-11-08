using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using KBPL.Models.HelperModels;

namespace KBPL.Oracle.DataAccess
{
    public interface IConnectionFactory : IDisposable
    {
        IDbConnection Connection { get; }
    }
    public class ConnectionFactory : IConnectionFactory
    {
        private DbConnection con;
        public IDbConnection Connection
        {
            get
            {
                return CreateConnection();
            }
        }
        private string _connectionString;
        private readonly DBConnectionSettings _dBConnectionSettings;
        public ConnectionFactory()
        {
            _connectionString = "Data Source=192.168.2.114/orcl;User ID=invent;Password=invent;Incr Pool Size=5;Decr Pool Size=2;Max Pool Size=100;Min Pool Size=10;"; //_dBConnectionSettings.DBConnectionstring;
            //_connectionString = "Data Source=192.168.1.201/KBPLtest;User ID=invent;Password=invent;Incr Pool Size=5;Decr Pool Size=2;"; //_dBConnectionSettings.DBConnectionstring;


        }
        //public ConnectionFactory(IOptions<DBConnectionSettings> dBConnectionSettings)
        //{
        //    _dBConnectionSettings = dBConnectionSettings.Value;

        //    _connectionString = "Data Source=192.168.1.202/KBPL;User ID=invent;Password=invent;Incr Pool Size=5;Decr Pool Size=2;"; //_dBConnectionSettings.DBConnectionstring;
        //}

        /// <summary>
        /// Create connection string as per datapase
        /// </summary>
        /// <param name="enumConnections"></param>
        /// <param name="server"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="hcoDbName"></param>
        /// <param name="connectionTimeout"></param>
        public ConnectionFactory(string server, string userName, string password, string hcoDbName, string connectionTimeout)
        {
            string connectionString, defaultConnectionString = "";
            if (string.IsNullOrWhiteSpace(defaultConnectionString))
            {
                defaultConnectionString = "Server={0};Database={1};Uid={2};Pwd={3};MultipleActiveResultSets=true;Connection Timeout={4};Min Pool Size=100;Max Pool Size={4};";
                //defaultConnectionString = "Server={0};Database={1};Uid=EVO;Pwd=05INFO;";
            }
            //server = "SILICUSR322\\MSSQLSERVER2017"; //db connection on 322 server
            //server = "192.168.3.200"; 

            //write code to prepare connection string according to the enum value. Need to check how it has been done in Evolution win app.

            connectionString = string.Format(defaultConnectionString, server, "EVO_EHR", userName, password, connectionTimeout);

            _connectionString = "Data Source=192.168.1.202/KBPL;User ID=invent;Password=invent";
        }

        private IDbConnection CreateConnection()
        {
            if (con == null)
            {
                //con = new SqlConnection(_connectionString);
                var factory = DbProviderFactories.GetFactory(new OracleConnection(_connectionString)); // Create connection object according to the type of provider in the connection string. This makes it flexible to connect to any Db SQl,MYSQL, Oracle etc.
                con = factory.CreateConnection();
                // con.ConnectionString = _connectionString;
            }
            if (string.IsNullOrWhiteSpace(con.ConnectionString))
            {
                con.ConnectionString = _connectionString;
            }
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            return con;
        }

        public void Dispose()
        {
            if (con.State == ConnectionState.Open)
                con.Close();

            con.Dispose();
            con = null;
        }
    }
}
