using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using factory.lib;
using System.Data;

namespace factory
{
    public partial class Tag_trend_h : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Clear();
                Session["unit"] = "'month'";
                Session["displayFormats"] = "month: 'YYYY-MM'";
                Session["min"] = DateTime.Now.ToString("yyyy-MM");
                Session["max"] = DateTime.Now.AddMonths(1).ToString("yyyy-MM");
                Session["stepSize"] = 0;

                //取得時間
                SQLDB db = new SQLDB();
                string time_s = DateTime.Now.ToString("yyyy-MM-01 00:00:00");
                string time_e = DateTime.Now.AddHours(1).ToString("yyyy-MM-dd HH:00:00");
                string sql = "SELECT DataDateTime FROM Value_Hour WHERE SourceServer like '%KY-T1HIST%' AND DataDateTime >= '" + time_s + "' AND DataDateTime <= '" + time_e + "' GROUP BY DataDateTime";


                DataTable dt = db.GetDataTable(sql,CommandType.Text);
                if (dt.Rows.Count > 0)
                {

                    //取得TagName
                    sql = "SELECT TagName FROM Value_Hour WHERE SourceServer like '%KY-T1HIST%' AND DataDateTime >= '" + time_s + "' AND DataDateTime <= '" + time_e + "' GROUP BY TagName";
                    dt = db.GetDataTable(sql,CommandType.Text);
                    List<string> myLists = new List<string>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        myLists.Add(dt.Rows[i][0].ToString());
                    }
                    System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string namelists = oSerializer.Serialize(myLists);
                    Session["TagName"] = namelists;
                    Session["count_data"] = dt.Rows.Count;

                    //取得data
                    List<List<string>> par_list = new List<List<string>>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sql = "select * from Value_Hour where TagName = '" + dt.Rows[i][0].ToString() + "'AND DataDateTime >= '" + time_s + "' AND DataDateTime <= '" + time_e + "'";
                        DataTable dx = db.GetDataTable(sql,CommandType.Text);

                        string d = "";
                        for (int j = 0; j < dx.Rows.Count; j++)
                        {
                            string datatime = Convert.ToDateTime(dx.Rows[j][0].ToString()).ToString("yyyy-MM-dd HH:mm");
                            d += "{x:" + "'" + datatime + "'" + ",y:" + dx.Rows[j][3].ToString() + "\"},";
                        }
                        par_list.Add(new List<string>() { d });
                    }
                    System.Web.Script.Serialization.JavaScriptSerializer o = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string datas = o.Serialize(par_list);
                    datas = datas.Replace("\\", "");
                    datas = datas.Replace("\"", "");
                    datas = datas.Replace("u0027", "'");
                    
                    Session["datas"] = datas;
                    //Response.Write(datas);
                }
                else
                {
                    lb_err.Visible = true;
                }
            }
            /*
            //被選中的選單會變為灰色
            HyperLink h = (HyperLink)Master.FindControl("Tag_h");
            h.CssClass = "list-group-item list-group-item-action list-group-item-dark collapsed";
            //第一層
            Session["show"] = "1";
            //第二層
            Session["factory"] = "";
            //箭頭向下
            HyperLink x = (HyperLink)Master.FindControl("Tag_acct");
            x.CssClass = "list-group-item list-group-item-action bg-light";
            */
        }

        protected void btn_confrim_Click(object sender, EventArgs e)
        {
            Session["unit"] = "'month'";
            Session["displayFormats"] = "month: 'YYYY-MM'";
            Session["min"] = DateTime.Now.ToString(tb_SDATE.Text);
            Session["max"] = DateTime.Now.ToString(tb_EDATE.Text);
            Session["stepSize"] = 0;

            //取得時間
            SQLDB db = new SQLDB();
            string time_s = DateTime.Now.ToString(tb_SDATE.Text + "-01 00:00:00");
            string time_e = DateTime.Now.ToString(tb_EDATE.Text + "-01 00:00:00");
            time_e = Convert.ToDateTime(time_e).AddMonths(1).AddHours(-1).ToString("yyyy-MM-dd HH:00:00");
            
            string sql = "SELECT DataDateTime FROM Value_Hour WHERE SourceServer like '" + '%' + ddl_fty.SelectedValue + '%' + "' AND DataDateTime >= '" + time_s + "' AND DataDateTime <= '" + time_e + "' GROUP BY DataDateTime";


            DataTable dt = db.GetDataTable(sql,CommandType.Text);
            if (dt.Rows.Count > 0)
            {

                //取得TagName
                sql = "SELECT TagName FROM Value_Hour WHERE SourceServer like '" + '%' + ddl_fty.SelectedValue + '%' + "' AND DataDateTime >= '" + time_s + "' AND DataDateTime <= '" + time_e + "' GROUP BY TagName";
                dt = db.GetDataTable(sql,CommandType.Text);
                List<string> myLists = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    myLists.Add(dt.Rows[i][0].ToString());
                }
                System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string namelists = oSerializer.Serialize(myLists);
                Session["TagName"] = namelists;
                Session["count_data"] = dt.Rows.Count;

                //取得data
                List<List<string>> par_list = new List<List<string>>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sql = "select * from Value_Hour where TagName = '" + dt.Rows[i][0].ToString() + "'AND DataDateTime >= '" + time_s + "' AND DataDateTime <= '" + time_e + "'";
                    DataTable dx = db.GetDataTable(sql,CommandType.Text);

                    string d = "";
                    for (int j = 0; j < dx.Rows.Count; j++)
                    {
                        string datatime = Convert.ToDateTime(dx.Rows[j][0].ToString()).ToString("yyyy-MM-dd HH:mm");
                        d += "{x:" + "'" + datatime + "'" + ",y:" + dx.Rows[j][3].ToString() + "\"},";
                    }
                    par_list.Add(new List<string>() { d });
                }
                System.Web.Script.Serialization.JavaScriptSerializer o = new System.Web.Script.Serialization.JavaScriptSerializer();
                string datas = o.Serialize(par_list);
                datas = datas.Replace("\\", "");
                datas = datas.Replace("\"", "");
                datas = datas.Replace("u0027", "'");

                Session["datas"] = datas;
                //Response.Write(datas);
            }
            else
            {
                lb_err.Visible = true;
            }
        }
    }
}