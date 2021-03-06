﻿using FileTransfer.DbHelper.Entitys;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace FileTransfer.LogToDb
{
    public class ReceiveLogger : LogToSQLiteDb<ReceiveLogEntity>
    {
        #region 方法
        protected override void BatchInsertRows(System.Data.Common.DbConnection conn, IEnumerable<ReceiveLogEntity> rows)
        {
            DbCommand command = conn.CreateCommand();
            command.CommandText = @"INSERT INTO ReceiveLog(ReceiveDate,ReceiveFile,MonitorIP,MonitorAlias,ReceiveState) VALUES(@ReceiveDate,@ReceiveFile,@MonitorIP,@MonitorAlias,@ReceiveState)";
            DbParameter dateParam = new SQLiteParameter("@ReceiveDate", DbType.DateTime);
            DbParameter fileParam = new SQLiteParameter("@ReceiveFile", DbType.String);
            DbParameter ipParam = new SQLiteParameter("@MonitorIP", DbType.String, 30);
            DbParameter dictParam = new SQLiteParameter("@MonitorAlias", DbType.String);
            DbParameter stateParam = new SQLiteParameter("@ReceiveState", DbType.String, 10);
            command.Parameters.Add(dateParam);
            command.Parameters.Add(fileParam);
            command.Parameters.Add(ipParam);
            command.Parameters.Add(dictParam);
            command.Parameters.Add(stateParam);
            foreach (var r in rows)
            {
                dateParam.Value = r.ReceiveDate;
                fileParam.Value = r.ReceiveFile;
                ipParam.Value = r.MonitorIP;
                dictParam.Value = r.MonitorAlias;
                stateParam.Value = r.ReceiveState;
                command.ExecuteNonQuery();
            }
        }
        #endregion
    }
}
