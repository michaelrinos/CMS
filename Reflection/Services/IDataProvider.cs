using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Reflection.Services
{
    public interface IDataProvider
    {
        string ConnectionString { get; }
        bool IsTransactionOpen { get; }
        int Timeout { get; set; }

        DbTransaction BeginTransaction(IsolationLevel isolation = IsolationLevel.ReadCommitted);
        void CommitTransaction();
        int ExecuteBatchUpdate(DataTable dataTable, int batchSize, DbCommand insertStatement, DbCommand updateStatement, DbCommand deleteStatement);

    }
}
