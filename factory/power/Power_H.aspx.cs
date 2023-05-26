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

namespace factory.power
{
    public partial class Power_H : System.Web.UI.Page
    {
        public f_class ff = new f_class();
        public string chart()
        {
            string F = Request.QueryString["F"];
            List<List<string>> par_list = new List<List<string>>();
            string md = tb_SDATE.Text.Substring(5, 5).Replace("-", "/");
            string factory = ff.factory_name(F)+"  ";
            string time_s = tb_SDATE.Text + " 01:00";
            string time_e = Convert.ToDateTime(tb_SDATE.Text).AddDays(1).ToString("yyyy-MM-dd 00:00");
            par_list.Add(new List<string>() { "{ min:'" + time_s + "'}" });
            par_list.Add(new List<string>() { "{ max:'" + time_e + "'}" });
            par_list.Add(new List<string>() { "{ title:'" + factory + md + "用電量'}" });

            //新增checkbox的名稱和值還有title名稱
            List<string> ch_name = new List<string>();
            List<string> lb_name = new List<string>();
            List<string> t = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                if (GV1.Columns[2 + i].Visible == true)
                {
                    string name1 = "ch_p" + (i + 1).ToString();
                    ch_name.Add(name1);
                    string name2 = "lb_p" + (i + 1).ToString();
                    lb_name.Add(name2);
                    t.Add(GV1.Columns[2 + i].HeaderText.Replace("<br>", ""));
                }
            }


