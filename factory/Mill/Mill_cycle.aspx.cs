using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using factory.lib;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Xml;
using Newtonsoft.Json;

namespace factory.Mill
{
    public partial class Mill_cycle : System.Web.UI.Page
    {

        public f_class ff = new f_class();
        
        public void call_SDS1_CMD(string F, string m, string time_s, string time_e)
        {
            
            SDS1.SelectCommand = "SELECT * FROM　G_Milling_P_Machine WHERE FactoryID = @F AND Mill_ID = @M AND DataTime >= @time_s AND DataTime <= @time_e";
            SDS1.SelectParameters.Clear();
            SDS1.SelectParameters.Add("F", F);
            SDS1.SelectParameters.Add("M", m);
            SDS1.SelectParameters.Add("time_s", time_s);
            SDS1.SelectParameters.Add("time_e", time_e);
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string F = Request.QueryString["F"];
                string M = Request.QueryString["M"];
                string m = ff.get_m(M, F);
                tb_SDATE.Attributes.Add("readonly", "readonly");
                string name = ff.factory_name(F);
                //判斷網址列F
                if (name == "")
                {
                    Response.Redirect("Mill_cycle.aspx?F=KY-T1HIST");
                }
                //M
                if (name == "觀音廠" && M != "1" && M != "3" && M != "5")
                {
                    Response.Redirect("Mill_cycle.aspx?F=" + F + "&M=1");
                }
                else if ((name == "八里廠" || name == "全興廠") && M != "1" && M != "3")
                {
                    Response.Redirect("Mill_cycle.aspx?F=" + F + "&M=1");
                }
                else if (name == "龍德廠" && M != "1" && M != "2")
                {
                    Response.Redirect("Mill_cycle.aspx?F=" + F + "&M=1");
                }
                else if (name == "彰濱廠" && M != "1" && M != "3" && M != "5" && M != "7")
                {
                    Response.Redirect("Mill_cycle.aspx?F=" + F + "&M=1");
                }
                else if (name != "觀音廠" && name != "八里廠" && name != "全興廠" && name != "龍德廠" && name != "彰濱廠" && M != "1")
                {
                    Response.Redirect("Mill_cycle.aspx?F=" + F + "&M=1");
                }

                //判斷時間是否為9點前還是9點後
                DateTime start = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                DateTime end = Convert.ToDateTime("09:00");
                TimeSpan ts = start - end;
                int x = Convert.ToInt32(ts.TotalSeconds);
                string d;
                if (x > 0)
                {
                    d = DateTime.Now.ToString("MM/dd");
                }
                else
                {
                    d = DateTime.Now.AddDays(-1).ToString("MM/dd");
                }

                string time_s = DateTime.Now.ToString("yyyy-") + d.Replace("/", "-");
                tb_SDATE.Text = time_s;

                if (Session["d"] != null)
                {
                    tb_SDATE.Text = Session["d"].ToString();
                }

                lb_factory.Text = name;
                //手機label加超連結
                hyl_factory.Text = name + " ＞";
                string f = F.Substring(0, 2);
                hyl_factory.NavigateUrl = "../phone/Factory_" + f + ".aspx";
                lb_name.Text = "循環提運機" + "#" + M;

                time_s = tb_SDATE.Text + " 08:00";
                string time_e = Convert.ToDateTime(time_s).AddDays(1).ToString("yyyy-MM-dd HH:mm");
                call_SDS1_CMD(F, m, time_s, time_e);
                ff.Enabled(tb_SDATE.Text, imgb_n);
            }

            Session["d"] = tb_SDATE.Text;
        }

        protected void tb_SDATE_TextChanged(object sender, EventArgs e)
        {
            string n = tb_SDATE.Text;
            ff.check_date(n, tb_SDATE);
            string time_s = tb_SDATE.Text + " 08:00";
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            string m = ff.get_m(M, F);
            string time_e = Convert.ToDateTime(time_s).AddDays(1).ToString("yyyy-MM-dd HH:mm");
            call_SDS1_CMD(F, m, time_s, time_e);
            Session["d"] = tb_SDATE.Text;
        }

