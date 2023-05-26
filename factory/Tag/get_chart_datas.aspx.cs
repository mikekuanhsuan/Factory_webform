using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using factory.lib;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Text.RegularExpressions;



namespace factory.Tag
{
    public partial class get_chart_datas : System.Web.UI.Page
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetData(string f, string v, string m,  string b, string n, string s, string e)
        {

            SQLDB db = new SQLDB();
            List<List<object>> datas = new List<List<object>>();
            List<object[]> tag = new List<object[]>();
            List<SqlParameter> p_list = new List<SqlParameter>();
            //傳過來的資料弄成陣列
            string[] sArray = Regex.Split(v, ",", RegexOptions.IgnoreCase);
            string[] nArray = Regex.Split(n, ",", RegexOptions.IgnoreCase);
            //TAG名稱
            for (int j = 0; j < nArray.Length; j++)
            {
                string name = nArray[j] + " : " + sArray[j];
                tag.Add(new object[] { name });
            }
            datas.Add(new List<object> { tag });

            for (int i = 0; i < sArray.Length; i++)
            {
                List<object[]> data = new List<object[]>();
                string TagName = sArray[i];
                string sql = "DECLARE @STime Datetime DECLARE @ETime Datetime DECLARE @FactoryID nvarchar(10) DECLARE @TagName nvarchar(50) " +
                "SET @STime = @s set @ETime = @e SET @FactoryID = @f set @TagName = @t " +
                "EXEC h_GetTagValuelist @STime ,@ETime ,@FactoryID ,@TagName";
                p_list.Add(new SqlParameter("@s", s));
                p_list.Add(new SqlParameter("@e", e));
                p_list.Add(new SqlParameter("@f", f));
                p_list.Add(new SqlParameter("@t", TagName));
                /*
                if (m == "A")
                {
                    sql = "DECLARE @STime Datetime DECLARE @ETime Datetime DECLARE @FactoryID nvarchar(10) DECLARE @TagName nvarchar(50) " +
                    "SET @STime = '" + s + "' set @ETime = '" + e + "' SET @FactoryID = '" + f + "' set @TagName = '" + TagName + "' " +
                    "EXEC h_GetTagValuelistAVG @STime ,@ETime ,@FactoryID ,@TagName";
                }
                */
                DataTable dt = db.GetDataTable(sql, p_list,CommandType.Text);
                p_list.Clear();



                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        string datatime = Convert.ToDateTime(dt.Rows[j][0].ToString()).ToString("yyyy-MM-dd HH:mm:00");
                        DateTime time = Convert.ToDateTime(datatime);
                        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                        long x = ((long)(time - startTime).TotalMilliseconds + 28800000);
                        string vv = dt.Rows[j][2].ToString();
                        Decimal z = 0;
                        if (vv.Length > 0)
                        {
                            z = Math.Round(Convert.ToDecimal(vv), 2);
                            if (z > 0)
                            {

                                data.Add(new object[]
                                {
                                    x,
                                    z
                                });
                            }
                            else if (z <= 0)
                            {
                                if (b == "0")
                                {
                                    data.Add(new object[]
                                    {
                                        x,
                                        null
                                    });
                                }
                                else
                                {
                                    data.Add(new object[]
                                    {
                                        x,
                                        z
                                     });
                                }
                            }
                        }
                        else
                        {
                            if (b == "0")
                            {
                                data.Add(new object[]
                                {
                                        x,
                                        null
                                });
                            }
                            else
                            {
                                data.Add(new object[]
                                {
                                        x,
                                        0
                                 });
                            }
                        }

                    }
                    datas.Add(new List<object> { data });


                    
                    List<object> r = new List<object>();
                    
                    int y = 0;
                    double u = 0;
                    double p1 = 0;
                    int p2 = 0;
                    int p3 = 0;
                    /*
                    for (int j = 0; j < data.Count; j++)
                    {
                        if (j < 30)
                        {
                            if (b == "1")
                            {
                                for (int q = 0; q < j+1; q++)
                                {
                                    p1 += Math.Round(Convert.ToDouble(dt.Rows[q][2].ToString()), 2);
                                    p2++;
                                }
                                r.Add(new object[]
                                {
                                    Convert.ToInt64(data[j][0].ToString()),
                                    Math.Round(Convert.ToDouble(p1/p2), 2)
                                });
                            }
                            else
                            {
                                if (Convert.ToDouble(dt.Rows[j][2].ToString()) <= 0)
                                {
                                    r.Add(new object[]
                                    {
                                        Convert.ToInt64(data[j][0].ToString()),
                                        null
                                    });
                                }
                                else
                                {
                                    for (int q = 0; q < j+1; q++)
                                    {
                                        if (Convert.ToDouble(dt.Rows[q][2].ToString()) > 0)
                                        {
                                            p1 += Math.Round(Convert.ToDouble(dt.Rows[q][2].ToString()), 2);
                                            p2++;
                                        }
                                    }
                                    r.Add(new object[]
                                    {
                                        Convert.ToInt64(data[j][0].ToString()),
                                        Math.Round(p1/p2, 2)
                                    });
                                }
                            }
                        }
                        

                        else
                        {
                            if (b == "1")
                            {
                                for (int k = 0; k < 30; k++)
                                {
                                    u += Math.Round(Convert.ToDouble(dt.Rows[y + k][2].ToString()), 2);
                                }
                                y++;

                                r.Add(new object[]
                                {
                                    Convert.ToInt64(data[29 + y][0].ToString()),
                                    Math.Round(u / 30, 2)
                                });
                                u = 0;
                            }
                            else
                            {
                                //不要0
                                if (Convert.ToDouble(dt.Rows[j][2].ToString()) <= 0)
                                {
                                    r.Add(new object[]
                                    {
                                        Convert.ToInt64(data[j][0].ToString()),
                                        null
                                    });
                                }
                                else
                                {
                                    for (int k = 0; k < 30; k++)
                                    {
                                        if (Convert.ToDouble(dt.Rows[y + k][2].ToString()) > 0)
                                        {
                                            u += Math.Round(Convert.ToDouble(dt.Rows[y + k][2].ToString()), 2);
                                            p3++;
                                        }
                                    }
                                    

                                    r.Add(new object[]
                                    {
                                        Convert.ToInt64(data[29 + y][0].ToString()),
                                        Math.Round(u / p3, 2)
                                    });
                                    p3 = 0;
                                    u = 0;
                                }
                                y++;

                            }

                        }         
                    }
                    */
                    for (int j = 0; j < data.Count; j++)
                    {

                        if (Convert.ToDouble(dt.Rows[j][2].ToString()) <= 0)
                        {
                            y = 0;
                        }
                        else
                        {
                            y++;
                        }
                        if (y >= 30)
                        {
                            for (int k = 0; k < 30; k++)
                            {
                                u += Math.Round(Convert.ToDouble(dt.Rows[j - k][2].ToString()), 2);
                            }
                            r.Add(new object[]
                            {
                                    Convert.ToInt64(data[j][0].ToString()),
                                    Math.Round(u / 30, 2)
                            });
                            u = 0;
                        }
                        else
                        {
                            r.Add(new object[]
                            {
                                    Convert.ToInt64(data[j][0].ToString()),
                                    null
                            });
                        }
                        
                    }
                    datas.Add(new List<object> { r });
                    //null和0拿掉不要線 假設有0或NULL從30個點為後開始
                }
            }
            //轉換為JSON
            string oStrJson = JsonConvert.SerializeObject(new
            {
                datas
            });

            return oStrJson;

            /*
            SQLDB db = new SQLDB();
            //尋找群組
            string sql = "SELECT ServerName,SourceTag FROM Tag_Name_Desc WHERE Tag_Group= @Group ORDER BY Tag_Sort DESC";
            List<SqlParameter> par_list = new List<SqlParameter>();
            par_list.Add(new SqlParameter("@Group", group));
            DataTable dt = db.GetDataTable(sql, par_list, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                //二微陣列
                List<List<object>> datas = new List<List<object>>();
                List<object[]> tag = new List<object[]>();
                //新增TAG名稱
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    tag.Add(new object[]{dt.Rows[j][1].ToString()});
                }
                datas.Add(new List<object> { tag });
                //新增TAG的值
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string FactoryID = dt.Rows[i][0].ToString();
                    string TagName = dt.Rows[i][1].ToString();
                    sql = "DECLARE @STime Datetime DECLARE @ETime Datetime DECLARE @FactoryID nvarchar(10) DECLARE @TagName nvarchar(50) " +
                    "set @STime = '"+s+"' set @ETime = '"+e+"' set @FactoryID = '"+ FactoryID + "' set @TagName = '"+ TagName + "' " +
                    "exec h_GetTagValuelist @STime ,@ETime ,@FactoryID ,@TagName";
                    DataTable dx = db.GetDataTable(sql, CommandType.Text);
                    List<object[]> data = new List<object[]>();
                    
                    for (int j = 0; j < dx.Rows.Count; j++)
                    {
                        string datatime = Convert.ToDateTime(dx.Rows[j][0].ToString()).ToString("yyyy-MM-dd HH:mm:00");
                        //轉換為毫秒
                        DateTime time = Convert.ToDateTime(datatime);
                        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                        long x = ((long)(time - startTime).TotalMilliseconds + 28800000);

                        string v = dx.Rows[j][2].ToString();
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
                    }
                    
                    datas.Add(new List<object> { data });
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
            */
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}