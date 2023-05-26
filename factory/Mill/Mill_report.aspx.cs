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


namespace factory.Mill
{

    public partial class Mill_report : System.Web.UI.Page
    {
        public f_class ff = new f_class();

        public void call_SDS1_CMD(string F, string m, string time_s, string time_e)
        {
            SDS1.SelectCommand = "SELECT * FROM　G_Milling_Machine WHERE FactoryID = @F AND Mill_ID = @M AND DataTime >= @time_s AND DataTime <= @time_e";

            SDS1.SelectParameters.Clear();
            SDS1.SelectParameters.Add("F", F);
            SDS1.SelectParameters.Add("M", m);
            SDS1.SelectParameters.Add("time_s", time_s);
            SDS1.SelectParameters.Add("time_e", time_e);
        }

        public void call_SDS2_CMD(string F, string m, string time_s)
        {
            SDS2.SelectCommand = "SELECT Q.DateTime, Q.Mill_ID, P.ProductName, Q.Moisture, Q.Specific_Surface, Q.Residual_On_Sieve From A_Product_Quality as Q " +
            "LEFT JOIN A_Product AS P ON Q.ProductID = P.ProductID " +
            "WHERE DTime >= @D1 AND DTime < @D2 AND FactoryID = @F AND Mill_ID = @M AND Visible IS NOT NULL";
            SDS2.SelectParameters.Clear();
            SDS2.SelectParameters.Add("F", F);
            SDS2.SelectParameters.Add("D1", tb_SDATE.Text);
            SDS2.SelectParameters.Add("D2", time_s);
            SDS2.SelectParameters.Add("M", m);
        }
        public void excel_text_vertical(ExcelWorkbook workbook, ExcelRange ws)
        {
            var x = ws;
            x.Style.TextRotation = 180;
            workbook.Styles.UpdateXml();
            workbook.Styles.CellXfs[ws.StyleID].TextRotation = 255;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            string B = Request.QueryString["B"];
            string m = ff.get_m(M, F);
            if (!IsPostBack)
            {
                //紀錄checkbox
                if (Session["box"] == null)
                {
                    List<int> t = new List<int>();
                    for (int i = 0; i < 60;i++)
                    {
                        t.Add(0);
                    }
                    Session["box"] = t; 
                }

                tb_SDATE.Attributes.Add("readonly", "readonly");
                string name = ff.factory_name(F);
                //判斷網址列F
                if (name == "")
                {
                    Response.Redirect("Mill_report.aspx?F=KY-T1HIST");
                }

                //M
                if (name == "觀音廠" && M != "1" && M != "3" && M != "5")
                {
                    Response.Redirect("Mill_report.aspx?F=" + F + "&M=1&B=Power");
                }
                else if ((name == "八里廠" || name == "全興廠") && M != "1" && M != "3")
                {
                    Response.Redirect("Mill_report.aspx?F=" + F + "&M=1&B=Power");
                }
                else if (name == "龍德廠" && M != "1" && M != "2")
                {
                    Response.Redirect("Mill_report.aspx?F=" + F + "&M=1&B=Power");
                }
                else if (name == "彰濱廠" && M != "1" && M != "3" && M != "5" && M != "7")
                {
                    Response.Redirect("Mill_report.aspx?F=" + F + "&M=1&B=Power");
                }
                else if (name != "觀音廠" && name != "八里廠" && name != "全興廠" && name != "龍德廠" && name != "彰濱廠" && M != "1")
                {
                    Response.Redirect("Mill_report.aspx?F=" + F + "&M=1&B=Power");
                }
                //B
                if (B != "Power" && B != "Temp" && B != "Wind" && B != "Fd" && B != "Quality")
                {
                    Response.Redirect("Mill_report.aspx?F=" + F + "&M=" + M + "&B=Power");
                }

                //給頁籤URL

                hyl_Power.NavigateUrl = "Mill_report.aspx?F=" + F + "&M=" + M + "&B=Power";
                hyl_Temp.NavigateUrl = "Mill_report.aspx?F=" + F + "&M=" + M + "&B=Temp";
                hyl_Wind.NavigateUrl = "Mill_report.aspx?F=" + F + "&M=" + M + "&B=Wind";
                hyl_Fd.NavigateUrl = "Mill_report.aspx?F=" + F + "&M=" + M + "&B=Fd";
                hyl_Quality.NavigateUrl = "Mill_report.aspx?F=" + F + "&M=" + M + "&B=Quality";


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

                lb_factory.Text = name;

                //手機label加超連結
                hyl_factory.Text = name + " ＞";
                string f = F.Substring(0, 2);
                hyl_factory.NavigateUrl = "../phone/Factory_" + f + ".aspx";
                lb_name.Text = "磨機日報" + "#" + M;

                string time_s = DateTime.Now.ToString("yyyy-") + d.Replace("/", "-");
                tb_SDATE.Text = time_s;

                if (Session["d"] != null)
                {
                    tb_SDATE.Text = Session["d"].ToString();
                }

                time_s = tb_SDATE.Text + " 08:00";
                string time_e = Convert.ToDateTime(time_s).AddDays(1).ToString("yyyy-MM-dd HH:mm");

                call_SDS1_CMD(F, m, time_s, time_e);


                ff.Enabled(tb_SDATE.Text, imgb_n);


                G2.Visible = false; //隱藏成品品質
                time_s = Convert.ToDateTime(tb_SDATE.Text).AddDays(1).ToString("yyyy-MM-dd");
                call_SDS2_CMD(F, m, time_s);
                
                if (B == "Quality") //顯示成品品質
                {
                    G1.Visible = false;
                    G2.Visible = true;
                    btn_chart.Visible = false;
                    /*
                    SDS2.SelectCommand = "SELECT Q.DateTime, Q.Mill_ID, P.ProductName, Q.Moisture, Q.Specific_Surface, Q.Residual_On_Sieve From (Select MAX(Dtime)AS sn FROM A_Product_Quality GROUP BY LEFT(Convert(Varchar,Dtime,120),13),Mill_ID) AS A " +
                        "LEFT JOIN A_Product_Quality as Q on A.sn = Q.Dtime " +
                        "LEFT JOIN A_Product AS P ON Q.ProductID = P.ProductID " +
                        "WHERE DTime >= @D1 AND DTime < @D2 AND FactoryID = @F AND Mill_ID = @M";
                    */
                }
            }
            //tootip每次postback的時候都會產生一次 否則換頁後會消失
            List<SqlParameter> p_list = new List<SqlParameter>();
            p_list.Add(new SqlParameter("@F", F));
            p_list.Add(new SqlParameter("@M", m));
            string sql = "SELECT * FROM G_Milling_Machine_Mapping WHERE FactoryID = @F AND Mill_ID = @M";
            SQLDB db = new SQLDB();
            DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                
                try
                {
                    //顯示該機器的名稱
                    for (int i = 0; i < 60; i++)
                    {
                        GV1.HeaderRow.Cells[5 + i].ToolTip = dt.Rows[0][2 + i].ToString();
                    }
                }
                catch
                {
                    
                }
            }
            //傳值給下一個網頁
            Session["d"] = tb_SDATE.Text;
        }

