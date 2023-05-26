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
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Xml;

namespace factory.PV
{
    public partial class PV_H : System.Web.UI.Page
    {
        public f_class ff = new f_class();
        public string chart()
        {
            if (GV1.Rows.Count > 0)
            {
                string F = Request.QueryString["F"];
                string M = Request.QueryString["M"];
                List<List<string>> par_list = new List<List<string>>();
                string md = tb_SDATE.Text.Substring(5, 5).Replace("-", "/");
                string factory = ff.factory_name(F)+"  ";
                if (F == "ZB-T1HIST" && M == "1")
                {
                    factory = "線西廠 ";
                }
                else if (F == "ZB-T1HIST" && M == "2")
                {
                    factory = "伸港廠 ";
                }
                string time_s = tb_SDATE.Text + " 00:00";
                string time_e = tb_SDATE.Text + " 23:00";
                par_list.Add(new List<string>() { "{ min:'" + time_s + "'}" });
                par_list.Add(new List<string>() { "{ max:'" + time_e + "'}" });
                par_list.Add(new List<string>() { "{ title:'" + factory + md + "太陽能'}" });

                string d = "";
                string y = tb_SDATE.Text.Substring(0, 4)+"-";
                int max = 0;
                //發電量(度)
                for (int i = 0; i < GV1.Rows.Count; i++)
                {
                    string datatime = y + GV1.Rows[i].Cells[0].Text.Replace("/","-");
                    Double z = Convert.ToDouble(GV1.Rows[i].Cells[1].Text);
                    int v = Convert.ToInt32(z);
                    if (v > 0)
                    {
                        d += "{x:" + "'" + datatime + "'" + ",y:" + v + "\"},";
                    }
                    if (v > max)
                    {
                        max = v;
                    }
                }
                //圖表的max和步長
                int stepSize = 20;
                if (max < 100)
                {
                    max = 100;
                }
                else
                {
                    if (max.ToString().Substring(1, 1) == "8" || max.ToString().Substring(1, 1) == "9")
                    {
                        max = Convert.ToInt32(max.ToString().Substring(0, 1) + "50") + 100;
                    }
                    else
                    {
                        max = Convert.ToInt32(max.ToString().Substring(0, 1) + "00") + 100;
                    }
                    stepSize = 50;
                }
                
                par_list.Add(new List<string>() { d });
                par_list.Add(new List<string>() { "{ power:'發電量(度)'}" });
                d = "";
                //每KW系統日均發電度數
                for (int i = 0; i < GV1.Rows.Count; i++)
                {
                    string datatime = y + GV1.Rows[i].Cells[0].Text.Replace("/", "-");
                    Double z = Convert.ToDouble(GV1.Rows[i].Cells[2].Text);
                    if (z > 0)
                    {
                        d += "{x:" + "'" + datatime + "'" + ",y:" + z + "\"},";
                    }
                }
                par_list.Add(new List<string>() { d });
                par_list.Add(new List<string>() { "{ power_avg:'每kW系統日均發電度數'}" });
                par_list.Add(new List<string>() { "{ max:'" + max + "'}" });
                par_list.Add(new List<string>() { "{ stepSize:'" + stepSize + "'}" });
                //轉換為JSON
                System.Web.Script.Serialization.JavaScriptSerializer o = new System.Web.Script.Serialization.JavaScriptSerializer();
                string datas = o.Serialize(par_list);

                //修改格式
                datas = datas.Replace("\\", "");
                datas = datas.Replace("\"", "");
                datas = datas.Replace("u0027", "'");
                return datas;
            }
            else
            {
                return "";
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tb_SDATE.Attributes.Add("readonly", "readonly");
                string F = Request.QueryString["F"];
                string M = Request.QueryString["M"];
                string D = Request.QueryString["D"];
                string d = DateTime.Now.ToString("yyyy-MM-dd");
                //預設為觀音廠 網址列參數禁止亂打
                string name = ff.factory_name(F);
                if (name == "")
                {
                    Response.Redirect("PV_H.aspx?F=KY-T1HIST&D=" + d + "");
                }

                //只有彰濱廠要判斷是哪個機器
                if (name == "彰濱廠" && M == "" || name == "彰濱廠" && (M != "1" && M != "2"))
                {
                    Response.Redirect("PV_H.aspx?F=ZB-T1HIST&M=1&D=" + d + "");
                }
                
                if (D == "" || D == null)
                {
                    if (name == "彰濱廠")
                    {
                        Response.Redirect("PV_H.aspx?F=ZB-T1HIST&M=" + M + "&D=" + d + "");
                    }

                    Response.Redirect("PV_H.aspx?F=" + F + "&D=" + d + "");
                }
                else
                {
                    DateTime t;
                    bool flag = DateTime.TryParse(D, out t);
                    if (flag == false || D.Length != 10)
                    {
                        if (name == "彰濱廠")
                        {
                            Response.Redirect("PV_H.aspx?F=ZB-T1HIST&M=" + M + "&D=" + d + "");
                        }
                        Response.Redirect("PV_H.aspx?F=" + F + "&D=" + d + "");
                    }
                }

                if (Session["d"] != null)
                {
                    d = Session["d"].ToString();
                    Session["d"] = null;
                    Response.Redirect("PV_H.aspx?F=" + F + "&D=" + d + "");
                    //tb_SDATE.Text = Session["d"].ToString();
                    //time_s = tb_SDATE.Text;
                }

                lb_factory.Text = name;
                hyl_factory.Text = name + " ＞";
                string f = F.Substring(0, 2);
                hyl_factory.NavigateUrl = "../phone/Factory_" + f + ".aspx";
                tb_SDATE.Text = D;
                

                ff.Enabled(tb_SDATE.Text,imgb_n);
                d = tb_SDATE.Text.Substring(0,7).Replace("-","/");
                string SE_ID;
                if (name == "觀音廠")
                {
                    SE_ID = "HG00025";
                    hyl_mgt.NavigateUrl = "PV_D.aspx?F=KY-T1HIST&D=" + d + "";
                }
                else if (name == "利澤廠")
                {
                    SE_ID = "HG00020";
                    hyl_mgt.NavigateUrl = "PV_D.aspx?F=LZ-T1HIST&D=" + d + "";
                }
                else if (name == "彰濱廠" && M=="1")
                {
                    SE_ID = "HG00010";
                    hyl_mgt.NavigateUrl = "PV_D.aspx?F=ZB-T1HIST&M=1&D=" + d + "";
                }
                else
                {
                    SE_ID = "HG00017";
                    hyl_mgt.NavigateUrl = "PV_D.aspx?F=ZB-T1HIST&M=2&D=" + d + "";
                }
                
                //之後要加上小港廠的

                string time_e = Convert.ToDateTime(tb_SDATE.Text).AddDays(1).ToString("yyyy-MM-dd");
                SDS1.SelectCommand = "SELECT DTime,Power_Generation,kwh_avg FROM G_SE_H WHERE SE_ID = @SE_ID AND DTime >= @time_s AND DTime < @time_e";
                SDS1.SelectParameters.Add("SE_ID", SE_ID);
                SDS1.SelectParameters.Add("time_s", tb_SDATE.Text);
                SDS1.SelectParameters.Add("time_e", time_e);
                ff.title(GV1);
            }
        }

