﻿using FileTransfer.Utils;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransfer.LogToDb
{
    public class LogToSQLiteDb<T> : ProducerConsumerLite<T>
    {
        #region 变量
        private static ILog _logger = LogManager.GetLogger(typeof(LogToSQLiteDb<T>));
        private string _connectStr = string.Empty;
        #endregion

        #region 构造函数
        public LogToSQLiteDb()
        {
            _bufferSize = 100;
            string exePath = System.Environment.CurrentDirectory;
            string dbPath = System.IO.Path.Combine(exePath, "Database", "log.db");
            _connectStr = string.Format("Data Source = {0};PRAGMA journal_mode = WAL", dbPath);
        }
        #endregion

        #region 方法
        protected override void Consume(IEnumerable<T> items)
        {
            InsertRowsToDb(items);
        }

        private void InsertRowsToDb(IEnumerable<T> rows)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_connectStr))
            {
                try
                {
                    conn.Open();
                    DbTransaction trans = conn.BeginTransaction();
                    try
                    {
                        BatchInsertRows(conn, rows);
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        _logger.Error(string.Format("日志批插入至数据库时发生异常{0}，已回滚。", e.Message));
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(string.Format("日志插入时打开数据库的过程中发生异常{0}", ex.Message));
                }
            }
        }

        protected virtual void BatchInsertRows(DbConnection conn, IEnumerable<T> rows)
        { }

        #endregion

    }
}
