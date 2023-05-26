using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using factory.lib;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;


namespace factory.factory_index
{
    public partial class index : System.Web.UI.Page
    {
        public f_class ff = new f_class();
        public String doughnut()
        {
            if (lb_power.Text != "無資料" && lb_power.Text != "0")
            {
                Double power = Math.Round(Convert.ToDouble(lb_power.Text.Replace(",", "")), 0);
                List<List<string>> r_list = new List<List<string>>();
                List<string> par_list = new List<string>();
                List<string> name = new List<string>();
                List<string> lb_list = new List<string>();
                List<string> div_list = new List<string>();
                for (int j = 1; j < 9; j++)
                {
                    //新增前端8個Label的名稱
                    string n = "lb_p" + j.ToString();
                    lb_list.Add(n);
                    //新增前端8個div的名稱
                    string d = "d" + j.ToString();
                    div_list.Add(d);
                }
                //資料
                Double other = 100;
                for (int i = 0; i < 8; i++)
                {
                    Control div = Page.Master.FindControl("ContentPlaceHolder1").FindControl(div_list[i]);
                    if (div.Visible == true)
                    {
                        Label lb = (Label)Page.Master.FindControl("ContentPlaceHolder1").FindControl(lb_list[i]);
                        Double d = Convert.ToDouble(lb.Text.Replace(",", ""));
                        Double r = Math.Round(d / power * 100, 0);
                        other -= r;
                        par_list.Add(r.ToString());
                        string n = "#" + (i + 1).ToString() + " " + r.ToString() + "%";
                        name.Add(n);
                    }
                }
                par_list.Add(other.ToString());
                name.Add("#其它" + " " + other.ToString() + "%");

                r_list.Add(par_list);
                r_list.Add(name);
                System.Web.Script.Serialization.JavaScriptSerializer o = new System.Web.Script.Serialization.JavaScriptSerializer();
                string datas = o.Serialize(r_list);
                return datas;
            }
            else
            {
                return "1";
            }
        }

        

        //顯示#1~#8畫面和資料
        public void data_visible(string F,string Month)
        {
            //給DIV超連結
            string D = tb_SDATE.Text.Replace("-", "/");
            link_d.Attributes.Add("onclick", "location.href='/power/Power_D.aspx?F="+F+"&D="+D+"'");
            
            SQLDB db = new SQLDB();
            //判斷哪些機器的資料要顯示
            //先去取得該工廠有哪些機器
            string sql = "SELECT * FROM G_Power_Mapping WHERE FactoryID = @F_ID";
            List<SqlParameter> p_list = new List<SqlParameter>();
            List<string> s_list = new List<string>();
            List<string> div_list = new List<string>();
            p_list.Add(new SqlParameter("@F_ID", F));
            DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    //其它8台機器
                    if (dt.Rows[0][i * 2 + 9].ToString() != "")
                    {
                        s_list.Add(dt.Rows[0][i * 2 + 9].ToString());
                    }
                    //新增前端8個div的名稱
                    string d = "d" + (i + 1).ToString();
                    div_list.Add(d);
                }
            }
            else
            {
                div_power.Visible = false;
            }

            //隱藏沒有資料的
            if (s_list.Count > 0)
            {
                for (int k = 0; k < 8 - s_list.Count; k++)
                {
                    Control div = Page.Master.FindControl("ContentPlaceHolder1").FindControl(div_list[7 - k]);
                    div.Visible = false;
                }
            }
            