        protected void tb_SDATE_TextChanged(object sender, EventArgs e)
        {
            ff.check_date(tb_SDATE.Text,tb_SDATE);
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            string d = tb_SDATE.Text;
            if (F != "ZB-T1HIST")
            {
                Response.Redirect("PV_H.aspx?F=" + F + "&D=" + d + "");
            }
            else
            {
                Response.Redirect("PV_H.aspx?F=" + F + "&M=" + M + "&D=" + d + "");
            }

        }

        protected void imgb_p_Click(object sender, ImageClickEventArgs e)
        {
            ff.check_date(tb_SDATE.Text,tb_SDATE);
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            string d = Convert.ToDateTime(tb_SDATE.Text).AddDays(-1).ToString("yyyy-MM-dd");
            if (F != "ZB-T1HIST")
            {
                Response.Redirect("PV_H.aspx?F=" + F + "&D=" + d + "");
            }
            else
            {
                Response.Redirect("PV_H.aspx?F=" + F + "&M=" + M + "&D=" + d + "");
            }

        }

        protected void imgb_n_Click(object sender, ImageClickEventArgs e)
        {
            ff.check_date(tb_SDATE.Text, tb_SDATE);
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            string d = Convert.ToDateTime(tb_SDATE.Text).AddDays(1).ToString("yyyy-MM-dd");
            if (F != "ZB-T1HIST")
            {
                Response.Redirect("PV_H.aspx?F=" + F + "&D=" + d + "");
            }
            else
            {
                Response.Redirect("PV_H.aspx?F=" + F + "&M=" + M + "&D=" + d + "");
            }
        }

