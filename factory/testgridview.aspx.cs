using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using factory.lib;
using System.Net;
using System.IO;
using System.Net.Sockets;


namespace factory
{
    public partial class testgridview : System.Web.UI.Page
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
            string x = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string datatime = Convert.ToDateTime(dt.Rows[i][0].ToString()).ToString("yyyy-MM-dd HH:mm:00");
                DateTime time = Convert.ToDateTime(datatime);
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                x = ((long)(time - startTime).TotalMilliseconds + 28800000).ToString();
                string v = dt.Rows[i][2].ToString();
                Decimal z = 0;
                if (v.Length > 0)
                {
                    z = Convert.ToDecimal(v);
                    if (z > 0)
                    {
                        d += "[" + "" + x + "\","+z+"],";
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

        public string line1()
        {
            return line_chart("HPA-1_KW");
        }

        public string line2()
        {
            return line_chart("HPA-2_KW");
        }

        public string line3()
        {
            return line_chart("HPA-3_KW");
        }

        public string line4()
        {
            return line_chart("HPA-4_KW");
        }

        public string line5()
        {
            return line_chart("HPA-5_KW");
        }

        public string line6()
        {
            return line_chart("HPA-6_KW");
        }

        public List<string> g()
        {
            List<string> s = new List<string>();
            s.Add("3");
            s.Add("4");
            return s;
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        protected string GetIPAddress_real()
        {
            string SvrIP = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address).ToString();
            return SvrIP;
        }

        protected string Get_OS_Browser_DV()
        {
            HttpBrowserCapabilities browser = Request.Browser;
            OperatingSystem OSv = System.Environment.OSVersion;
            string data = OSv + "," + browser.Type + "," + browser.MobileDeviceModel;
            return data;
        }
        private List<string> GetHostIPAddress()
        {
            List<string> lstIPAddress = new List<string>();
            IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ipa in IpEntry.AddressList)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    lstIPAddress.Add(ipa.ToString());
            }
            return lstIPAddress; // result: 192.168.1.17 ......
        }




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*
                //IP
                string i = GetIPAddress();
                HttpBrowserCapabilities browser = Request.Browser;
                //作業系統+版本
                OperatingSystem OSv = System.Environment.OSVersion;
                Response.Write("作業系統版本 : " + OSv.ToString());
                //瀏覽器+版本
                Response.Write(browser.Type);
                Response.Write("<br>");
                //取得行動裝置品牌
                Response.Write(browser.MobileDeviceModel);
                Response.Write("<br>");
                */
                //string x = GetIPAddress();
                //string i = Get_OS_Browser_DV();
                //IPAddress SvrIP = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
                //Response.Write(SvrIP);
                /*
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                Response.Write(Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
                */
                //string userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");
                //Response.Write(userAgent);
                /*
                if (agent.Length > 0)
                {
                    string userPhonetype = agent[1]; //其中，“/”拆分的第一项的内容就是用户的手机品牌信息
                    Response.Write(userAgent);
                }
                */
                //目前網頁
                //Response.Write(System.IO.Path.GetFileName(Request.PhysicalPath));
            }
            
        }

    }
}