        protected void tb_SDATE_TextChanged(object sender, EventArgs e)
        {
            string n = tb_SDATE.Text;
            ff.check_date(n, tb_SDATE);
            string B = Request.QueryString["B"];
            string time_s = tb_SDATE.Text + " 08:00";
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            string m = ff.get_m(M, F);
            if (B == "Quality")
            {
                G1.Visible = false;
                G2.Visible = true;
                time_s = Convert.ToDateTime(tb_SDATE.Text).AddDays(1).ToString("yyyy-MM-dd");

                call_SDS2_CMD(F, m, time_s);
            }
            else
            {
                string time_e = Convert.ToDateTime(time_s).AddDays(1).ToString("yyyy-MM-dd HH:mm");
                call_SDS1_CMD(F, m, time_s, time_e);
            }
            ff.Enabled(tb_SDATE.Text, imgb_n);
            Session["d"] = tb_SDATE.Text;
            lb_error.Visible = false;
        }

        protected void imgb_p_Click(object sender, ImageClickEventArgs e)
        {
            string n = tb_SDATE.Text;
            ff.check_date(n, tb_SDATE);
            tb_SDATE.Text = Convert.ToDateTime(tb_SDATE.Text).AddDays(-1).ToString("yyyy-MM-dd");
            string B = Request.QueryString["B"];
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            string m = ff.get_m(M, F);
            string time_s;
            if (B == "Quality")
            {
                G1.Visible = false;
                SDS1.SelectCommand = "";

                G2.Visible = true;
                time_s = Convert.ToDateTime(tb_SDATE.Text).AddDays(1).ToString("yyyy-MM-dd");
                call_SDS2_CMD(F, m, time_s);
            }
            else
            {
                time_s = Convert.ToDateTime(tb_SDATE.Text).ToString("yyyy-MM-dd 08:00");
                string time_e = Convert.ToDateTime(time_s).AddDays(1).ToString("yyyy-MM-dd 08:00");
                //SDS1.SelectCommand = "SELECT * FROM　G_Milling_Machine WHERE FactoryID = '" + F + "' AND Mill_ID = '" + m + "'  AND DataTime >= '" + time_s + "' AND DataTime <= '" + time_e + "'";
                call_SDS1_CMD(F, m, time_s, time_e);
            }
            ff.Enabled(tb_SDATE.Text, imgb_n);
            Session["d"] = tb_SDATE.Text;

            lb_error.Visible = false;
        }

