using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;


namespace SafetyCorner.Models.Repository
{
    public class DataAccess : IDisposable
    {
        protected SQLiteConnection conn;
        protected SQLiteCommand cmd;
        protected SQLiteDataAdapter da;

        // Track whether Dispose has been called.
        private bool disposed = false;

        public DataAccess()
        {
            conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SafetyCornerConnection"].ConnectionString.Replace("{0}",System.Web.HttpContext.Current.Server.MapPath("~")+"db\\"));
            cmd = new SQLiteCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            da = new SQLiteDataAdapter(cmd);
        }
        public DataSet ExecuteDataSet()
        {
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }


        /// <summary>
        /// 執行T-SQL 查詢，並將查詢結果以DataSet回傳
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string sqlCommand)
        {
            if (string.IsNullOrWhiteSpace(sqlCommand)) throw new ArgumentException("參數為空值", "sqlCommand");

            DataSet ds = new DataSet();
            cmd.Parameters.Clear();

            cmd.CommandText = sqlCommand;
            cmd.CommandType = CommandType.Text;

            da.Fill(ds);

            return ds;
        }

        /// <summary>
        /// 執行T-SQL 查詢, 並將查詢結果以DataSet回傳
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="p">查詢參數</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string sqlCommand, SQLiteParameter[] p)
        {
            if (string.IsNullOrWhiteSpace(sqlCommand)) throw new ArgumentException("參數為空值", "sqlCommand");
            DataSet ds = new DataSet();
            cmd.Parameters.Clear();
            foreach (var sqlParameter in p)
            {
                cmd.Parameters.Add(sqlParameter);
            }

            cmd.CommandText = sqlCommand;
            cmd.CommandType = CommandType.Text;

            da.Fill(ds);

            return ds;

        }

        ~DataAccess()
        {
            Dispose(false);
        }

        public void Dispose()
        {


            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    //_db.Dispose();

                    if (conn.State != ConnectionState.Closed) conn.Close();
                    da.Dispose();
                    cmd.Dispose();
                    conn.Dispose();
                }

                // Note disposing has been done.
                disposed = true;

            }
        }


    }
}
