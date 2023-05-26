using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using factory.lib;
using System.Net;
using System.Text.RegularExpressions;


namespace factory.Check
{
    public partial class add_Quality : System.Web.UI.Page
    {
        public f_class ff = new f_class();
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
        public string Get_userAgent()
        {
            string userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");
            return userAgent;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tb_SDATE.Attributes.Add("readonly", "readonly");
                string time_s = DateTime.Now.ToString("yyyy-MM-dd");
                tb_SDATE.Text = time_s;
                string time_hm = DateTime.Now.ToString("HH:mm");
                int h = Convert.ToInt32(time_hm.Substring(0, 2));
                int m = Convert.ToInt32(time_hm.Substring(3, 2));
                //時
                DDL_H.Items[h].Selected = true;
                //分
                DDL_M.Items[m].Selected = true;
                SQLDB db = new SQLDB();
                string sql = "SELECT FactoryID,FactoryName FROM Factory ORDER BY aOrder ASC";
                DataTable dt = db.GetDataTable(sql, CommandType.Text);
                //廠區
                for (int i = 0; i < dt.Rows.Count-1; i++)
                {
                    DDL_F.Items.Add(new ListItem(dt.Rows[i][1].ToString()+"廠",dt.Rows[i][0].ToString()));
                }
                //磨機
                sql = "SELECT Mill_ID FROM G_Milling_Machine_Mapping WHERE FactoryID ='KY-T1HIST'";
                dt = db.GetDataTable(sql, CommandType.Text);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rbl_M.Items.Add(new ListItem(dt.Rows[i][0].ToString(), dt.Rows[i][0].ToString()));
                }
                /*
                //水泥
                sql = "SELECT * FROM A_Product";
                dt = db.GetDataTable(sql, CommandType.Text);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rbl_P.Items.Add(new ListItem("(" + dt.Rows[i][0].ToString() + ")" + dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                }
                */
                rbl_M.Items[0].Selected = true;
                //rbl_P.Items[0].Selected = true;
                //提示的DIV
                tip.Visible = false;

            }
        }

        protected void DDL_F_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLDB db = new SQLDB();
            List<SqlParameter> p_list = new List<SqlParameter>();
            string sql = "SELECT Mill_ID FROM G_Milling_Machine_Mapping WHERE FactoryID = @F";
            p_list.Add(new SqlParameter("@F", DDL_F.SelectedValue));
            DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
            rbl_M.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rbl_M.Items.Add(new ListItem(dt.Rows[i][0].ToString(), dt.Rows[i][0].ToString()));
            }
            rbl_M.Items[0].Selected = true;
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (tb_M.Text == "" && tb_R.Text == "" && tb_S.Text == "")
            {
                lb_error.Visible = true;
                btn_save.Attributes.Add("style", "margin-left:20px");
            }
            else
            {
                string F = DDL_F.SelectedValue;
                string time = tb_SDATE.Text + " " + DDL_H.SelectedValue + ":" + DDL_M.SelectedValue;
                string l = rbl_L.SelectedValue;
                string m = rbl_M.SelectedValue;
                string p = rbl_P.SelectedValue;
                string data = GetIPAddress_real() + "," + GetIPAddress() + "," + Get_userAgent();
                //時間正確
                DateTime t;
                bool flag = DateTime.TryParse(time, out t);
                if (flag == true)
                {
                    //判斷是否有在同一小時內輸入資料 前一筆資料要給Visible X
                    string sql = "SELECT TOP 1 FactoryID,Mill_ID, DTime,DateTime FROM A_Product_Quality2 WHERE FactoryID LIKE @F+'%' AND Mill_ID = @M_ID " +
                        "AND DateTime >= @time_s AND DateTime < @time_e";
                    string time_s = DateTime.Now.ToString("yyyy-MM-dd HH:00");
                    string time_e = Convert.ToDateTime(time_s).AddHours(1).ToString("yyyy-MM-dd HH:00");
                    SQLDB db = new SQLDB();
                    List<SqlParameter> p_list = new List<SqlParameter>();
                    p_list.Add(new SqlParameter("@F", F));
                    p_list.Add(new SqlParameter("@M_ID", m));
                    p_list.Add(new SqlParameter("@time_s", time_s));
                    p_list.Add(new SqlParameter("@time_e", time_e));
                    //先修改再來新增!
                    DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
                    if (dt.Rows.Count > 0)
                    {
                        p_list.Clear();
                        sql = "UPDATE A_Product_Quality2 SET Visible = 'X' WHERE FactoryID = @F AND Mill_ID = @M AND DateTime >= @D";
                        p_list.Add(new SqlParameter("@F", dt.Rows[0][0].ToString()));
                        p_list.Add(new SqlParameter("@M", dt.Rows[0][1].ToString()));
                        string timeStr = dt.Rows[0][3].ToString();
                        DateTime x = new DateTime();
                        DateTime.TryParse(timeStr, out x);
                        p_list.Add(new SqlParameter("@D", x.ToString("yyyy-MM-dd HH:mm:ss")));
                        db.RunCmd(sql, p_list, CommandType.Text);
                        
                    }
                    /*
                    bool tb_M_V = Regex.IsMatch(tb_M.Text, "^([0-9]\\.[0-9]{1}|[0-9]\\.[0-9]{2}|\\.[0-9]{2}|[1-9][0-9]\\.[0-9]{1}|[1-9][0-9]\\.[0-9]{2}|[0-9][0-9]|[1-9][0-9]\\.[0-9]{2})$|^([0-9]|[0-9][0-9]|[0-99])$|^100$") ? true : false;
                    bool tb_S_V = Regex.IsMatch(tb_S.Text, "^([0-9]\\.[0-9]{1}|[0-9]\\.[0-9]{2}|\\.[0-9]{2}|[1-9][0-9]\\.[0-9]{1}|[1-9][0-9]\\.[0-9]{2}|[0-9][0-9]|[1-9][0-9]\\.[0-9]{2})$|^([0-9]|[0-9][0-9]|[0-99])$|^100$") ? true : false;
                    bool tb_R_V = Regex.IsMatch(tb_R.Text, "^([0-9]\\.[0-9]{1}|[0-9]\\.[0-9]{2}|\\.[0-9]{2}|[1-9][0-9]\\.[0-9]{1}|[1-9][0-9]\\.[0-9]{2}|[0-9][0-9]|[1-9][0-9]\\.[0-9]{2})$|^([0-9]|[0-9][0-9]|[0-99])$|^100$") ? true : false;
                    //(object)DBNull.Value
                    if (tb_M_V == false && tb_S_V == false && tb_R_V == false)
                    {
                        lb_error.Text = "輸入有誤";
                        lb_error.Visible = true;
                        btn_save.Attributes.Add("style", "margin-left:70px");
                    }
                    else
                    {
                        object tb_M_V2 = DBNull.Value;
                        object tb_S_V2 = DBNull.Value;
                        object tb_R_V2 = DBNull.Value;
                        if (tb_M_V == true)
                        {
                            tb_M_V2 = tb_M.Text;
                        }
                        if (tb_S_V == true)
                        {
                            tb_S_V2 = tb_M.Text;
                        }
                        if (tb_R_V == true)
                        {
                            tb_R_V2 = tb_M.Text;
                        }
                    }
                    */
                    object tb_M_V2 = tb_M.Text;
                    object tb_S_V2 = tb_S.Text;
                    object tb_R_V2 = tb_R.Text;
                    if (tb_M.Text == "")
                    {
                        tb_M_V2 = DBNull.Value;
                    }
                    if (tb_S.Text == "")
                    {
                        tb_S_V2 = DBNull.Value;
                    }
                    if (tb_R.Text == "")
                    {
                        tb_R_V2 = DBNull.Value;
                    }

                    sql = "INSERT INTO A_Product_Quality2(FactoryID,Mill_ID,DTime,DateTime,ProductID,Moisture,Specific_Surface,Residual_On_Sieve,Visible,Client_Info,Laboratory)" +
                        " VALUES(@F,@M_ID,@time,GETDATE(),@P,@M,@S,@R,NULL,@data,@L)";
                    p_list.Clear();
                    p_list.Add(new SqlParameter("@F", F));
                    p_list.Add(new SqlParameter("@M_ID", m));
                    p_list.Add(new SqlParameter("@time", time));
                    p_list.Add(new SqlParameter("@P", p));
                    p_list.Add(new SqlParameter("@M", tb_M_V2));
                    p_list.Add(new SqlParameter("@S", tb_S_V2));
                    p_list.Add(new SqlParameter("@R", tb_R_V2));
                    p_list.Add(new SqlParameter("@data", data));
                    p_list.Add(new SqlParameter("@L", l));
                    db.RunCmd(sql, p_list, CommandType.Text);
                    lb_error.Visible = false;
                    btn_save.Attributes.Add("style", "margin-left:140px");
                    lb_M1.Text = "水份 " + tb_M.Text + " %";
                    lb_M2.Text = "比表面積 " + tb_S.Text + " ㎡/kg";
                    lb_M3.Text = "篩餘 " + tb_R.Text + " %";
                    tip.Visible = true;
                    //儲存完後清空欄位
                    tb_M.Text = "";
                    tb_R.Text = "";
                    tb_S.Text = "";
                }
            }
        }

        protected void btn_v_Click(object sender, EventArgs e)
        {
            Response.Redirect("experiment_data.aspx");
        }
    }
}