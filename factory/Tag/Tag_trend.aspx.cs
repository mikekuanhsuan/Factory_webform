using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using factory.lib;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;




namespace factory
{
    public partial class Tag_trend : System.Web.UI.Page
    {

        public f_class ff = new f_class();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tb_SDATE.Attributes.Add("readonly", "readonly");
                tb_EDATE.Attributes.Add("readonly", "readonly");
                //開始和結束時間
                string time_s = DateTime.Now.ToString("yyyy-MM-dd");
                string time_e = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                tb_SDATE.Text = time_s;
                tb_EDATE.Text = time_e;
                ff.Enabled(tb_SDATE.Text, imgb_n);
            }
        }
        protected void imgb_p_Click(object sender, ImageClickEventArgs e)
        {
            string n = tb_SDATE.Text;
            ff.check_date(n, tb_SDATE);
            ff.check_date(n, tb_EDATE);
            
            string time_s = Convert.ToDateTime(tb_SDATE.Text).AddDays(-1).ToString("yyyy-MM-dd");
            string time_e = tb_SDATE.Text;
            ff.Enabled(time_s, imgb_n);
            tb_SDATE.Text = time_s;
            tb_EDATE.Text = time_e;

        }

        protected void imgb_n_Click(object sender, ImageClickEventArgs e)
        {
            string n = tb_SDATE.Text;
            ff.check_date(n, tb_SDATE);
            ff.check_date(n, tb_EDATE);
            string time_s = tb_EDATE.Text;
            string time_e = Convert.ToDateTime(tb_EDATE.Text).AddDays(+1).ToString("yyyy-MM-dd");
            tb_SDATE.Text = time_s;
            tb_EDATE.Text = time_e;
            ff.Enabled(time_s, imgb_n);
        }
    }
}