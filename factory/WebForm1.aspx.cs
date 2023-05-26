using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using factory.lib;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Data;
using System.Text.RegularExpressions;

using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;

namespace factory
{

    public partial class WebForm1 : System.Web.UI.Page
    {

        public SQLDB db = new SQLDB();

        public string line_chart(string TagName)
        {
            string time_s = "2021-10-15 00:00:00.000";
            string time_e = "2021-10-15 02:00:00.000";
            List<List<string>> par_list = new List<List<string>>();
            par_list.Add(new List<string>() { "{ min:'" + time_s + "'}" });
            par_list.Add(new List<string>() { "{ max:'" + time_e + "'}" });

            string sql = "DECLARE @STime Datetime DECLARE @ETime Datetime DECLARE @FactoryID nvarchar(10) DECLARE @TagName nvarchar(50) " +
                "set @STime = '2021-10-15 00:00:00.000' set @ETime = '2021-10-18 00:00:00.000' set @FactoryID = 'KY-T1HIST' set @TagName = '" + TagName + "' " +
                "exec h_GetTagValuelist @STime ,@ETime ,@FactoryID ,@TagName";
            DataTable dt = db.GetDataTable(sql, CommandType.Text);
            string d = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string datatime = Convert.ToDateTime(dt.Rows[i][0].ToString()).ToString("yyyy-MM-dd HH:mm:00");
                string v = dt.Rows[i][2].ToString();
                Decimal z = 0;
                if (v.Length > 0)
                {
                    z = Convert.ToDecimal(v);
                    if (z > 0)
                    {
                        d += "[" + "'" + datatime + "'\"," + z + "],";
                    }
                }

            }
            par_list.Add(new List<string>() { d });
            //轉換為JSON

            System.Web.Script.Serialization.JavaScriptSerializer o = new System.Web.Script.Serialization.JavaScriptSerializer();
            string datas = o.Serialize(par_list);

            //修改格式
            datas = datas.Replace("\\", "");
            datas = datas.Replace("\"", "");
            datas = datas.Replace("u0027", "'");
            return datas;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            
            string[] Files_name = Directory.GetFiles("C:\\test","*");
            File.Delete(Files_name[0]);
            /*
            string car_number = TextBox1.Text;
            string FilePath = "C:\\test\\"+car_number+".txt";
            File.Delete(FilePath);
            StringBuilder sb = new StringBuilder();
            sb.Append("車牌號碼:"+car_number);
            sb.Append("\n");
            sb.Append("車道:"+DropDownList1.SelectedValue);
            sb.Append("\n");
            sb.Append("公斤:" + TextBox3.Text);
            File.AppendAllText(FilePath, sb.ToString());
            //WriteAllText 會覆蓋
            sb.Clear();
            */
        }
    }
}