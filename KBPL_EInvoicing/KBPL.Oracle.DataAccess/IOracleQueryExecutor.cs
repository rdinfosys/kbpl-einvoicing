using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace KBPL.Oracle.DataAccess
{
    public interface IOracleQueryExecutor
    {
        OracleTransaction Transaction { get; }
        OracleTransaction BeginTransaction();
        void Commit();
        OracleCommand CreateCommand(string qry, CommandType type, params object[] args);
        void Dispose();
        // Task<SqlDataReader> ExecDataReaderAsync(string qry, params object[] args);
        int ExecNonQuery(string qry, params object[] args);
        object ExecScalar(string qry, params object[] args);
        void Rollback();
    }
}