            //有資料時才新增，以往是用SQL去撈資料，這邊改用GV來抓資料
            if (GV1.Rows.Count > 0)
            {
                //日期
                List<string> date = new List<string>();
                for (int i = 0; i < GV1.Rows.Count; i++)
                {
                    string time = tb_SDATE.Text.Substring(0, 5) + GV1.Rows[i].Cells[0].Text.Replace("/", "-");
                    date.Add(time);
                }

                string d = "";
                if (GV1.Columns[1].Visible == true)
                {
                    //用電量
                    for (int i = 0; i < GV1.Rows.Count; i++)
                    {
                        string datatime = date[i];
                        int z = Convert.ToInt32(GV1.Rows[i].Cells[1].Text.Replace(",", ""));
                        d += "{x:" + "'" + datatime + "'" + ",y:" + z + "\"},";
                    }
                    par_list.Add(new List<string>() { d });
                    par_list.Add(new List<string>() { "{ power:'用電量'}" });
                }
                else
                {
                    par_list.Add(new List<string>() { "" });
                    par_list.Add(new List<string>() { "" });
                }

                //其他
                for (int i = 0; i < t.Count; i++)
                {
                    d = "";
                    CheckBox ch_p1 = (CheckBox)GV1.HeaderRow.FindControl(ch_name[i]);
                    if (ch_p1.Checked == true)
                    {
                        for (int j = 0; j < GV1.Rows.Count; j++)
                        {
                            Label lb_p1 = (Label)GV1.Rows[j].FindControl(lb_name[i]);
                            //資料如果NULL或空給0
                            if (lb_p1.Text == "" || lb_p1.Text == null)
                            {
                                lb_p1.Text = "0";
                            }
                            string datatime = date[j];
                            int z = Convert.ToInt32(lb_p1.Text.Replace(",", ""));
                            d += "{x:" + "'" + datatime + "'" + ",y:" + z + "\"},";
                        }
                        par_list.Add(new List<string>() { d });
                        string n = t[i];
                        par_list.Add(new List<string>() { "{ p:'" + n + "'}" });
                    }
                }


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
                string D = Request.QueryString["D"];
                
                //判斷時間是否為9點前還是9點後
                DateTime start = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                DateTime end = Convert.ToDateTime("09:00");
                TimeSpan ts = start - end;
                int x = Convert.ToInt32(ts.TotalSeconds);
                string d;
                if (x > 0)
                {
                    d = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    d = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }

                //預設為觀音廠 網址列參數禁止亂打
                string name = ff.factory_name(F);
                if (name == "")
                {
                    Response.Redirect("Power_H.aspx?F=KY-T1HIST&D=" + d + "");
                }
                
                //判斷資料日期
                if (D == "")
                {
                    Response.Redirect("Power_H.aspx?F=" + F + "&D=" + d + "");
                }
                else
                {
                    DateTime t;
                    bool flag = DateTime.TryParse(D, out t);
                    if (flag == false || D.Length != 10)
                    {
                        Response.Redirect("Power_H.aspx?F=" + F + "&D=" + d + "");
                    }
                }
                
                //手機label加超連結
                hyl_factory.Text = name + " ＞";
                string f = F.Substring(0,2);
                hyl_factory.NavigateUrl = "../phone/Factory_" + f +".aspx";
                lb_factory.Text = name;
                
                //TEXTBOX的時間
                //string dd = D.Replace("/", "-");
                string time_s = D;
                tb_SDATE.Text = time_s;

                if (Session["d"] != null)
                {
                    d = Session["d"].ToString();
                    Session["d"] = null;
                    Response.Redirect("Power_H.aspx?F=" + F + "&D=" + d + "");
                    //tb_SDATE.Text = Session["d"].ToString();
                    //time_s = tb_SDATE.Text;
                }

                time_s += " 01:00";
                string time_e = Convert.ToDateTime(time_s).AddDays(1).ToString("yyyy-MM-dd 00:00");
                //隱藏不必要的欄位
                string sql = "SELECT * FROM G_Power_Mapping WHERE FactoryID = '" + F + "'";
                SQLDB db = new SQLDB();
                DataTable dt = db.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][1].ToString() == "")
                    {
                        GV1.Columns[1].Visible = false;
                    }
                    int z = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        if (dt.Rows[0][9 + z].ToString() == "")
                        {
                            GV1.Columns[2 + i].Visible = false;
                        }
                        z += 2;
                    }
                }

                sql = "SELECT * FROM G_Power_h WHERE FactoryID ='" + F + "' AND DTime >= '" + time_s + "' AND DTime <= '" + time_e + "'";
                SDS1.SelectCommand = sql;
                if (GV1.Rows.Count > 0)
                {
                    GV1.DataBind();
                    /*
                    //某些欄位需要顯示哪些機器名稱
                    List<string> ll = new List<string>();
                    for (int i = 1; i < 9; i++)
                    {
                        string n = "lb_n" + i.ToString();
                        ll.Add(n);
                    }
                    for (int j = 0; j < 8; j++)
                    {
                        Label txt = (Label)GV1.HeaderRow.FindControl(ll[j]);
                        txt.ToolTip = dt.Rows[0][j].ToString();
                    }
                    */

                    //固定表頭
                    ff.title(GV1);
                }

                //判斷當前日期 停用/啟動 下一個月的功能
                ff.Enabled(tb_SDATE.Text, imgb_n);
                //超連結
                string ym = tb_SDATE.Text.Substring(0, 7).Replace("-", "/");
                hyl_mgt.NavigateUrl = "/power/Power_D.aspx?F=" + F + "&D=" + ym + "";
            }
            //傳值給下一個網頁
            Session["d"] = tb_SDATE.Text;
        }

        protected void tb_SDATE_TextChanged(object sender, EventArgs e)
        {
            string n = tb_SDATE.Text;
            //判斷時間是否正確
            ff.check_date(n,tb_SDATE);
            string F = Request.QueryString["F"];
            string s = Convert.ToDateTime(tb_SDATE.Text).ToString("yyyy-MM-dd");
            Session["d"] = tb_SDATE.Text;
            Response.Write(s);
            Response.Redirect("Power_H.aspx?F=" + F + "&D=" + s + "");
            
        }

        protected void ch_p0_CheckedChanged(object sender, EventArgs e)
        {
            ff.title(GV1);
        }

        protected void imgb_p_Click(object sender, ImageClickEventArgs e)
        {

            string F = Request.QueryString["F"];
            string time_s = tb_SDATE.Text;
            ff.check_date(time_s,tb_SDATE);

            time_s = Convert.ToDateTime(time_s).AddDays(-1).ToString("yyyy-MM-dd");
            Session["d"] = time_s;
            string s = Convert.ToDateTime(time_s).ToString("yyyy-MM-dd");
            Response.Redirect("Power_H.aspx?F=" + F + "&D=" + s + "");
            
        }

        protected void imgb_n_Click(object sender, ImageClickEventArgs e)
        {

            string F = Request.QueryString["F"];
            string time_s = tb_SDATE.Text;
            ff.check_date(time_s,tb_SDATE);

            time_s = Convert.ToDateTime(time_s).AddDays(1).ToString("yyyy-MM-dd");
            //判斷時間是否大於當前日期
            ff.Enabled(time_s, imgb_n);
            

            Session["d"] = time_s;
            string s = Convert.ToDateTime(time_s).ToString("yyyy-MM-dd");
            Response.Redirect("Power_H.aspx?F=" + F + "&D=" + s + "");
            
        }

        protected void imgb_excel_Click(object sender, ImageClickEventArgs e)
        {
            //有資料的時候才執行
            if (GV1.Rows.Count > 0)
            {
                //新增checkbox的名稱和值還有title名稱
                List<string> ch_name = new List<string>();
                List<string> lb_name = new List<string>();
                List<string> t = new List<string>();

                for (int i = 0; i < 8; i++)
                {
                    if (GV1.Columns[1 + i].Visible == true)
                    {
                        string name1 = "ch_p" + (i + 1).ToString();
                        ch_name.Add(name1);
                        string name2 = "lb_p" + (i + 1).ToString();
                        lb_name.Add(name2);
                        t.Add(GV1.Columns[1 + i].HeaderText.Replace("<br>", ""));
                    }
                }

                //將gridview轉換成datatable
                //欄
                DataTable dt = new DataTable();
                DataColumn dc;
                DataRow dr;
                int c = 0;
                for (int i = 0; i < GV1.Columns.Count; i++)
                {
                    if (GV1.Columns[i].Visible == true)
                    {
                        string n = GV1.Columns[i].HeaderText;
                        if (n == null)
                        {
                            n = t[c];
                            c += 1;
                        }
                        n = n.Replace("<br>", "");
                        dc = new DataColumn();
                        dc.ColumnName = n;
                        dt.Columns.Add(dc);
                    }
                }
                c = 0;
                string s;
                for (int i = 0; i < GV1.Rows.Count; i++)
                {
                    dr = dt.NewRow();
                    int x = 0;
                    for (int j = 0; j < GV1.Columns.Count; j++)
                    {
                        if (GV1.Columns[j].Visible == true)
                        {
                            s = GV1.Rows[i].Cells[j].Text;

                            if (s == null || s == "")
                            {

                                Label lb = (Label)GV1.Rows[i].FindControl(lb_name[c]);
                                s = lb.Text;
                                c += 1;
                            }
                            dr[x] = s;
                            x += 1;
                        }
                    }
                    c = 0;
                    dt.Rows.Add(dr);
                }
                string name = "用電量_" + lb_factory.Text + "_" + tb_SDATE.Text;
                
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var excel = new ExcelPackage();
                var ws = excel.Workbook.Worksheets.Add(name);
                //欄位名稱
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ws.Cells[21, i + 1].Value = dt.Columns[i].ToString();
                    ws.Cells[21, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[21, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }
                string y = tb_SDATE.Text.Substring(0, 4);

                //資料
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            ws.Cells[j + 22, i + 1].Value = y + "/" + dt.Rows[j][i].ToString();
                            ws.Cells[j + 22, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[j + 200, i + 1].Value = dt.Rows[j][i].ToString().Substring(6,5);
                        }
                        else
                        {
                            string w = dt.Rows[j][i].ToString();
                            if (w == "" || w == null || w == "&nbsp;")
                            {
                                w = "0";
                            }
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
                string data = "B22:B" + ct.ToString();

                serie = line.Series.Add(ws.Cells[data], ws.Cells["A200:A300"]);
                serie.HeaderAddress = ws.Cells[21, 2];//设置每条线的名称
                
                //bar
                var bar = chart.PlotArea.ChartTypes.Add(eChartType.ColumnClustered);
                //新增位置
                List<string> p = new List<string>();
                p.Add("C22:C" + ct.ToString());
                p.Add("D22:D" + ct.ToString());
                p.Add("E22:E" + ct.ToString());
                p.Add("F22:F" + ct.ToString());
                p.Add("G22:G" + ct.ToString());
                p.Add("H22:H" + ct.ToString());
                p.Add("I22:I" + ct.ToString());
                p.Add("J22:J" + ct.ToString());

                ExcelChartSerie serie0 = null;
                for (int i = 0; i < ch_name.Count; i++)
                {
                    CheckBox ch_p1 = (CheckBox)GV1.HeaderRow.FindControl(ch_name[i]);
                    if (ch_p1.Checked == true)
                    {
                        serie0 = bar.Series.Add(ws.Cells[p[i]], ws.Cells["A200:A300"]);
                        serie0.Header = t[i];
                        //第二條Y軸線
                        bar.UseSecondaryAxis = true;
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
    }
}