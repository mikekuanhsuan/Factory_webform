using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace factory.lib
{
    public class SQLDB
    {
        public String DBConnStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ZDBConnStr"].ConnectionString;
        private static SqlConnection conn;
        
        public void replace(int i)
        {
            if (i == 1)
            {
                DBConnStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["RuntimeConnStr"].ConnectionString;
            }
            else
            {
                DBConnStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ZDBConnStr"].ConnectionString;
            }
        }
        
        public void BuildConn()
        {
            conn = new SqlConnection(DBConnStr);
        }

        public String ConnnectionTest()
        {

            String ErrorMsg = "";
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    conn.Close();
                    ErrorMsg = "正常!";
                }

            }
            catch (InvalidCastException e)
            {
                ErrorMsg = "資料庫連線異常，錯誤訊息=" + e;
            }
            return ErrorMsg;
        }

        private static void OpenConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        private static void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public DataTable GetDataTable(string sql, CommandType commandType)
        {       //回傳值且沒有Parameters

            BuildConn();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = commandType;
            cmd.CommandTimeout = 120;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dt = new DataTable();

            OpenConnection();
            adapter.Fill(dt);
            cmd.Dispose();
            CloseConnection();
            return dt;
        }

        public DataTable GetDataTable(string sql, List<SqlParameter> par_list, CommandType commandType)
        {       //回傳值且有Parameters

            BuildConn();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = commandType;
            cmd.CommandTimeout = 120;
            foreach (SqlParameter param in par_list)
            {
                cmd.Parameters.Add(param);
            }
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataTable dt = new DataTable();
            OpenConnection();
            adapter.Fill(dt);
            cmd.Dispose();
            CloseConnection();
            return dt;

        }

        public void RunCmd(string sql, CommandType commandType)
        {       //增刪改 沒Parameters
            BuildConn();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = commandType;
            cmd.CommandTimeout = 120;
            OpenConnection();
            cmd.ExecuteReader();
            CloseConnection();
        }

        public void RunCmd(string sql, List<SqlParameter> par_list, CommandType commandType)
        {       //增刪改 有Parameters
            BuildConn();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = commandType;
            cmd.CommandTimeout = 120;

            foreach (SqlParameter param in par_list)
            {
                cmd.Parameters.Add(param);
            }
            OpenConnection();
            cmd.ExecuteReader();
            CloseConnection();

        }
    }
}