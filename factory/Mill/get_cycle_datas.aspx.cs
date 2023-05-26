using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using factory.lib;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace factory.Mill
{
    public partial class get_cycle_datas : System.Web.UI.Page
    {
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetData(string Mill, string F, string s, string e)
        {
            SQLDB db = new SQLDB();

            string sql = "SELECT DataTime,Motor_Current_A,Motor_Current_B,Motor_PowerKW_A,Motor_PowerKW_B,Bucket_Elevator_A,Bucket_Elevator_B,OSEPA_Current,OSEPA_RPM " +
                "FROM G_Milling_Machine WHERE FactoryID = @F " +
                "AND Mill_ID = @Mill AND DataTime >= @time_s AND DataTime < @time_e";
            List<SqlParameter> par_list = new List<SqlParameter>();
            par_list.Add(new SqlParameter("@Mill", Mill.Replace("r","#")));
            par_list.Add(new SqlParameter("@F", F));
            par_list.Add(new SqlParameter("@time_s", s));
            par_list.Add(new SqlParameter("@time_e", e));
            DataTable dt = db.GetDataTable(sql, par_list, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                List<List<object>> datas = new List<List<object>>();
                List<object[]> tag = new List<object[]>();
                //機器的名稱
                par_list.Clear();
                sql = "SELECT * FROM G_Milling_Machine_Mapping WHERE FactoryID = @F AND Mill_ID = @Mill";
                par_list.Add(new SqlParameter("@F", F));
                par_list.Add(new SqlParameter("@Mill", Mill.Replace("r", "#")));
                DataTable dx = db.GetDataTable(sql, par_list, CommandType.Text);
                for (int i = 2; i < 10; i++)
                {
                    if (dx.Rows[0][i].ToString().Length > 0)
                    {
                        tag.Add(new object[] { dx.Rows[0][i].ToString() });
                    }
                }

                datas.Add(new List<object> { tag });
                //新增值
                for (int j = 0; j < 8; j++)
                {
                    //判斷要不要新增資料
                    int f = 0;
                    List<object[]> data = new List<object[]>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string datatime = Convert.ToDateTime(dt.Rows[i][0].ToString()).ToString("yyyy-MM-dd HH:mm:00");
                        DateTime time = Convert.ToDateTime(datatime);
                        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                        long x = ((long)(time - startTime).TotalMilliseconds + 28800000);
                        string v = dt.Rows[i][j+1].ToString();
                        Decimal z = 0;
                        if (v.Length > 0)
                        {

                            z = Convert.ToDecimal(v);
                            if (z > 0)
                            {
                                data.Add(new object[]
                                {
                                    x,
                                    z
                                });
                            }

                            else if (z == 0)
                            {
                                data.Add(new object[]
                                {
                                    x,
                                    null
                                });
                            }
                        }
                        else if (v.Length == 0)
                        {
                            f += 1;
                            break;
                        }
                    }
                    if (f == 0)
                    {
                        datas.Add(new List<object> { data });
                    }
                }
                //轉換為JSON
                string oStrJson = JsonConvert.SerializeObject(new
                {
                    datas
                });

                return oStrJson;
            }
            else
            {
                return "1";
            }
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}