        protected void imgb_n_Click(object sender, ImageClickEventArgs e)
        {
            string n = tb_SDATE.Text;
            ff.check_date(n, tb_SDATE);
            string F = Request.QueryString["F"];
            n = Convert.ToDateTime(tb_SDATE.Text).AddDays(1).ToString("yyyy-MM-dd");
            string M = Request.QueryString["M"];
            string B = Request.QueryString["B"];
            string m = ff.get_m(M, F);
            string time_s;
            tb_SDATE.Text = n;
            if (B == "Quality")
            {
                G1.Visible = false;
                SDS1.SelectCommand = "";
                G2.Visible = true;
                time_s = Convert.ToDateTime(tb_SDATE.Text).AddDays(1).ToString("yyyy-MM-dd");
                call_SDS2_CMD(F, m, time_s);
            }
            else
            {
                time_s = n + " 08:00";
                string time_e = Convert.ToDateTime(n).AddDays(1).ToString("yyyy-MM-dd 08:00");
                call_SDS1_CMD(F, m, time_s, time_e);
            }
            Session["d"] = n;
            ff.Enabled(tb_SDATE.Text, imgb_n);
            lb_error.Visible = false;
        }

        protected void imgb_excel_Click(object sender, ImageClickEventArgs e)
        {
            //檢查日期
            string n = tb_SDATE.Text;
            ff.check_date(n, tb_SDATE);
            //匯出EXCEL
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            string m = ff.get_m(M, F);
            string time_s = tb_SDATE.Text + " 08:00";
            string time_e = Convert.ToDateTime(time_s).AddDays(1).ToString("yyyy-MM-dd HH:mm");

            call_SDS1_CMD(F, m, time_s, time_e);
            if (GV1.Rows.Count > 0)
            {
                string name = "磨機日報" + m + "_" + lb_factory.Text + "_" + tb_SDATE.Text;
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var excel = new ExcelPackage();
                var ws = excel.Workbook.Worksheets.Add(name);
                var workbook = excel.Workbook;
                /*
                for (int i = 0; i < GV1.Columns.Count-4; i++)
                {
                    ws.Cells[1, i + 1].Value = GV1.Columns[i+4].HeaderText.Replace("<br>","");
                    ws.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }
                */
                ws.Cells[1, 1].Value = "時間";
                ws.Cells[1, 1, 10, 1].Merge = true;
                ws.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[1, 1]);


                //
                ws.Cells[1, 2].Value = "馬達";
                ws.Cells[1, 2, 1, 5].Merge = true;
                ws.Cells[1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                ws.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[2, 2].Value = "電流";
                ws.Cells[2, 2, 5, 3].Merge = true;
                ws.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[2, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                ws.Cells[2, 4].Value = "功率";
                ws.Cells[2, 4, 5, 4].Merge = true;
                ws.Cells[2, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[2, 4]);


                ws.Cells[2, 5].Value = "功率";
                excel_text_vertical(workbook, ws.Cells[2, 5]);
                ws.Cells[2, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[2, 5, 5, 5].Merge = true;

                ws.Cells[6, 2].Value = "#1";
                ws.Cells[6, 2, 9, 2].Merge = true;
                ws.Cells[6, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[6, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells[6, 3].Value = "#2";
                ws.Cells[6, 3, 9, 3].Merge = true;
                ws.Cells[6, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[6, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells[6, 4].Value = "#1";
                ws.Cells[6, 4, 9, 4].Merge = true;
                ws.Cells[6, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[6, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells[6, 5].Value = "#2";
                ws.Cells[6, 5, 9, 5].Merge = true;
                ws.Cells[6, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[6, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                ws.Cells[10, 2].Value = "A";
                ws.Cells[10, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[10, 3].Value = "A";
                ws.Cells[10, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[10, 4].Value = "KW";
                ws.Cells[10, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[10, 5].Value = "KW";
                ws.Cells[10, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //
                ws.Cells[1, 6].Value = "電流(AMP)";
                ws.Cells[1, 6, 1, 12].Merge = true;
                ws.Cells[1, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 6].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                ws.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 6].Value = "循環提運機";
                ws.Cells[2, 6, 10, 6].Merge = true;
                ws.Cells[2, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[2, 6]);

                ws.Cells[2, 7].Value = "入庫提運機";
                ws.Cells[2, 7, 10, 7].Merge = true;
                ws.Cells[2, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[2, 7]);

                ws.Cells[2, 8].Value = "O-SEPA";
                ws.Cells[2, 8, 5, 9].Merge = true;
                ws.Cells[2, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[2, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                ws.Cells[2, 10].Value = "收塵排風機";
                ws.Cells[2, 10, 5, 12].Merge = true;
                ws.Cells[2, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[2, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                ws.Cells[6, 8].Value = "A";
                ws.Cells[6, 8, 10, 8].Merge = true;
                ws.Cells[6, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[6, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                ws.Cells[6, 9].Value = "RPM";
                ws.Cells[6, 9, 10, 9].Merge = true;
                ws.Cells[6, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[6, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                ws.Cells[6, 10].Value = "M";
                ws.Cells[6, 10, 9, 10].Merge = true;
                ws.Cells[6, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[6, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                ws.Cells[6, 11].Value = "M";
                ws.Cells[6, 11, 9, 11].Merge = true;
                ws.Cells[6, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[6, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                ws.Cells[10, 10].Value = "#1";
                ws.Cells[10, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[10, 11].Value = "#2";
                ws.Cells[10, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[6, 12].Value = "S";
                ws.Cells[6, 12, 10, 12].Merge = true;
                ws.Cells[6, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[6, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;




                //
                ws.Cells[1, 13].Value = "溫度℃";
                ws.Cells[1, 13, 1, 26].Merge = true;
                ws.Cells[1, 13].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 13].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                ws.Cells[1, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 13].Value = "耳軸承";
                ws.Cells[2, 13, 2, 16].Merge = true;
                ws.Cells[2, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 17].Value = "潤滑油";
                ws.Cells[2, 17, 2, 20].Merge = true;
                ws.Cells[2, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[2, 21].Value = "磨機";
                ws.Cells[2, 21, 2, 22].Merge = true;
                ws.Cells[2, 21].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[2, 23].Value = "磨機";
                ws.Cells[2, 23, 2, 24].Merge = true;
                ws.Cells[2, 23].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[3, 13].Value = "#1Mill";
                ws.Cells[3, 13, 4, 14].Merge = true;
                ws.Cells[3, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[3, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells[3, 15].Value = "#2Mill";
                ws.Cells[3, 15, 4, 16].Merge = true;
                ws.Cells[3, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[3, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Center;



                ws.Cells[3, 17].Value = "#1Mill";
                ws.Cells[3, 17, 4, 18].Merge = true;
                ws.Cells[3, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[3, 17].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                ws.Cells[3, 19].Value = "#2Mill";
                ws.Cells[3, 19, 4, 20].Merge = true;
                ws.Cells[3, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[3, 19].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                ws.Cells[3, 21].Value = "#1Mill";
                ws.Cells[3, 21, 4, 22].Merge = true;
                ws.Cells[3, 21].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[3, 21].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                ws.Cells[3, 23].Value = "#2Mill";
                ws.Cells[3, 23, 4, 24].Merge = true;
                ws.Cells[3, 23].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[3, 23].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                ws.Cells[5, 13].Value = "入口端";
                ws.Cells[5, 13, 10, 13].Merge = true;
                ws.Cells[5, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[5, 13]);

                ws.Cells[5, 14].Value = "出口端";
                ws.Cells[5, 14, 10, 14].Merge = true;
                ws.Cells[5, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[5, 14]);

                ws.Cells[5, 15].Value = "入口端";
                ws.Cells[5, 15, 10, 15].Merge = true;
                ws.Cells[5, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[5, 15]);

                ws.Cells[5, 16].Value = "出口端";
                ws.Cells[5, 16, 10, 16].Merge = true;
                ws.Cells[5, 16].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[5, 16]);

                ws.Cells[5, 17].Value = "入口端";
                ws.Cells[5, 17, 10, 17].Merge = true;
                ws.Cells[5, 17].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[5, 17]);

                ws.Cells[5, 18].Value = "出口端";
                ws.Cells[5, 18, 10, 18].Merge = true;
                ws.Cells[5, 18].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[5, 18]);

                ws.Cells[5, 19].Value = "入口端";
                ws.Cells[5, 19, 10, 19].Merge = true;
                ws.Cells[5, 19].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[5, 19]);

                ws.Cells[5, 20].Value = "出口端";
                ws.Cells[5, 20, 10, 20].Merge = true;
                ws.Cells[5, 20].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[5, 20]);

                ws.Cells[5, 21].Value = "料溫";
                ws.Cells[5, 21, 10, 21].Merge = true;
                ws.Cells[5, 21].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[5, 21]);

                ws.Cells[5, 22].Value = "氣溫";
                ws.Cells[5, 22, 10, 22].Merge = true;
                ws.Cells[5, 22].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[5, 22]);

                ws.Cells[5, 23].Value = "料溫";
                ws.Cells[5, 23, 10, 23].Merge = true;
                ws.Cells[5, 23].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[5, 23]);

                ws.Cells[5, 24].Value = "氣溫";
                ws.Cells[5, 24, 10, 24].Merge = true;
                ws.Cells[5, 24].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[5, 24]);

                ws.Cells[2, 25].Value = "風析機出口";
                ws.Cells[2, 25, 10, 25].Merge = true;
                ws.Cells[2, 25].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[2, 25]);

                ws.Cells[2, 26].Value = "成品入庫料溫";
                ws.Cells[2, 26, 10, 26].Merge = true;
                ws.Cells[2, 26].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[2, 26]);


                //
                ws.Cells[1, 27].Value = "磨機馬達及軸承溫度℃";
                ws.Cells[1, 27, 1, 38].Merge = true;
                ws.Cells[1, 27].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 27].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                ws.Cells[1, 27].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[2, 28].Value = "#1Mill";
                ws.Cells[2, 28, 2, 32].Merge = true;
                ws.Cells[2, 28].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[2, 33].Value = "#2Mill";
                ws.Cells[2, 33, 2, 38].Merge = true;
                ws.Cells[2, 33].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[3, 27].Value = "馬達線圈R";
                ws.Cells[3, 27, 10, 27].Merge = true;
                ws.Cells[3, 27].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 27]);


                ws.Cells[3, 28].Value = "馬達線圈S";
                ws.Cells[3, 28, 10, 28].Merge = true;
                ws.Cells[3, 28].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 28]);


                ws.Cells[3, 29].Value = "馬達線圈T";
                ws.Cells[3, 29, 10, 29].Merge = true;
                ws.Cells[3, 29].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 29]);


                ws.Cells[3, 30].Value = "負載端軸承";
                ws.Cells[3, 30, 10, 30].Merge = true;
                ws.Cells[3, 30].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 30]);


                ws.Cells[3, 31].Value = "無負載端軸承";
                ws.Cells[3, 31, 10, 31].Merge = true;
                ws.Cells[3, 31].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 31]);

                ws.Cells[3, 32].Value = "主減速機供油溫度";
                ws.Cells[3, 32, 10, 32].Merge = true;
                ws.Cells[3, 32].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 32]);

                ws.Cells[3, 33].Value = "馬達線圈R";
                ws.Cells[3, 33, 10, 33].Merge = true;
                ws.Cells[3, 33].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 33]);

                ws.Cells[3, 34].Value = "馬達線圈S";
                ws.Cells[3, 34, 10, 34].Merge = true;
                ws.Cells[3, 34].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 34]);


                ws.Cells[3, 35].Value = "馬達線圈T";
                ws.Cells[3, 35, 10, 35].Merge = true;
                ws.Cells[3, 35].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 35]);


                ws.Cells[3, 36].Value = "負載端軸承";
                ws.Cells[3, 36, 10, 36].Merge = true;
                ws.Cells[3, 36].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 36]);

                ws.Cells[3, 37].Value = "無負載端軸承";
                ws.Cells[3, 37, 10, 37].Merge = true;
                ws.Cells[3, 37].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 37]);


                ws.Cells[3, 38].Value = "主減速機供油溫度";
                ws.Cells[3, 38, 10, 38].Merge = true;
                ws.Cells[3, 38].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 38]);


                //
                ws.Cells[1, 39].Value = "風壓(MMWG)";
                ws.Cells[1, 39, 1, 43].Merge = true;
                ws.Cells[1, 39].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 39].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                ws.Cells[1, 39].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 39].Value = "磨機出口";
                ws.Cells[2, 39, 9, 40].Merge = true;
                ws.Cells[2, 39].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[2, 39]);


                ws.Cells[10, 39].Value = "#1";
                ws.Cells[10, 39].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[10, 40].Value = "#2";
                ws.Cells[10, 40].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[2, 41].Value = "風析機出口";
                ws.Cells[2, 41, 10, 41].Merge = true;
                ws.Cells[2, 41].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[2, 41]);

                ws.Cells[2, 42].Value = "S系排風機";
                ws.Cells[2, 42, 4, 43].Merge = true;
                ws.Cells[2, 42].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[2, 42].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                ws.Cells[5, 42].Value = "入口";
                ws.Cells[5, 42, 10, 42].Merge = true;
                ws.Cells[5, 42].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[5, 42].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[5, 43].Value = "壓差";
                ws.Cells[5, 43, 10, 43].Merge = true;
                ws.Cells[5, 43].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[5, 43].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                //
                ws.Cells[1, 44].Value = "轉速(RPM)";
                ws.Cells[1, 44, 1, 49].Merge = true;
                ws.Cells[1, 44].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 44].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                ws.Cells[1, 44].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 44].Value = "收塵排風機";
                ws.Cells[2, 44, 4, 46].Merge = true;
                ws.Cells[2, 44].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[2, 44].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                ws.Cells[5, 44].Value = "M系";
                ws.Cells[5, 44, 9, 44].Merge = true;
                ws.Cells[5, 44].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[5, 44]);

                ws.Cells[5, 45].Value = "M系";
                ws.Cells[5, 45, 9, 45].Merge = true;
                ws.Cells[5, 45].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[5, 45]);

                ws.Cells[5, 46].Value = "S系";
                ws.Cells[5, 46, 10, 46].Merge = true;
                ws.Cells[5, 46].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[5, 46].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells[10, 44].Value = "#1";
                ws.Cells[10, 44].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[10, 45].Value = "#2";
                ws.Cells[10, 45].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[2, 47].Value = "收塵排風機";
                ws.Cells[2, 47, 4, 49].Merge = true;
                ws.Cells[2, 47].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[2, 47].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                ws.Cells[5, 47].Value = "M系";
                ws.Cells[5, 47, 9, 47].Merge = true;
                ws.Cells[5, 47].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[5, 47]);

                ws.Cells[5, 48].Value = "M系";
                ws.Cells[5, 48, 9, 48].Merge = true;
                ws.Cells[5, 48].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[5, 48]);

                ws.Cells[5, 49].Value = "S系";
                ws.Cells[5, 49, 10, 49].Merge = true;
                ws.Cells[5, 49].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[5, 49].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells[10, 47].Value = "#1";
                ws.Cells[10, 47].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[10, 48].Value = "#2";
                ws.Cells[10, 48].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                //
                ws.Cells[1, 50].Value = "#1秤飼機";
                ws.Cells[1, 50, 1, 55].Merge = true;
                ws.Cells[1, 50].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 50].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                ws.Cells[1, 50].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 50].Value = "熟料/爐石";
                ws.Cells[2, 50, 2, 52].Merge = true;
                ws.Cells[2, 50].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[3, 50].Value = "飼量調節器";
                ws.Cells[3, 50, 10, 50].Merge = true;
                ws.Cells[3, 50].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 50]);


                ws.Cells[3, 51].Value = "積數器";
                ws.Cells[3, 51, 9, 51].Merge = true;
                ws.Cells[3, 51].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 51]);


                ws.Cells[3, 52].Value = "飼料量";
                ws.Cells[3, 52, 9, 52].Merge = true;
                ws.Cells[3, 52].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 52]);


                ws.Cells[10, 51].Value = "(T/H)";
                ws.Cells[10, 51, 10, 52].Merge = true;
                ws.Cells[10, 51].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[2, 53].Value = "石膏";
                ws.Cells[2, 53, 2, 55].Merge = true;
                ws.Cells[2, 53].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[3, 53].Value = "飼量調節器";
                ws.Cells[3, 53, 10, 53].Merge = true;
                ws.Cells[3, 53].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 53]);

                ws.Cells[3, 54].Value = "積數器";
                ws.Cells[3, 54, 9, 54].Merge = true;
                ws.Cells[3, 54].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 54]);

                ws.Cells[3, 55].Value = "飼料量";
                ws.Cells[3, 55, 9, 55].Merge = true;
                ws.Cells[3, 55].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 55]);

                ws.Cells[10, 54].Value = "(T/H)";
                ws.Cells[10, 54, 10, 55].Merge = true;
                ws.Cells[10, 54].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[1, 56].Value = "添加劑飼料量";
                ws.Cells[1, 56, 9, 56].Merge = true;
                ws.Cells[1, 56].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[1, 56]);
                ws.Cells[10, 56].Value = "RPM";
                ws.Cells[10, 56].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[1, 57].Value = "產量";
                ws.Cells[1, 57, 9, 57].Merge = true;
                ws.Cells[1, 57].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[1, 57]);
                ws.Cells[10, 57].Value = "T/H";
                ws.Cells[10, 57].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                //
                ws.Cells[1, 58].Value = "#2秤飼機";
                ws.Cells[1, 58, 1, 63].Merge = true;
                ws.Cells[1, 58].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 58].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                ws.Cells[1, 58].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 58].Value = "熟料/爐石";
                ws.Cells[2, 58, 2, 60].Merge = true;
                ws.Cells[2, 58].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[3, 58].Value = "飼量調節器";
                ws.Cells[3, 58, 10, 58].Merge = true;
                ws.Cells[3, 58].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 58]);


                ws.Cells[3, 59].Value = "積數器";
                ws.Cells[3, 59, 9, 59].Merge = true;
                ws.Cells[3, 59].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 59]);


                ws.Cells[3, 60].Value = "飼料量";
                ws.Cells[3, 60, 9, 60].Merge = true;
                ws.Cells[3, 60].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 60]);


                ws.Cells[10, 59].Value = "(T/H)";
                ws.Cells[10, 59, 10, 60].Merge = true;
                ws.Cells[10, 59].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[2, 61].Value = "石膏";
                ws.Cells[2, 61, 2, 63].Merge = true;
                ws.Cells[2, 61].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[3, 61].Value = "飼量調節器";
                ws.Cells[3, 61, 10, 61].Merge = true;
                ws.Cells[3, 61].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 61]);


                ws.Cells[3, 62].Value = "積數器";
                ws.Cells[3, 62, 9, 62].Merge = true;
                ws.Cells[3, 62].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 62]);


                ws.Cells[3, 63].Value = "飼料量";
                ws.Cells[3, 63, 9, 63].Merge = true;
                ws.Cells[3, 63].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[3, 63]);


                ws.Cells[10, 62].Value = "(T/H)";
                ws.Cells[10, 62, 10, 63].Merge = true;
                ws.Cells[10, 62].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[1, 64].Value = "添加劑飼料量";
                ws.Cells[1, 64, 9, 64].Merge = true;
                ws.Cells[1, 64].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[1, 64]);
                ws.Cells[10, 64].Value = "RPM";
                ws.Cells[10, 64].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[1, 65].Value = "產量";
                ws.Cells[1, 65, 9, 65].Merge = true;
                ws.Cells[1, 65].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[1, 65]);
                ws.Cells[10, 65].Value = "T/H";
                ws.Cells[10, 65].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[1, 66].Value = "成品品質";
                ws.Cells[1, 66, 1, 68].Merge = true;
                ws.Cells[1, 66].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 66].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                ws.Cells[1, 66].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 66].Value = "水份";
                ws.Cells[2, 66, 9, 66].Merge = true;
                ws.Cells[2, 66].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[2, 66]);
                ws.Cells[10, 66].Value = "%";
                ws.Cells[10, 66].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells[2, 67].Value = "比表面積";
                ws.Cells[2, 67, 9, 67].Merge = true;
                ws.Cells[2, 67].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[2, 67]);
                ws.Cells[10, 67].Value = "㎡/kg";
                ws.Cells[10, 67].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                ws.Cells[2, 68].Value = "篩餘";
                ws.Cells[2, 68, 9, 68].Merge = true;
                ws.Cells[2, 68].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excel_text_vertical(workbook, ws.Cells[2, 68]);
                ws.Cells[10, 68].Value = "%";
                ws.Cells[10, 68].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //文字垂直
                /*
                ws.Cells[1, 33].Style.TextRotation = 180;
                var workbook = excel.Workbook;
                workbook.Styles.UpdateXml();
                workbook.Styles.CellXfs[ws.Cells[1, 33].StyleID].TextRotation = 255;
                */



                //ws.Cells[1,20]. = eTextVerticalType.WordArtVertical 

                //eTextVerticalType.WordArtVertical;

                //自動欄寬
                //ws.Cells[ws.Dimension.Address].AutoFitColumns();



                //label名稱
                List<string> lb_name = new List<string>();
                for (int i = 0; i < 60; i++)
                {
                    lb_name.Add("lb_" + i.ToString());
                }
                int ct = 0;
                for (int i = 0; i < GV1.Rows.Count; i++)
                {
                    for (int j = 0; j < GV1.Columns.Count - 4; j++)
                    {
                        //double v = Convert.ToDouble(GV1.Rows[i].Cells[j + 5].Text);
                        string c = GV1.Rows[i].Cells[j + 4].Text;
                        if (c == "" || c == null)
                        {
                            Label lb = (Label)GV1.Rows[i].FindControl(lb_name[ct]);
                            c = lb.Text;
                            ct += 1;
                        }
                        //互換 耳軸承 潤滑油
                        if (j == 14 || j == 15)
                        {
                            ws.Cells[i + 11, j + 3].Value = c;
                            ws.Cells[i + 11, j + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        }
                        else if (j == 16 || j == 17)
                        {
                            ws.Cells[i + 11, j + -1].Value = c;
                            ws.Cells[i + 11, j + -1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        }
                        else if (j > 54 && j != 60)
                        {
                            ws.Cells[i + 11, j + 3].Value = c;
                            ws.Cells[i + 11, j + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        }
                        //產量
                        else if (j == 60)
                        {
                            
                            ws.Cells[i + 11, j + 3].Value = c;
                            ws.Cells[i + 11, j + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                            Label v1 = (Label)GV1.Rows[i].FindControl(lb_name[50]);
                            Label v2 = (Label)GV1.Rows[i].FindControl(lb_name[53]);
                            //double v1 = Convert.ToDouble(GV1.Rows[i].Cells[55].Text);
                            //double v2 = Convert.ToDouble(GV1.Rows[i].Cells[58].Text);
                            ws.Cells[i + 11, j - 3].Value = Convert.ToDouble(v1.Text) + Convert.ToDouble(v2.Text);

                            Label v3 = (Label)GV1.Rows[i].FindControl(lb_name[56]);
                            Label v4 = (Label)GV1.Rows[i].FindControl(lb_name[59]);

                            //double v3 = Convert.ToDouble(GV1.Rows[i].Cells[61].Text);
                            //double v4 = Convert.ToDouble(GV1.Rows[i].Cells[64].Text);
                            ws.Cells[i + 11, j + 5].Value = Convert.ToDouble(v3.Text) + Convert.ToDouble(v4.Text);

                        }
                        else
                        {
                            ws.Cells[i + 11, j + 1].Value = c;
                            ws.Cells[i + 11, j + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        }

                    }
                    ct = 0;
                }
                //成品品質
                time_s = Convert.ToDateTime(tb_SDATE.Text).AddDays(1).ToString("yyyy-MM-dd");
                call_SDS2_CMD(F, m, time_s);
                if (GV2.Rows.Count > 0)
                {
                    /*
                    for (int i = 0; i < GV2.Columns.Count; i++)
                    {
                        ws.Cells[1, i + 62].Value = GV2.Columns[i].HeaderText.Replace("<br>", "");
                        ws.Cells[1, i + 62].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[1, i + 62].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                    */
                    //ws.Cells[ws.Dimension.Address].AutoFitColumns();

                    int c = 0;
                    for (int i = 0; i < GV1.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(GV1.Rows[i].Cells[4].Text) == Convert.ToInt32(GV2.Rows[c].Cells[0].Text.Substring(0, 2)))
                        {
                            for (int j = 3; j < GV2.Columns.Count; j++)
                            {
                                ws.Cells[i + 11, j + 63].Value = GV2.Rows[c].Cells[j].Text;
                                ws.Cells[i + 11, j + 63].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            c += 1;
                            if (c == GV2.Rows.Count)
                            {
                                break;
                            }
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

        protected void btn_add_Click(object sender, EventArgs e)
        {
            string F = Request.QueryString["F"].Substring(0, 2);
            Response.Redirect("/Check/add_data.aspx?F=" + F + "");
        }



        public void GV1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                string B = Request.QueryString["B"];
                if (B == "Power")
                {
                    for (int i = 0; i < 49; i++)
                    {
                        e.Row.Cells[16 + i].Visible = false;
                        //GV1.Columns[16 + i].Visible = false;
                    }
                }
                else if (B == "Temp")
                {
                    
                    for (int i = 0; i < 11; i++)
                    {
                        e.Row.Cells[5 + i].Visible = false;
                        //GV1.Columns[5 + i].Visible = false;
                    }
                    
                    for (int j = 0; j < 23; j++)
                    {
                        e.Row.Cells[42 + j].Visible = false;
                        //GV1.Columns[42 + j].Visible = false;
                    }
                }
                else if (B == "Wind") //11
                {
                    for (int i = 0; i < 37; i++)
                    {
                        e.Row.Cells[5 + i].Visible = false;
                        //GV1.Columns[5 + i].Visible = false;
                    }
                    for (int j = 0; j < 12; j++)
                    {
                        e.Row.Cells[53 + j].Visible = false;
                        //GV1.Columns[53 + j].Visible = false;
                    }
                }
                else if (B == "Fd") //12
                {
                    for (int i = 0; i < 48; i++)
                    {
                        e.Row.Cells[5 + i].Visible = false;
                        //GV1.Columns[5 + i].Visible = false;
                    }
                }
            }
        }

        protected void btn_chart_Click(object sender, EventArgs e)
        {

            string F = Request.QueryString["F"];
            //先把6個checkbox的名稱加進來
            List<string> ch_box = new List<string>();
            for (int i = 0; i < 60; i++)
            {
                ch_box.Add("cb_" + i.ToString());
            }

            string v = "";
            string n = "";
            for (int j = 0; j < 60; j++)
            {
                List<int> t = (List<int>)Session["box"];
                if (t[j] == 1)
                {
                    if (GV1.HeaderRow.Cells[j + 5].ToolTip.ToString().Length > 0)
                    {
                        v += GV1.HeaderRow.Cells[j + 5].ToolTip + ",";
                        n += GV1.Columns[j + 5].HeaderText.Replace("#","").Replace("<br>","") + ",";
                    }
                }
            }
            
            
            if (v.Length > 0)
            {
                v = v.Remove(v.LastIndexOf(","), 1);
                n = n.Remove(n.LastIndexOf(","), 1);
                List<int> t = new List<int>();
                for (int i = 0; i < 60; i++)
                {
                    t.Add(0);
                }
                Session["box"] = t;
                Response.Redirect("/Tag/Tag_trend.aspx?F=" + F + "&M=C&V=" + v + "&N=" + n + "");
            }
            else
            {
                lb_error.Visible = true;
            }
            
        }

        protected void cb_box_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb_box = (CheckBox)sender;
            int x = Convert.ToInt32(cb_box.ID.Replace("cb_", ""));
            if (cb_box.Checked == true)
            {
                ((List<int>)Session["box"])[x] = 1;
                
            }
            else
            {
                ((List<int>)Session["box"])[x] = 0;
            }

        }

        
        public void visible_checkbox(List<int> t, int s, int e)
        {
            //cb_box名稱
            List<string> ch_name = new List<string>();
            for (int i = s; i < e; i++)
            {
                ch_name.Add("cb_" + i.ToString());
            }
            int c = e - s;

            for (int i = 0; i < c; i++)
            {
                CheckBox cb = (CheckBox)GV1.HeaderRow.FindControl(ch_name[i]);
                if (t[s + i] == 1)
                {
                    cb.Checked = true;
                }
            }
        }

        //checkbox顯示的部分
        protected void GV1_DataBound(object sender, EventArgs e)
        {
            string B = Request.QueryString["B"];
            //cb_box顯示
            List<int> t = ((List<int>)Session["box"]);
            if (B == "Power")
            {
                visible_checkbox(t, 0, 11);
            }
            else if (B == "Temp")
            {
                visible_checkbox(t, 11, 37);
            }
            else if (B == "Wind")
            {
                visible_checkbox(t, 37, 48);
            }
            else
            {
                visible_checkbox(t, 48, 60);
            }

        }
    }
}