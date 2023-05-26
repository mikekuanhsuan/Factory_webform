using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using factory.lib;

namespace factory
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        /*
        public string test(int n)
        {
            string F = "";
            string factory = "";
            if (n == 0)
            {
                F = "KY-T1HIST";
                factory = "觀音廠  ";
            }
            else if(n == 1)
            {
                F = "BL-T1HIST";
                factory = "八里廠  ";
            }
            else if (n == 2)
            {
                F = "QX-T1HIST";
                factory = "全興廠  ";
            }
            
            else if (n == 3)
            {
                F = "ZB-T1HIST";
                factory = "彰濱廠  ";
            }
            
            else if (n == 4)
            {
                F = "KH-PCC-LH";
                factory = "高雄廠  ";
            }
            else if (n == 5)
            {
                F = "LD-T1HIST";
                factory = "龍德廠  ";
            }
            else if (n == 6)
            {
                F = "LZ-T1HIST";
                factory = "利澤廠  ";
            }
            else
            {
                F = "HL-T1HIST";
                factory = "花蓮廠  ";
            }

            string M = "#0";
            string time_s = DateTime.Now.ToString("yyyy-MM-01");
            //這個月最後一天
            string time_e = Convert.ToDateTime(time_s).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            SQLDB db = new SQLDB();
            db.replace(1);
            string sql = "SELECT DataDate FROM G_Air_Comp WHERE FactoryID like '" + '%' + F + '%' + "'AND MID = '" + M + "' AND DataDate >= '"+time_s+"' AND DataDate <= '"+time_e+"' GROUP BY DataDate";

            DataTable dt = db.GetDataTable(sql,CommandType.Text);
            //判斷是否有資料
            if (dt.Rows.Count > 0)
            {
                //比功率
                List<List<string>> par_list = new List<List<string>>();
                sql = "SELECT DataDate,Specific_Power FROM G_Air_Comp where FactoryID like '" + '%' + F + '%' + "'AND MID = '" + M + "' AND DataDate >= '"+time_s+"' AND DataDate <= '"+time_e+"'";
                DataTable dx = db.GetDataTable(sql,CommandType.Text);
                string d = "";
                for (int j = 0; j < dx.Rows.Count; j++)
                {
                    decimal z = Convert.ToDecimal(dx.Rows[j][1].ToString());
                    //比功率大於0才加到list裡面
                    if (z > 0)
                    {
                        string datatime = Convert.ToDateTime(dx.Rows[j][0].ToString()).ToString("yyyy-MM-dd");
                        d += "{x:" + "'" + datatime + "'" + ",y:" + dx.Rows[j][1].ToString() + "\"},";
                    }
                }
                par_list.Add(new List<string>() { d });
                //平均比功率
                string y = time_s.Substring(0, 4);
                string m = time_s.Substring(5, 2);
                //轉換月 06月---6月
                int mv = Convert.ToInt32(m);
                //取得最後一天
                int x = Convert.ToInt32(time_e.Substring(8, 2));
                sql = "SELECT (SUM(Power_C_01) + SUM(Power_C_02) + SUM(Power_C_03) + SUM(Power_C_04) + SUM(Power_C_05)) / sum(Air_Consumption)  AS avgSP," +
                    "FactoryID , DATEPART(YYYY,DataDate) as tYear, DATEPART(MM,DataDate) as tMonth" +
                    " FROM G_Air_Comp where Specific_Power>0" +
                    "GROUP BY FactoryID,MID , DATEPART(YYYY,DataDate), DATEPART(MM,DataDate)" +
                    "HAVING FactoryID = '" + F + "'AND MID = '" + M + "' and DATEPART(YYYY,DataDate) = '"+y+"' and DATEPART(MM,DataDate)='"+m+"'";

                dx = db.GetDataTable(sql,CommandType.Text);
                if (dx.Rows.Count > 0)
                {
                    decimal v = Convert.ToDecimal(dx.Rows[0][0].ToString());
                    v = Math.Round(v, 2);
                    string dd = "";
                    for (int i = 0; i < x; i++)
                    {
                        string datatime = Convert.ToDateTime(time_s).AddDays(i).ToString("yyyy-MM-dd");
                        dd += "{x:" + "'" + datatime + "'" + ",y:" + v + "\"},";
                    }
                    par_list.Add(new List<string>() { dd });
                    par_list.Add(new List<string>() { "{ min:'" + time_s + "'}" });
                    par_list.Add(new List<string>() { "{ max:'" + time_e + "'}" });
                    par_list.Add(new List<string>() { "{ b:'比功率'}" });
                    par_list.Add(new List<string>() { "{ avg_b:'平均比功率'}" });
                    par_list.Add(new List<string>() { "{ title:'" + factory + mv + "月比功率'}" });
                    
                    //轉換為JSON
                    System.Web.Script.Serialization.JavaScriptSerializer o = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string datas = o.Serialize(par_list);
                    //修改格式
                    datas = datas.Replace("\\", "");
                    datas = datas.Replace("\"", "");
                    datas = datas.Replace("u0027", "'");
                    //Session["datas"] = datas;
                    return datas;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        */
    }
}