        protected void imgb_p_Click(object sender, ImageClickEventArgs e)
        {
            string n = tb_SDATE.Text;
            ff.check_date(n, tb_SDATE);
            tb_SDATE.Text = Convert.ToDateTime(tb_SDATE.Text).AddDays(-1).ToString("yyyy-MM-dd");
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            string m = ff.get_m(M, F);
            string time_s = tb_SDATE.Text + " 08:00";
            string time_e = Convert.ToDateTime(time_s).AddDays(1).ToString("yyyy-MM-dd HH:mm");
            call_SDS1_CMD(F, m, time_s, time_e);
            ff.Enabled(tb_SDATE.Text, imgb_n);
            Session["d"] = tb_SDATE.Text;
        }

        protected void imgb_n_Click(object sender, ImageClickEventArgs e)
        {
            string n = tb_SDATE.Text;
            ff.check_date(n, tb_SDATE);
            tb_SDATE.Text = Convert.ToDateTime(tb_SDATE.Text).AddDays(1).ToString("yyyy-MM-dd");
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            string m = ff.get_m(M, F);
            string time_s = tb_SDATE.Text + " 08:00";
            string time_e = Convert.ToDateTime(time_s).AddDays(1).ToString("yyyy-MM-dd HH:mm");
            call_SDS1_CMD(F, m, time_s, time_e);
            ff.Enabled(tb_SDATE.Text, imgb_n);
            Session["d"] = tb_SDATE.Text;
        }

