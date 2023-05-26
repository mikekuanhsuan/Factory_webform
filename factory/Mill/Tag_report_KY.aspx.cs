using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Timers;
using factory.lib;
using System.Data;

namespace factory
{
    public partial class Tag_report : System.Web.UI.Page
    {

        public void data(int f,string time_data)
        {
                //取得現在時間
                string time_ymd = time_data;
                tb_SDATE.Text = time_ymd.Substring(0, 10);

                //創造欄位
                DataTable dt = new DataTable();
                for (int j = 0; j < 63; j++)
                {
                    dt.Columns.Add(j.ToString(), typeof(string));
                }

                string sql = "";
                SQLDB db = new SQLDB();
                DataTable data = new DataTable();

                //今天8點到隔天8點
                for (int k = 0; k < 25; k++)
                {
                    //取得今天8點到隔天8點的資料
                    string time_d = Convert.ToDateTime(time_ymd).AddHours(k).ToString("yyyy-MM-dd HH:00:00");
                    if (f == 1)
                    {
                        sql = "SELECT * FROM Value_Hour WHERE DataDateTime = '" + time_d + "' AND TagName in " +
                        "('HPA-1_A','HPA-1_KW','HPA-2_A','HPA-2_KW','H-0113','B-0117','B-0217','B-0120','W-0101','W-0102','W-0201','W-0202')";
                    }
                    else if (f == 2)
                    {
                        sql = "";
                    }
                    else
                    {
                        sql = "";
                    }

                    data = db.GetDataTable(sql,CommandType.Text);
                    if (data.Rows.Count > 0)
                    {
                        List<int> Lists = new List<int>();
                        for (int l = 0; l < data.Rows.Count; l++)
                        {
                            Lists.Add(Convert.ToInt32(Math.Round(Convert.ToDecimal(data.Rows[l][3].ToString()), 0)));
                        }
                        //時間
                        int t = 8;
                        t += k;
                        if (t > 25)
                        {
                            t -= 24;
                        }
                        dt.Rows.Add(t, Lists[4], Lists[6], Lists[5], Lists[7], Lists[3], null, null, Lists[0], Lists[2], Lists[1],
                            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                            null, Lists[8], null, Lists[9], null, null, null, null, null, Lists[10], null, Lists[11], null,
                            null, null, null, null, null, null, null);
                    }
                    else
                    {
                        break;
                    }
                }
                ViewState["GV1"] = dt;
                GV1.DataSource = dt;
                GV1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            /*
            Session["show"] = "2";
            Session["factory"] = "1";
            */
            //取得磨機號碼
            string M = Request.QueryString["M"];
            if (M == "12")
            {
                //HyperLink h = (HyperLink)Master.FindControl("KY_34");
                //h.CssClass = "list-group-item list-group-item-action list-group-item-dark collapsed";
                if (!IsPostBack)
                {
                    string time_ymd = DateTime.Now.ToString("yyyy-MM-dd 08:00:00");
                    data(1, time_ymd);
                }
            }
            else if (M == "34")
            {
                //HyperLink h = (HyperLink)Master.FindControl("KY_56");
                //h.CssClass = "list-group-item list-group-item-action list-group-item-dark collapsed";
                if (!IsPostBack)
                {
                    string time_ymd = DateTime.Now.ToString("yyyy-MM-dd 08:00:00");
                    //data(2, time_ymd);
                }
            }
            else if (M == "56")
            {
                //HyperLink h = (HyperLink)Master.FindControl("KY_12");
                //h.CssClass = "list-group-item list-group-item-action list-group-item-dark collapsed";
                if (!IsPostBack)
                {
                    string time_ymd = DateTime.Now.ToString("yyyy-MM-dd 08:00:00");
                    //data(3, time_ymd);
                }
            }
            else
            {
                Response.Redirect("Tag_report_KY.aspx?M=12");
            }
            /*
            HyperLink x = (HyperLink)Master.FindControl("Tag_r");
            x.CssClass = "list-group-item list-group-item-action bg-light";
            */
        }



        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            string M = Request.QueryString["M"];
            string tb_time = Request.Form[tb_SDATE.UniqueID];
            string time_ymd = DateTime.Now.ToString(tb_time + " 08:00:00");
            data(1, time_ymd);
        }

    }
}