        protected void imgb_excel_Click(object sender, ImageClickEventArgs e)
        {
            
            if (GV1.Rows.Count > 0)
            {
                string F = Request.QueryString["F"];
                string M = Request.QueryString["M"];
                string n = lb_factory.Text;
                if (F == "ZB-T1HIST" && M == "1")
                {
                    n = "線西廠";
                }
                else if (F == "ZB-T1HIST" && M == "2")
                {
                    n = "伸港廠";
                }

                string name = "太陽能_" + n + "_" + tb_SDATE.Text;
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var excel = new ExcelPackage();
                var ws = excel.Workbook.Worksheets.Add(name);

                //欄位名稱
                for (int i = 0; i < GV1.Columns.Count; i++)
                {
                    ws.Cells[21, i + 1].Value = GV1.Columns[i].HeaderText;
                    ws.Cells[21, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[21, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }
                //自動欄寬
                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                string y = tb_SDATE.Text.Substring(0, 4);

                //資料
                for (int j = 0; j < GV1.Rows.Count; j++)
                {
                    for (int i = 0; i < GV1.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            ws.Cells[j + 22, i + 1].Value = y + "/" + GV1.Rows[j].Cells[i].Text;
                            ws.Cells[j + 22, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[j + 200, i + 1].Value = GV1.Rows[j].Cells[i].Text.Substring(5,6);
                        }
                        else
                        {
                            string w = GV1.Rows[j].Cells[i].Text;
                            ws.Cells[j + 22, i + 1].Value = Convert.ToDouble(w);
                        }
                    }
                }
                
                //chart
                ExcelChartSerie serie = null;
                ExcelChart chart = ws.Drawings.AddChart("chart", eChartType.LineMarkers);
                
                chart.SetPosition(0, 0);
                chart.Legend.Add();
                chart.Legend.Position = eLegendPosition.Bottom;
                chart.Title.Text = name;
                chart.ShowHiddenData = true;
                chart.SetSize(1300, 400);//设置图表大小

                //line
                var line = chart.PlotArea.ChartTypes.Add(eChartType.LineMarkers);
                int ct = 21 + GV1.Rows.Count;
                string data = "C22:C" + ct.ToString();

                serie = line.Series.Add(ws.Cells[data], ws.Cells["A200:A300"]);
                serie.HeaderAddress = ws.Cells[21, 3];//设置每条线的名称
                line.YAxis.Crosses = eCrosses.Max;
                line.YAxis.MaxValue = 2;
                //bar
                var bar = chart.PlotArea.ChartTypes.Add(eChartType.ColumnClustered);
                //新增位置
                List<string> p = new List<string>();
                p.Add("B22:B" + ct.ToString());
                ExcelChartSerie serie0 = null;
                serie0 = bar.Series.Add(ws.Cells[p[0]], ws.Cells["A200:A300"]);
                serie0.Header = "發電量(度)";
                //第二條Y軸線
                bar.UseSecondaryAxis = true;
                bar.YAxis.Crosses = eCrosses.Min;
           
                

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

                ff.title(GV1);
            }

        }
    }
}