        protected void imgb_excel_Click(object sender, ImageClickEventArgs e)
        {

            if (GV1.Rows.Count > 0)
            {
                string F = Request.QueryString["F"];
                string M = Request.QueryString["M"];
                string m = ff.get_m(M, F);
                string name = "循環提運機" + m + "_" + lb_factory.Text + "_" + tb_SDATE.Text;
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var excel = new ExcelPackage();
                var ws = excel.Workbook.Worksheets.Add(name);
                var workbook = excel.Workbook;

                ws.Cells[1, 1].Value = "時間";
                ws.Cells[1, 1, 3, 1].Merge = true;
                ws.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[1, 2].Value = "功率kw";
                ws.Cells[1, 2, 1, 3].Merge = true;
                ws.Cells[1, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 2].Value = "#1";
                ws.Cells[2, 2, 3, 2].Merge = true;
                ws.Cells[2, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                
                ws.Cells[2, 3].Value = "#2";
                ws.Cells[2, 3, 3, 3].Merge = true;
                ws.Cells[2, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[1, 4].Value = "電流(AMP)";
                ws.Cells[1, 4, 1, 9].Merge = true;
                ws.Cells[1, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 4].Value = "循環提運機";
                ws.Cells[2, 4, 3, 4].Merge = true;
                ws.Cells[2, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                

                ws.Cells[2, 5].Value = "O-SEPA風析機";
                ws.Cells[2, 5, 2, 6].Merge = true;
                ws.Cells[2, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[3, 5].Value = "rpm";
                ws.Cells[3, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[3, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[3, 6].Value = "A";
                ws.Cells[3, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[3, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 7].Value = "收塵排風機";
                ws.Cells[2, 7, 2, 9].Merge = true;
                ws.Cells[2, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[3, 7].Value = "#1M";
                ws.Cells[3, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[3, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[3, 8].Value = "#2M";
                ws.Cells[3, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[3, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[3, 9].Value = "S";
                ws.Cells[3, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[3, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                ws.Cells[1, 10].Value = "出口溫度(℃)";
                ws.Cells[1, 10, 1, 14].Merge = true;
                ws.Cells[1, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[2, 10].Value = "#1磨";
                ws.Cells[2, 10, 2, 11].Merge = true;
                ws.Cells[2, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[3, 10].Value = "氣溫";
                ws.Cells[3, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[3, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[3, 11].Value = "料溫";
                ws.Cells[3, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[3, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 12].Value = "#2磨";
                ws.Cells[2, 12, 2, 13].Merge = true;
                ws.Cells[2, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[3, 12].Value = "氣溫";
                ws.Cells[3, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[3, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[3, 13].Value = "料溫";
                ws.Cells[3, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[3, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 14].Value = "風析機出口";
                ws.Cells[2, 14, 3, 14].Merge = true;
                ws.Cells[2, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[1, 15].Value = "風壓(mmAq)";
                ws.Cells[1, 15, 1, 19].Merge = true;
                ws.Cells[1, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[2, 15].Value = "#1磨出口";
                ws.Cells[2, 15, 3, 15].Merge = true;
                ws.Cells[2, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 16].Value = "#2磨出口";
                ws.Cells[2, 16, 3, 16].Merge = true;
                ws.Cells[2, 16].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 17].Value = "風析機出口";
                ws.Cells[2, 17, 3, 17].Merge = true;
                ws.Cells[2, 17].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 18].Value = "S系收塵排風機";
                ws.Cells[2, 18, 2, 19].Merge = true;
                ws.Cells[2, 18].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[3, 18].Value = "入口";
                ws.Cells[3, 18].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[3, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[3, 19].Value = "差壓";
                ws.Cells[3, 19].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[3, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[1, 20].Value = "開度(% or rpm)";
                ws.Cells[1, 20, 1, 22].Merge = true;
                ws.Cells[1, 20].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 20].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[2, 20].Value = "收塵排風機";
                ws.Cells[2, 20, 2, 22].Merge = true;
                ws.Cells[2, 20].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 20].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[3, 20].Value = "#1M";
                ws.Cells[3, 20].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[3, 20].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[3, 21].Value = "#2M";
                ws.Cells[3, 21].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[3, 21].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[3, 22].Value = "S";
                ws.Cells[3, 22].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[3, 22].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[1, 23].Value = "#1磨機飼料量";
                ws.Cells[1, 23, 1, 25].Merge = true;
                ws.Cells[1, 23].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 23].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[2, 23].Value = "熟料";
                ws.Cells[2, 23].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 23].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 24].Value = "石膏";
                ws.Cells[2, 24].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 24].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 25].Value = "產量";
                ws.Cells[2, 25].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 25].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[1, 26].Value = "#2磨機飼料量";
                ws.Cells[1, 26, 1, 28].Merge = true;
                ws.Cells[1, 26].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 26].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[2, 26].Value = "熟料";
                ws.Cells[2, 26].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 26].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 27].Value = "石膏";
                ws.Cells[2, 27].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 27].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 28].Value = "產量";
                ws.Cells[2, 28].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 28].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                for (int i = 0; i < 6; i++)
                {
                    ws.Cells[3, 23 + i].Value = "T/H";
                    ws.Cells[3, 23 + i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ws.Cells[3, 23 + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                //調整欄寬
                ws.Column(4).Width = 11;
                ws.Column(14).Width = 11;
                ws.Column(15).Width = 11;
                ws.Column(16).Width = 11;
                ws.Column(17).Width = 11;

                ws.Cells[1, 1, 100, 100].Style.Font.Name = "微軟正黑體";

                List<string> lb_name = new List<string>();
                for (int i = 0; i < 25; i++)
                {
                    lb_name.Add("lb_" + i.ToString());
                }
                lb_name.Insert(23, "lb_Yield1");
                lb_name.Add("lb_Yield2");
                

                //資料的部分
                for (int i = 0; i < GV1.Rows.Count; i++)
                {
                    for (int j = 0; j < GV1.Columns.Count; j++)
                    {
                        //除了時間其他都是樣板
                        if (j == 0)
                        {
                            ws.Cells[i + 4, j + 1].Value = Convert.ToDecimal(GV1.Rows[i].Cells[j].Text);
                        }
                        else
                        {
                            Label v = (Label)GV1.Rows[i].FindControl(lb_name[j-1]);
                            ws.Cells[i + 4, j + 1].Value = Convert.ToDecimal(v.Text);   
                        }
                    }
                }



                //== 輸出Excel 2007檔案
                MemoryStream MS = new MemoryStream(); //需要System.IO命名空間
                excel.SaveAs(MS);
                Response.AddHeader("Content-Disposition", "attachment;filename=" + name + ".xlsx");
                //增加HTTP表頭讓EDGE可以用
                Response.AppendHeader("X-UA-Compatible", "IE=edge,chrome=1");
                Response.BinaryWrite(MS.ToArray());
                MS.Close();
                MS.Dispose();
                Response.Flush(); //== 不寫這兩段程式，輸出EXCEL檔並開啟後，會出現檔案內容混損，需要修復的字母
                Response.End();

            }

        }

        protected void btn_chart1_onClick(object sender, EventArgs e)
        {
            string F = Request.QueryString["F"];
            string v = "";
            string n = "";
            for (int i = 0; i < 25; i++)
            {
                CheckBox cb = (CheckBox)GV1.HeaderRow.FindControl("cb_"+i.ToString());
                if (cb.Checked == true)
                {
                    if (i < 23)
                    {
                        if (GV1.HeaderRow.Cells[i + 1].ToolTip.ToString().Length > 0)
                        {
                            v += GV1.HeaderRow.Cells[i + 1].ToolTip + ",";
                            n += GV1.Columns[i + 1].HeaderText.Replace("#", "").Replace("<br>", "") + ",";
                        }
                    }
                    else
                    {
                        if (GV1.HeaderRow.Cells[i + 2].ToolTip.ToString().Length > 0)
                        {
                            v += GV1.HeaderRow.Cells[i + 2].ToolTip + ",";
                            n += GV1.Columns[i + 2].HeaderText.Replace("#", "").Replace("<br>", "") + ",";
                        }
                    }
                }
            }

            if (v.Length > 0)
            {
                v = v.Remove(v.LastIndexOf(","), 1);
                n = n.Remove(n.LastIndexOf(","), 1);
                Response.Redirect("/Tag/Tag_trend.aspx?F=" + F + "&M=A&V=" + v + "&N=" + n + "");
            }
            else
            {
                lb_error.Visible = true;
                
            }

        }

        protected void GV1_DataBound(object sender, EventArgs e)
        {
            if (GV1.Rows.Count > 0)
            {
                for (int i = 0; i < GV1.Rows.Count; i++)
                {
                    //計算#1 #2 產量
                    Label lb21 = (Label)GV1.Rows[i].FindControl("lb_21");
                    Label lb22 = (Label)GV1.Rows[i].FindControl("lb_22");
                    Label lb23 = (Label)GV1.Rows[i].FindControl("lb_23");
                    Label lb24 = (Label)GV1.Rows[i].FindControl("lb_24");
                    Label lb_Yield1 = (Label)GV1.Rows[i].FindControl("lb_Yield1");
                    Label lb_Yield2 = (Label)GV1.Rows[i].FindControl("lb_Yield2");

                    lb_Yield1.Text = (Convert.ToDecimal(lb21.Text) + Convert.ToDecimal(lb22.Text)).ToString();
                    lb_Yield2.Text = (Convert.ToDecimal(lb23.Text) + Convert.ToDecimal(lb24.Text)).ToString();
                }
                //tooltip
                string F = Request.QueryString["F"];
                string M = Request.QueryString["M"];
                string m = ff.get_m(M, F);
                List<SqlParameter> p_list = new List<SqlParameter>();
                p_list.Add(new SqlParameter("@F", F));
                p_list.Add(new SqlParameter("@M", m));
                string sql = "SELECT * FROM G_Milling_P_Map WHERE FactoryID = @F AND Mill_ID = @M";
                SQLDB db = new SQLDB();
                DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    //顯示該機器的名稱
                    for (int i = 0; i < 25; i++)
                    {
                        //跳過#1產量
                        if (i < 23)
                        {
                            GV1.HeaderRow.Cells[1 + i].ToolTip = dt.Rows[0][2 + i].ToString();
                        }
                        else
                        {
                            GV1.HeaderRow.Cells[2 + i].ToolTip = dt.Rows[0][2 + i].ToString();
                        }

                    }
                }
            }

        }

        public string cb_name()
        {
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            string m = ff.get_m(M, F);
            List<SqlParameter> p_list = new List<SqlParameter>();
            p_list.Add(new SqlParameter("@F", F));
            p_list.Add(new SqlParameter("@M", m));
            string sql = "SELECT * FROM G_Milling_P_Map WHERE FactoryID = @F AND Mill_ID = @M";
            SQLDB db = new SQLDB();
            DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
            string d = "";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count-2; i++)
                {
                    string v = dt.Rows[0][i+2].ToString();
                    d += v+",";
                }
                d = d.Remove(d.LastIndexOf(","), 1);
                System.Web.Script.Serialization.JavaScriptSerializer o = new System.Web.Script.Serialization.JavaScriptSerializer();
                string datas = o.Serialize(d);
                return datas;
            }
            else
            {
                return "1";
            }

        }

    }
}