            //顯示該工廠的機器資料
            string DMonth = Month.Replace("-", "");
            sql = "SELECT * FROM G_Power_M WHERE FactoryID = @F_ID AND DMonth = @DMonth";
            p_list.Clear();
            p_list.Add(new SqlParameter("@F_ID", F));
            p_list.Add(new SqlParameter("@DMonth", DMonth));
            dt = db.GetDataTable(sql, p_list, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                //新增前端8個Label的名稱
                List<string> lb_list = new List<string>();
                for (int j = 1; j < 9; j++)
                {
                    string n = "lb_p" + j.ToString();
                    lb_list.Add(n);
                }
                //用電量
                lb_power.Text = "0";
                if (dt.Rows[0][15].ToString() != "")
                {
                    Decimal dd = Convert.ToDecimal(dt.Rows[0][15].ToString());
                    int xx = Convert.ToInt32(dd);
                    lb_power.Text = string.Format("{0:#,0.####}", Convert.ToDecimal(xx));
                }
                //機器資料
                for (int i = 0; i < s_list.Count; i++)
                {
                    string d = dt.Rows[0][19 + i].ToString();
                    Label lb = (Label)Page.Master.FindControl("ContentPlaceHolder1").FindControl(lb_list[i]);
                    lb.Text = "0";
                    if (d != "")
                    {
                        Decimal dd = Convert.ToDecimal(d);
                        int xx = Convert.ToInt32(dd);
                        lb.Text = string.Format("{0:#,0.####}", Convert.ToDecimal(xx));
                    }
                }
            }
            //無資料給0
            else
            {
                List<string> lb_list = new List<string>();
                for (int j = 1; j < 9; j++)
                {
                    string n = "lb_p" + j.ToString();
                    lb_list.Add(n);
                }
                lb_power.Text = "無資料";
                for (int i = 0; i < 8; i++)
                {
                    Label lb = (Label)Page.Master.FindControl("ContentPlaceHolder1").FindControl(lb_list[i]);
                    lb.Text = "無資料";
                }
            }
            //廠區無資料全部隱藏
            /*
            else
            {
                List<string> div_list = new List<string>();
                for (int j = 1; j < 9; j++)
                {
                    string n = "d" + j.ToString();
                    div_list.Add(n);
                }
                Control d = Page.Master.FindControl("ContentPlaceHolder1").FindControl("d");
                d.Visible = false;
                Control div_power = Page.Master.FindControl("ContentPlaceHolder1").FindControl("div_power");
                div_power.Visible = false;
                for (int k = 0; k < 8; k++)
                {
                    Control div = Page.Master.FindControl("ContentPlaceHolder1").FindControl(div_list[k]);
                    div.Visible = false;
                }
            }
            */
        }
        //台電用電量
        public void TPC_M(string F, string n)
        {
            SQLDB db = new SQLDB();
            n += "-01";
            string sql = "SELECT Power_KW FROM G_TPC_M WHERE FactoryID = @F_ID AND Dtime = @Dtime";
            List<SqlParameter> p_list = new List<SqlParameter>();
            p_list.Clear();
            p_list.Add(new SqlParameter("@F_ID", F));
            p_list.Add(new SqlParameter("@Dtime", n));
            DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                lb_contract.Text = string.Format("{0:#,0.####}", Convert.ToDecimal(dt.Rows[3][0].ToString()));
                lb_rush.Text = string.Format("{0:#,0.####}", Convert.ToDecimal(dt.Rows[1][0].ToString()));
                lb_half.Text = string.Format("{0:#,0.####}", Convert.ToDecimal(dt.Rows[0][0].ToString()));
                lb_sd.Text = string.Format("{0:#,0.####}", Convert.ToDecimal(dt.Rows[2][0].ToString()));
                try
                {
                    lb_off.Text = string.Format("{0:#,0.####}", Convert.ToDecimal(dt.Rows[4][0].ToString()));
                }
                catch
                {
                    lb_off.Text = "0";
                }
                double rush = Convert.ToInt32(dt.Rows[1][0].ToString());
                double half = Convert.ToInt32(dt.Rows[0][0].ToString());
                double sd = Convert.ToInt32(dt.Rows[2][0].ToString());
                double off = 0;
                try
                {
                    off = Convert.ToInt32(dt.Rows[4][0].ToString());
                }
                catch
                {

                }
                
                double sum = rush + half + sd + off;

                string d_rush = rush == 0 ? "0" : Math.Round(((rush * 100) / sum),0).ToString();
                lb_rush_p.Text = d_rush.Length < 2 ? "\xA0 " + d_rush + "%" : d_rush+"%";
                string d_half = half == 0 ? "0" : Math.Round(((half * 100) / sum),0).ToString();
                lb_half_p.Text = d_half.Length < 2 ? "\xA0 " + d_half + "%" : d_half+"%";
                string d_sd = sd == 0 ? "0" : Math.Round(((sd * 100) / sum),0).ToString();
                lb_sd_p.Text = d_sd.Length < 2 ? "\xA0 " + d_sd + "%" : d_sd+"%";
                string d_off = off == 0 ? "0" : Math.Round(((off * 100) / sum),0).ToString();
                lb_off_p.Text = d_off.Length < 2 ? "\xA0 " + d_off + "%" : d_off+"%";
            }
            else
            {
                lb_contract.Text = "0";
                lb_rush.Text = " 0";
                lb_half.Text = "0";
                lb_sd.Text = "0";
                lb_off.Text = "0";
                lb_rush_p.Text = "0";
            }
        }
        public void power(string F,string n)
        {
            //彰濱的第二廠區
            zb.Visible = false;

            SQLDB db = new SQLDB();
            n += "-01";
            string sql = "SELECT SUM(Power_Generation) FROM G_SE_D WHERE SE_ID = @SE_ID AND DDate >= @time_s AND DDate < @time_e";
            List<SqlParameter> p_list = new List<SqlParameter>();
            p_list.Clear();
            string time_e = Convert.ToDateTime(n).AddMonths(1).ToString("yyyy-MM-dd");
            p_list.Add(new SqlParameter("@time_s", n));
            p_list.Add(new SqlParameter("@time_e", time_e));

            if (F == "KY-T1HIST")
            {
                lb_sys.Text = "396.00";
                p_list.Add(new SqlParameter("@SE_ID", "HG00025"));
                DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    lb_energy.Text = dt.Rows[0][0].ToString();
                }
            }
            else if (F == "ZB-T1HIST")
            {
                zb.Visible = true;
                lb_sys.Text = "99.76";
                lb_sys2.Text = "444.36";
                p_list.Add(new SqlParameter("@SE_ID", "HG00017"));
                DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    lb_energy.Text = dt.Rows[0][0].ToString();
                }
                p_list.Clear();
                p_list.Add(new SqlParameter("@time_s", n));
                p_list.Add(new SqlParameter("@time_e", time_e));
                p_list.Add(new SqlParameter("@SE_ID", "HG00010"));
                dt = db.GetDataTable(sql, p_list, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    lb_energy_2.Text = dt.Rows[0][0].ToString();
                }
            }
            else if(F == "LZ-T1HIST")
            {
                lb_sys.Text = "406.26";
                p_list.Add(new SqlParameter("@SE_ID", "HG00020"));
                DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    lb_energy.Text = dt.Rows[0][0].ToString() ;
                }
            }
        }
        //空壓機總用電量
        public void air(string F, string n)
        {

            SQLDB db = new SQLDB();
            n += "-01";
            string sql = "SELECT ISNULL(SUM(Power_CH_01)+SUM(Power_CH_02)+SUM(Power_CH_03)+SUM(Power_CH_04)+SUM(Power_CH_05),0)" +
                    " FROM G_Air_Comp WHERE FactoryID = @F_ID AND DataDate >= @time_s AND DataDate < @time_e";
            List<SqlParameter> p_list = new List<SqlParameter>();
            p_list.Clear();
            string time_e = Convert.ToDateTime(n).AddMonths(1).ToString("yyyy-MM-dd");
            p_list.Add(new SqlParameter("@F_ID", F));
            p_list.Add(new SqlParameter("@time_s", n));
            p_list.Add(new SqlParameter("@time_e", time_e));
            DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                Double x = Math.Round(Convert.ToDouble(dt.Rows[0][0].ToString()), 0);
                
                lb_air.Text = string.Format("{0:#,0.####}", Convert.ToDecimal(x.ToString())); ;
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tb_SDATE.Attributes.Add("readonly", "readonly");
                string time_s = DateTime.Now.AddDays(-3).ToString("yyyy-MM");
                tb_SDATE.Text = time_s;
                string F = Request.QueryString["F"];
                string name = ff.factory_name(F);
                if (name == "")
                {
                    Response.Redirect("/factory_index/index.aspx?F=KY-T1HIST");
                }
                lb_factory.Text = name;
                //取得工廠機器資料並顯示
                if (Session["p"] == null)
                {
                    data_visible(F,tb_SDATE.Text);
                    TPC_M(F, tb_SDATE.Text);
                    power(F, tb_SDATE.Text);
                    air(F, tb_SDATE.Text);
                }
                else
                {
                    tb_SDATE.Text = Session["p"].ToString();
                    data_visible(F, tb_SDATE.Text);
                    TPC_M(F, tb_SDATE.Text);
                    power(F, tb_SDATE.Text);
                    air(F, tb_SDATE.Text);
                }
                //判斷當前月份 停用/啟動 下一個月的功能
                ff.En_imgb_n(tb_SDATE.Text, imgb_n);

                //手機label加超連結
                hyl_factory.Text = name + " ＞";
                string f = F.Substring(0, 2);
                hyl_factory.NavigateUrl = "../phone/Factory_" + f + ".aspx";

            }
        }

        protected void tb_SDATE_TextChanged(object sender, EventArgs e)
        {
            string F = Request.QueryString["F"];
            string n = tb_SDATE.Text;
            ff.check_ym(n,tb_SDATE);
            data_visible(F,n);
            TPC_M(F, n);
            power(F, n);
            air(F, n);
            Session["p"] = tb_SDATE.Text;
            ff.En_imgb_n(tb_SDATE.Text,imgb_n);
        }

        protected void imgb_p_Click(object sender, ImageClickEventArgs e)
        {
            string F = Request.QueryString["F"];
            string n = tb_SDATE.Text;
            ff.check_ym(n, tb_SDATE);
            string time_s = Convert.ToDateTime(n).AddMonths(-1).ToString("yyyy-MM");
            tb_SDATE.Text = time_s;
            data_visible(F,time_s);
            TPC_M(F, time_s);
            power(F, time_s);
            air(F, time_s);
            Session["p"] = tb_SDATE.Text;
            ff.En_imgb_n(tb_SDATE.Text, imgb_n);
        }

        protected void imgb_n_Click(object sender, ImageClickEventArgs e)
        {
            string F = Request.QueryString["F"];
            string n = tb_SDATE.Text;
            ff.check_ym(n, tb_SDATE);
            string time_s = tb_SDATE.Text;
            time_s = Convert.ToDateTime(time_s).AddMonths(1).ToString("yyyy-MM");
            tb_SDATE.Text = time_s;
            data_visible(F,time_s);
            TPC_M(F, time_s);
            power(F, time_s);
            air(F, time_s);
            Session["p"] = tb_SDATE.Text;
            ff.En_imgb_n(tb_SDATE.Text, imgb_n);

        }

        protected void imgb_pdf_Click(object sender, ImageClickEventArgs e)
        {
            //https://www.cc.ntu.edu.tw/chinese/epaper/0015/20101220_1509.htm
            var doc1 = new Document(PageSize.A4, 50, 50, 80, 50);
            MemoryStream memory = new MemoryStream();
            PdfWriter pdfw = PdfWriter.GetInstance(doc1, memory);

            doc1.Open();
            doc1.Add(new Paragraph(10f, "Hello, 大家好"));
            doc1.Close();

            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + "test" + ".pdf");
            //增加HTTP表頭讓EDGE可以用
            Response.AppendHeader("X-UA-Compatible", "IE=edge,chrome=1");
            Response.OutputStream.Write(memory.GetBuffer(), 0, memory.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.Flush(); //== 不寫這兩段程式，輸出EXCEL檔並開啟後，會出現檔案內容混損，需要修復的字母
            Response.End();


        }
    }
}